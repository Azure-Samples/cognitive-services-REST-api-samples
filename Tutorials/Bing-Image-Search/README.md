# Bing Image Search single-page web app

This single-page web application demonstrates how the Bing Image Search API (an [Azure Cognitive Service](https://docs.microsoft.com/azure/cognitive-services/)) can be used to retrieve, parse, and display relevant image results based on a user's query. This sample compliments the [single-page web app](https://docs.microsoft.com/azure/cognitive-services/bing-image-search/tutorial-bing-image-search-single-page-app) tutorial on docs.microsoft.com.

The sample app can:

* Call the Bing Image Search API with search options
* Display image results
* Paginate results
* Manage subscription keys
* Handle errors

To use this app, you must have a valid Azure subscription. If you don't have one, you can [get an account](https://azure.microsoft.com/free/) for free. After you sign up, you can use the subscription key from creating an [Azure Cognitive Services resource](http://docs.microsoft.com/azure/cognitive-services/cognitive-services-apis-create-account) for the Bing Search APIs. 

## Prerequisites 

* Node.js 8 or later
* A valid Azure Cognitive Services subscription key for the Bing Search APIs

## Get started

1. Clone the repository.
2. Navigate to the Bing Web Search Tutorial directory.
3. Install Express.js:
    `npm install`

4. Run the sample app:
    `node bing-web-search.js`

5. Navigate to the provided URL and perform your first Bing Image Search!

## Next steps

* See the [single-page webapp tutorial](https://docs.microsoft.com/azure/cognitive-services/bing-image-search/tutorial-bing-image-search-single-page-app) that goes along with this sample.
* Explore all of the available [Azure Cognitive Services](https://docs.microsoft.com/azure/cognitive-services/).
* Use [computer vision](https://docs.microsoft.com/azure/cognitive-services/computer-vision/quickstarts-sdk/csharp-analyze-sdk) to quickly analyze an image.
* View the rest of the [Bing Image Search Documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/bing-image-search/).