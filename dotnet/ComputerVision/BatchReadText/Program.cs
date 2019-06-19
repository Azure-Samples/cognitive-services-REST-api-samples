using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Microsoft.Azure.CognitiveServices.Samples.ComputerVision.BatchReadText
{
    using Newtonsoft.Json.Linq;

    public class Program
    {
        public const string subscriptionKey = "<your training key here>"; //Insert your Cognitive Services subscription key here
        public const string endpoint = "https://westus.api.cognitive.microsoft.com"; // You must use the same Azure region that you generated your subscription keys for.  Free trial subscription keys are generated in the westus region. 

        static void Main(string[] args)
        {
            BatchReadTextSample.RunAsync(endpoint, subscriptionKey).Wait(5000);

            Console.WriteLine("\nPress ENTER to exit.");
            Console.ReadLine();
        }
    }

    public class BatchReadTextSample
    {
        public static async Task RunAsync(string endpoint, string key)
        {
            Console.WriteLine("Extracting text from the images:");

            string imageFilePath = @"Images\handwritten_text.jpg"; // See this repo's readme.md for info on how to get these images. Alternatively, you can just set the path to any appropriate image on your machine.
            string remoteImageUrl = "https://github.com/Azure-Samples/cognitive-services-sample-data-files/raw/master/ComputerVision/Images/printed_text.jpg";

            await BatchReadTextFromStreamAsync(imageFilePath, endpoint, key);  
            await BatchReadTextFromUrlAsync(remoteImageUrl, endpoint, key); 
        }

        
        static async Task BatchReadTextFromStreamAsync(string imageFilePath, string endpoint, string subscriptionKey)
        {
            if (!File.Exists(imageFilePath))
            {
                Console.WriteLine("\nInvalid file path");
                return;
            }

            // Two REST API methods are required to extract handwritten or printed text.
            // This method is to submit the image for processing. 
            // The other method, namely WaitForExtractTextOperationResultAsync is to retrieve the text found in the image.

            try
            {
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                
                //Assemble the URI and content header for the REST API request
                string uri = endpoint+@"/vision/v2.0/read/core/asyncBatchAnalyze";

                // Reads the contents of the specified local image into a byte array.
                byte[] byteData = GetImageAsByteArray(imageFilePath);

                // Adds the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses the "application/octet-stream" content type.
                    // The other content types you can use are "application/json" and "multipart/form-data".
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    // The first REST API method, Batch Read, starts the async process to analyze the written text in the image.
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    await WaitForExtractTextOperationResultAsync(client, response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            // Open a read-only file stream for the specified file.
            using (FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                // Read the file's contents into a byte array.
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

        static async Task WaitForExtractTextOperationResultAsync(HttpClient client, HttpResponseMessage response)
        {
            // operationLocation stores the URI of the second REST API method returned by the first REST API method.
            string operationLocation;

            // The response header for the Batch Read method contains the URI of the second method, Read Operation Result, which returns the results of the process in the response body.
            // The Batch Read operation does not return anything in the response body.
            if (response.IsSuccessStatusCode)
            {
                operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();
            }
            else
            {
                // Display the JSON error data.
                string errorString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("\n\nResponse:\n{0}\n", JToken.Parse(errorString).ToString());
                return;
            }

            // If the first REST API method completes successfully, the second REST API method retrieves the text written in the image.
            // Note: The response may not be immediately available.
            // This example checks once per second for ten seconds.
            string contentString;
            int i = 0;
            do
            {
                System.Threading.Thread.Sleep(1000);
                response = await client.GetAsync(operationLocation);
                contentString = await response.Content.ReadAsStringAsync();
                ++i;
            } while (i < 10 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1);

            if (i == 10 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1)
            {
                Console.WriteLine("\nTimeout error.\n");
                return;
            }

            // Display the JSON response.
            Console.WriteLine("\nResponse:\n\n{0}\n", JToken.Parse(contentString).ToString());
        }

        /// <summary>
        /// Gets the text from the specified image URL by using the Computer Vision REST API.
        /// </summary>
        static async Task BatchReadTextFromUrlAsync(string remoteImgUrl, string endpoint, string subscriptionKey)
        {
            if (!Uri.IsWellFormedUriString(remoteImgUrl, UriKind.Absolute))
            {
                Console.WriteLine("\nInvalid remote image url:\n{0} \n", remoteImgUrl);
                return;
            }

            try
            {
                //Assemble the URI and content header for the REST API request
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                string uri = endpoint + @"/vision/v2.0/read/core/asyncBatchAnalyze";
                string requestBody = " {\"url\":\"" + remoteImgUrl + "\"}";
                var content = new StringContent(requestBody);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Post the request
                HttpResponseMessage response = await client.PostAsync(uri, content);

                // The response header for the Batch Read method contains the URI of the second method, Read Operation Result, which returns the results of the process in the response body.
                await WaitForExtractTextOperationResultAsync(client, response);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }
    }
}
