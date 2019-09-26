using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Microsoft.Azure.CognitiveServices.Samples.ComputerVision.AnalyzeImage
{
    using Newtonsoft.Json.Linq;

    class Program
    {
        // Add your Azure Computer Vision subscription key and endpoint to your environment variables
        public const string subscriptionKey = Environment.GetEnvironmentVariable("COMPUTER_VISION_SUBSCRIPTION_KEY");
        public const string endpoint = Environment.GetEnvironmentVariable("COMPUTER_VISION_ENDPOINT");
        
        static void Main(string[] args)
        {
            AnalyzeImageSample.RunAsync(endpoint, subscriptionKey).Wait(6000);
            Console.WriteLine("\nPress ENTER to exit.");
            Console.ReadLine();
        }
    }

    public class AnalyzeImageSample
    {
        public static async Task RunAsync(string endpoint, string key)
        {
            Console.WriteLine("Images being analyzed:");
            // See this repo's readme.md for info on how to get these images. Or, set the path to any image on your machine.
            string imageFilePath = @"Images\faces.jpg"; 
            string remoteImageUrl = "https://github.com/Azure-Samples/cognitive-services-sample-data-files/raw/master/ComputerVision/Images/landmark.jpg";

            await AnalyzeFromUrlAsync(remoteImageUrl, endpoint, key);
            await AnalyzeFromStreamAsync(imageFilePath, endpoint, key);
        }

        static async Task AnalyzeFromStreamAsync(string imageFilePath, string endpoint, string subscriptionKey)
        {
            if (!File.Exists(imageFilePath))
            {
                Console.WriteLine("Invalid file path");
                return;
            }

            try
            {
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                // Request parameters. A third optional parameter is "details".
                // Comment parameters that aren't required
                string requestParameters = "visualFeatures=" +
                    "Categories," +
                    "Description," +
                    "Color, " +
                    "Tags, " +
                    "Faces, " +
                    "ImageType, " +
                    "Adult , " +
                    "Brands , " +
                    "Objects"
                    ;

                // Assemble the URI for the REST API method.
                string uri = $"{endpoint}/vision/v2.0/analyze?{requestParameters}";

                // Read the contents of the specified local image
                // into a byte array.
                byte[] byteData = GetImageAsByteArray(imageFilePath);

                // Add the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses the "application/octet-stream" content type.
                    // The other content types you can use are "application/json" and "multipart/form-data".
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    // Asynchronously call the REST API method.
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    // Asynchronously get the JSON response.
                    string contentString = await response.Content.ReadAsStringAsync();

                    // Display the JSON response.
                    Console.WriteLine("\nResponse:\n\n{0}\n", JToken.Parse(contentString).ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }

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

        static async Task AnalyzeFromUrlAsync(string imageUrl, string endpoint, string subscriptionKey)
        {
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                Console.WriteLine("\nInvalid remote image url:\n{0} \n", imageUrl);
                return;
            }

            try
            {
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                // Request parameters. A third optional parameter is "details".
                // Comment parameters that aren't required
                string requestParameters = "visualFeatures=" +
                    "Categories," +
                    "Description," +
                    "Color, " +
                    "Tags, " +
                    "Faces, " +
                    "ImageType, " +
                    "Adult , " +
                    "Brands , " +
                    "Objects"
                    ;

                //Assemble the URI and content header for the REST API request
                string uri = $"{endpoint}/vision/v2.0/analyze?{requestParameters}";

                string requestBody = " {\"url\":\"" + imageUrl + "\"}";
                var content = new StringContent(requestBody);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Post the request and display the result
                HttpResponseMessage response = await client.PostAsync(uri, content);
                string contentString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("\nResponse:\n\n{0}\n", JToken.Parse(contentString).ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }
    }
}
