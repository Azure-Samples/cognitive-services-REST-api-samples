// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <vars>
var request = require("request");

/**
 * This sample uses the Bing Custom Search API to send a customized query
 * that returns a lot of data about the query with results.
 */

// Add your Bing Custom Search subscription key and endpoint to your environment variables.
var subscriptionKey = process.env['BING_CUSTOM_SEARCH_SUBSCRIPTION_KEY'];
var endpoint = process.env['BING_CUSTOM_SEARCH_ENDPOINT'] + "/bingcustomsearch/v7.0/search?";

var customConfigId = process.env['BING_CUSTOM_CONFIG']; //you can also use "1"

// Word(s) you want to search for.
var query = 'Microsoft';
// Market you want to search in.
let mkt = 'en-US'
// </vars>

// <requestOptions>
// Construct parameters
let request_params = {
    uri: endpoint,
    headers: {
        'Ocp-Apim-Subscription-Key': subscriptionKey
    },
    qs: {
        customConfig: customConfigId,
        q: query,
        mkt: mkt
    },
    json: true
}
// </requestOptions>

// <requestMethod>
// Make request
request(request_params, function (error, response, body) {
    console.error('error:', error)
    console.log('statusCode:', response && response.statusCode)

    console.log(body.queryContext)
    console.log()
    body.webPages.value.forEach(v => {
        console.log(v)
    })
})
// </requestMethod>
