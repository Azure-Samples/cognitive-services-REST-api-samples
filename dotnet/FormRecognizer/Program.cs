using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;


namespace CSHttpClientSample
{
    static class Program
    {
        static void Main()
        {
            MakeRequest();
            Console.WriteLine("Please wait for the results to appear...");
            Console.ReadLine();
        }

        static async void MakeRequest() // Extract values from form 
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            string fileLoc = @"path\FileName.pdf";
            string contentType = "application/pdf";

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "Subsciption key");

            // Request parameters
            //queryString["keys"] = "test";
            var uri = "https://westus2.api.cognitive.microsoft.com/formrecognizer/v1.0-preview/custom/<modelID>/ff956100-613b-40d3-ae74-58e4fcc76384/analyze?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = GetFileAsByteArray(fileLoc);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                response = await client.PostAsync(uri, content);

                // Asynchronously get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.
                Console.WriteLine("\nResponse:\n\n{0}\n", JToken.Parse(contentString).ToString());
            }

        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetFileAsByteArray(string imageFilePath)
        {
            // Open a read-only file stream for the specified file.
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                // Read the file's contents into a byte array.
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
    }

}
