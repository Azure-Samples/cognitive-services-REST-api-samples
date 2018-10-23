//Copyright (c) Microsoft Corporation. All rights reserved.
//Licensed under the MIT License.

'use strict';

let https = require('https');

/* NOTE: Replace this endpoint with a valid Azure computer vision endpoint. Example https://northeurope.api.cognitive.microsoft.com/ */
let endpoint = 'https://northeurope.api.cognitive.microsoft.com/';

/* NOTE: Replace this example key with a valid subscription key (see the Prequisites section above). Also note v5 and v7 require separate subscription keys. */
let key = 'f04bc2820847439aa78e836355ea7af4';

/* NOTE: Replace this url with an image with text url */
let url = 'https://media-s3-us-east-1.ceros.com/ozy/images/2017/12/15/a1d39ecf46cbec0ddd89f130bcdb42bc/convo-1-text-4.png?imageOpt=1&fit=bounds&width=297';
let detectOrientation = false;

const ComputerVisionClient = require('azure-cognitiveservices-computervision');
const CognitiveServicesCredentials = require('ms-rest-azure').CognitiveServicesCredentials;

let credentials = new CognitiveServicesCredentials(key);
let client = new ComputerVisionClient(credentials, endpoint);

client.recognizePrintedText(detectOrientation, url)
    .then(ocrResult => {
        printOcrResult(ocrResult);
    });

function printOcrResult(ocrResult) {
    console.log('OcrResult')
    console.log(`language: ${ocrResult.language}`)
    console.log(`textAngle: ${ocrResult.textAngle}`)
    console.log(`orientation: ${ocrResult.orientation}`)

    ocrResult.regions.forEach((r, ri) => {
        console.log(`\tRegion ${ri} - boundingBox: ${r.boundingBox}`)

        r.lines.forEach((l, li) => {
            console.log(`\t\tLine ${li} - boundingBox: ${l.boundingBox}`)

            l.words.forEach((w, wi) => {
                console.log(`\t\t\tWord ${wi} - boundingBox: ${w.boundingBox}`)
                console.log(`\t\t\ttext: ${w.text}`)
            })

        });

    });
}
