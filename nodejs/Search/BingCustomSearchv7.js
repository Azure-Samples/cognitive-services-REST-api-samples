//Copyright (c) Microsoft Corporation. All rights reserved.
//Licensed under the MIT License.

var request = require("request");

var subscriptionKey = process.env['BING_CUSTOM_SEARCH_SUBSCRIPTION_KEY']
var customConfigId = 'YOUR-CUSTOM-CONFIG-ID';
var searchTerm = 'microsoft';

var options = {
    url: process.env['BING_CUSTOM_SEARCH_ENDPOINT'] + "/bingcustomsearch/v7.0/search?' + 
      'q=' + searchTerm + 
      '&customconfig=' + customConfigId,
    headers: {
        'Ocp-Apim-Subscription-Key' : subscriptionKey
    }
}

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
