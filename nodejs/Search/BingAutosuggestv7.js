// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

'use strict';

let request = require ('request');

/**
 * This samnple uses the Bing Autosuggest API with a text search query
 * that returns website suggestions based on the word(s) submitted.
 */

// Add your Bing Autosuggest subscription key and endpoint to your environment variables.
let subscriptionKey = process.env['BING_AUTOSUGGEST_SUBSCRIPTION_KEY']
let endpoint = process.env['BING_AUTOSUGGEST_ENDPOINT'] + '/bing/v7.0/Suggestions';

// Search term
let query = 'sail';
// Market to perform the search
let mkt = 'en-US'

// Construct parameters
let request_params = {
    method: 'GET',
    uri: endpoint,
    headers: { 'Ocp-Apim-Subscription-Key': subscriptionKey },
    qs: { q: query, mkt: mkt },
    json: true
}

// Make request
request(request_params, function (error, response, body) {
    console.error('error:', error)
    console.log('statusCode:', response && response.statusCode)

    console.log(body)
    console.log()
    body.suggestionGroups.forEach( sugg => {
        console.log(sugg)
    })
})
