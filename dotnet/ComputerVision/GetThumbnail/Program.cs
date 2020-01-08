using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Microsoft.Azure.CognitiveServices.Samples.ComputerVision.GetThumbnail
{
    using Newtonsoft.Json.Linq;

    class Program
    {
        // Add your Azure Computer Vision subscription key and endpoint to your environment variables
        public static string subscriptionKey = Environment.GetEnvironmentVariable("COMPUTER_VISION_SUBSCRIPTION_KEY");
        public static string endpoint = Environment.GetEnvironmentVariable("COMPUTER_VISION_ENDPOINT");
        
        static async Task Main(string[] args)
        {
            await GetThumbnailSample.RunAsync(endpoint, subscriptionKey);

            Console.WriteLine("\nPress ENTER to exit.");
            Console.ReadLine();
        }
    }

    public class GetThumbnailSample
    {
        public static async Task RunAsync(string endpoint, string key)
        {
            Console.WriteLine("Get thumbnail of specific size in images:");

            string imageFilePath = @"Images\objects.jpg"; // See this repo's readme.md for info on how to get these images. Alternatively, you can just set the path to any appropriate image on your machine.
            string remoteImageUrl = "https://github.com/Azure-Samples/cognitive-services-sample-data-files/raw/master/ComputerVision/Images/celebrities.jpg";

            await GetThumbnailFromStreamAsync(imageFilePath, endpoint, key, 50, 60, ".");
            await GetThumbnailFromUrlAsync(remoteImageUrl, endpoint, key, 50, 60, ".");
        }

        static async Task GetThumbnailFromStreamAsync(string imageFilePath, string endpoint, string subscriptionKey, int width, int height, string localSavePath)
        {
            if (!File.Exists(imageFilePath))
            {
                Console.WriteLine("\nInvalid file path");
                return;
            }
            try
            {
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                string uriBase = $"{endpoint}/vision/v2.0/generateThumbnail";
                string requestParameters = $"width={width}&height={height}&smartCropping=true";
                // Assemble the URI for the REST API method.
                string uri = uriBase + "?" + requestParameters;

                // Read the contents of the specified local image into a byte array.
                byte[] byteData = GetImageAsByteArray(imageFilePath);
                // Add the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses the "application/octet-stream" content type.
                    // The other content types you can use are "application/json" and "multipart/form-data".
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    // Asynchronously call the REST API method.
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        //Display the response
                        Console.WriteLine("\nResponse:\n{0}", response);
                        //Get the thumbnail image to save
                        byte[] thumbnailImageData = await response.Content.ReadAsByteArrayAsync();
                        //Save the thumbnail image. This will overwrite existing images at the path
                        string imageName = Path.GetFileName(imageFilePath);
                        string thumbnailFilePath = Path.Combine(localSavePath, imageName.Insert(imageName.Length - 4, "_thumb"));
                        File.WriteAllBytes(thumbnailFilePath, thumbnailImageData);
                        Console.WriteLine("Saved the image to {0}\n", thumbnailFilePath);
                    }
                    else
                    {
                        // Display the JSON error data.
                        string errorString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("\n\nResponse:\n{0}\n", JToken.Parse(errorString).ToString());
                    }
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

        static async Task GetThumbnailFromUrlAsync(string imageUrl, string endpoint, string subscriptionKey, int width, int height, string localSavePath)
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
                string uriBase = $"{endpoint}/vision/v2.0/generateThumbnail";
                string requestParameters = $"width={width}&height={height}&smartCropping=true";
                string uri = uriBase + "?" + requestParameters;
                Console.WriteLine(uri);

                string requestBody = " {\"url\":\"" + imageUrl + "\"}";
                var content = new StringContent(requestBody);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Post the request and display the result
                HttpResponseMessage response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nResponse:\n{0}", response);
                    //Get the thumbnail image to save
                    byte[] thumbnailImageData = await response.Content.ReadAsByteArrayAsync();
                    //Save the thumbnail image. This will overwrite existing images at the path
                    string imageName = Path.GetFileName(imageUrl);
                    string thumbnailFilePath = Path.Combine(localSavePath, imageName.Insert(imageName.Length - 4, "_thumb"));
                    File.WriteAllBytes(thumbnailFilePath, thumbnailImageData);
                    Console.WriteLine("Saved the thumbnail image from URL to {0}\n", thumbnailFilePath);
                }
                else
                {
                    // Display the JSON error data.
                    string errorString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("\n\nResponse:\n{0}\n", JToken.Parse(errorString).ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }
    }
}
