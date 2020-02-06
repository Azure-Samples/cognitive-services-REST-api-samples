// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

'use strict';

const request = require ('request')

/** 
 * This sample uses the Bing Spell Check API to check the spelling of a sentence. 
 * It returns the identified misspellings with suggestions for correctly spelled words
 * Plus, a score of what are deemed to be the best matches for suggestions. 
 */

// Add your Bing Spell Check key and endpoint (host) to your environment variables.
// Note v5 and v7 require separate subscription keys.
let key = process.env['BING_SPELL_CHECK_SUBSCRIPTION_KEY']
let endpoint = process.env['BING_SPELL_CHECK_ENDPOINT'] + '/bing/v7.0/spellcheck/'

let text = "Hollo, wrld!"
let mode = "proof"
let mkt = "en-US"

// These values are used for optional headers (see below).
// let CLIENT_ID = "<Client ID from Previous Response Goes Here>";
// let CLIENT_IP = "999.999.999.999";
// let CLIENT_LOCATION = "+90.0000000000000;long: 00.0000000000000;re:100.000000000000";

let headers = {
    'Content-Type': 'application/x-www-form-urlencoded',
    'Content-Length': text.length + 5,
    'Ocp-Apim-Subscription-Key': key
    // Optional Headers
    // 'X-Search-Location' : CLIENT_LOCATION,
    // 'X-MSEdge-ClientID' : CLIENT_ID,
    // 'X-MSEdge-ClientIP' : CLIENT_ID,
}

let request_params = {
    method: 'POST',
    url: endpoint,
    headers: headers,
    qs: { 
        mode: mode,
        mkt: mkt,
        text: text 
    },
    json: true
}

request(request_params, function (error, response, body) {
    console.error('error:', error)
    console.log('statusCode:', response && response.statusCode)

    // Print suggestions for each misspelled word (token)
     body.flaggedTokens.forEach(token => {
        console.log(token)
    }) 
})
