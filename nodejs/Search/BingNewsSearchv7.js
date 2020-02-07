// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

'use strict';

let request = require('request');

/**
 * This sample uses the Bing News Search API to query the web with a 
 * search term(s) and get relevant news sites in return.
 */

// Add your Bing Search V7 subscription key and endpoint to your environment variables.
let subscriptionKey = process.env['BING_SEARCH_V7_SUBSCRIPTION_KEY']
let endpoint = process.env['BING_SEARCH_V7_ENDPOINT'] + '/bing/v7.0/news/search';

// News topics you'd like to search for.
let query = 'Microsoft';
// Market you'd like to search in.
let mkt = 'en-US'

// Construct parameters
let request_params = {
    method: 'GET',
    uri: endpoint,
    headers: {
        'Ocp-Apim-Subscription-Key': subscriptionKey
    },
    qs: {
        q: query,
        mkt: mkt
    },
    json: true
}

// Make request
request(request_params, function (error, response, body) {
    console.error('error:', error)
    console.log('statusCode:', response && response.statusCode)
    console.log('original query: ' + body.queryContext.originalQuery)
    console.log()
    console.log(body)
})
