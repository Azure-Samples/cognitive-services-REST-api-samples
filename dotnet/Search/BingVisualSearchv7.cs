using Newtonsoft.Json;
using System;
using System.Text;
using System.Net;
using System.IO;

/* This sample makes a call to the Bing Visual Search API with a query image and returns similar images with details.
 * Bing Visual Search API: 
 * https://docs.microsoft.com/en-us/rest/api/cognitiveservices/bingvisualsearch/images/visualsearch
 */

namespace BingVisualSearch
{
    class Program
    {
        // Add your Azure Bing Search V7 subscription key and endpoint to your environment variables.
        static string subscriptionKey = Environment.GetEnvironmentVariable("BING_SEARCH_V7_SUBSCRIPTION_KEY");
        static string endpoint = Environment.GetEnvironmentVariable("BING_SEARCH_V7_ENDPOINT") + "/bing/v7.0/images/visualsearch";

        // Set the path to the image that you want to get insights of. 
        static string imagePath = @"objects.jpg";

        // Boundary strings for form data in body of POST.
        const string CRLF = "\r\n";
        static string BoundaryTemplate = "batch_{0}";
        static string StartBoundaryTemplate = "--{0}";
        static string EndBoundaryTemplate = "--{0}--";

        const string CONTENT_TYPE_HEADER_PARAMS = "multipart/form-data; boundary={0}";
        const string POST_BODY_DISPOSITION_HEADER = "Content-Disposition: form-data; name=\"image\"; filename=\"{0}\"" + CRLF + CRLF;


        static void Main()
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;

                // Gets image.
                var filename = new FileInfo(imagePath).FullName;
                Console.WriteLine("Getting image insights for image: " + Path.GetFileName(filename));
                var imageBinary = File.ReadAllBytes(imagePath);

                // Sets up POST body.
                var boundary = string.Format(BoundaryTemplate, Guid.NewGuid());

                // Builds form start data.
                var startBoundary = string.Format(StartBoundaryTemplate, boundary);
                var startFormData = startBoundary + CRLF;
                startFormData += string.Format(POST_BODY_DISPOSITION_HEADER, filename);

                // Builds form end data.
                var endFormData = CRLF + CRLF + string.Format(EndBoundaryTemplate, boundary) + CRLF;
                var contentTypeHeaderValue = string.Format(CONTENT_TYPE_HEADER_PARAMS, boundary);

                // Sets up the request for a visual search.
                WebRequest request = HttpWebRequest.Create(endpoint);
                request.ContentType = contentTypeHeaderValue;
                request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;
                request.Method = "POST";

                // Writes the boundary and Content-Disposition header, then writes
                // the image binary, and finishes by writing the closing boundary.
                using (Stream requestStream = request.GetRequestStream())
                {
                    StreamWriter writer = new StreamWriter(requestStream);
                    writer.Write(startFormData);
                    writer.Flush();
                    requestStream.Write(imageBinary, 0, imageBinary.Length);
                    writer.Write(endFormData);
                    writer.Flush();
                    writer.Close();
                }

                // Calls the Bing Visual Search endpoint and returns the JSON response.
                HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
                string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

                Console.WriteLine("\nJSON Response:\n");
                dynamic parsedJson = JsonConvert.DeserializeObject(json);
                Console.WriteLine(JsonConvert.SerializeObject(parsedJson, Formatting.Indented));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
