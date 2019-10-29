//Copyright (c) Microsoft Corporation. All rights reserved.
//Licensed under the MIT License.
// <vars>
var request = require("request");
// Add your Bing Custom Search subscription key to your environment variables.
// Your endpoint will have the form: 
//   https://<your-custom-subdomain>.cognitiveservices.azure.com/bingcustomsearch/v7.0
var subscriptionKey = process.env['BING_CUSTOM_SEARCH_SUBSCRIPTION_KEY'];
var endpoint = process.env['BING_CUSTOM_SEARCH_ENDPOINT'];
var customConfigId = 'YOUR-CUSTOM-CONFIG-ID'; //you can also use "1"
var searchTerm = 'microsoft';
// </vars>
// <requestOptions>
var options = {
    url: endpoint + "/search?" + 
      'q=' + searchTerm + 
      '&customconfig=' + customConfigId,
    headers: {
        'Ocp-Apim-Subscription-Key' : subscriptionKey
    }
}
// </requestOptions>
// <requestMethod>
request(options, function(error, response, body){
    var searchResponse = JSON.parse(body);
    for(var i = 0; i < searchResponse.webPages.value.length; ++i){
        var webPage = searchResponse.webPages.value[i];
        console.log('name: ' + webPage.name);
        console.log('url: ' + webPage.url);
        console.log('displayUrl: ' + webPage.displayUrl);
        console.log('snippet: ' + webPage.snippet);
        console.log('dateLastCrawled: ' + webPage.dateLastCrawled);
        console.log();
    }
})
// </requestMethod>