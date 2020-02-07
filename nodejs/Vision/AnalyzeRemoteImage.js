// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

'use strict'

const request = require('request')

/**
 * This sample uses the Computer Vision API to analyze a remote image.
 * It returns image properties such as categories, description (tags), color, and size.
 *
 * Computer Vision API - v2 .1:
 * https: //westus.dev.cognitive.microsoft.com/docs/services/5cd27ec07268f6c679a3e641/operations/56f91f2e778daf14a499f20d
 */

// Add your Computer Vision subscription key and endpoint to your environment variables.
let subscriptionKey = process.env['COMPUTER_VISION_SUBSCRIPTION_KEY']
let endpoint = process.env['COMPUTER_VISION_ENDPOINT'] + '/vision/v2.1/analyze'

// Image to be analyzed; you may use your own URL image.
const url =
    'https://raw.githubusercontent.com/Azure-Samples/cognitive-services-sample-data-files/master/ComputerVision/Images/objects.jpg'

// Request parameters.
const params = {
    'visualFeatures': 'Categories,Description,Color',
    'details': '',
    'language': 'en'
}

// Options
const options = {
    uri: endpoint,
    qs: params,
    body: '{"url": ' + '"' + url + '"}',
    headers: {
        'Content-Type': 'application/json',
        'Ocp-Apim-Subscription-Key' : subscriptionKey
    }
}

// Make the request.
request.post(options, (error, response, body) => {
    console.error('error:', error)
    console.log('statusCode:', response && response.statusCode)
    console.log('original image:', url.substring(url.lastIndexOf('/') + 1))
    console.log()
    console.log(JSON.stringify(JSON.parse(body), null, 2))
})
