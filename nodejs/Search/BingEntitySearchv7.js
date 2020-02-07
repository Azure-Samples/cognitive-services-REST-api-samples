
'use strict';

let request = require ('request');

/**
 * This sample uses the Bing Entity Search API to use an entity query to
 * get results with entity details like address and contact information.
 */

// Add your Bing Entity Search subscription key and endpoint to your environment variables.
let subscriptionKey = process.env['BING_ENTITY_SEARCH_SUBSCRIPTION_KEY']
let endpoint = process.env['BING_ENTITY_SEARCH_ENDPOINT'] + '/bing/v7.0/entities';

let mkt = 'en-US';
let query = 'italian restaurant near me';

//let query = '?mkt=' + mkt + '&q=' + encodeURI(q);

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

    console.log(body.queryContext.originalQuery)
    console.log()
    body.places.value.forEach(entity => {
        console.log(entity)
    })
})
