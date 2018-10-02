const fetch = require('node-fetch');
const fs = require('fs');
const util = require('util');
const delay = require('delay');

const readFile = util.promisify(fs.readFile);

// Based on https://westus.dev.cognitive.microsoft.com/docs/services/5adf991815e1060e6355ad44/operations/587f2c6a154055056008f200
class CognitiveServiceOCR {
  constructor(apiKey, opts = {}) {
    if (!apiKey) throw new Error('apiKey is required');

    this.apiKey = apiKey;
    this.baseUrl = "https://westeurope.api.cognitive.microsoft.com/vision/v2.0/";
    this.mode = opts.mode || 'Printed'; // Printed or Handwritten depending on what we want to detect
  }

  /**
   * An util to help you get the binary data of an image
   * @param {string} imagePath 
   */
  async readImageByPath(imagePath) {
    const imageBinary = await readFile(imagePath);
    const fileData = imageBinary.toString('hex');
    const result = [];
    for (let i = 0; i < fileData.length; i += 2) {
      result.push(parseInt(`${fileData[i]}${fileData[i + 1]}`, 16))
    }
    return Buffer.from(result);
  }

  async checkAnalyzeImageJob(operationLocationUrl) {
    return new Promise(async (resolve, reject) => {
      try {
        const response = await fetch(operationLocationUrl, {
          method: 'GET',
          headers: {
            "Content-Type": "application/json",
            "Ocp-Apim-Subscription-Key": this.apiKey
          }
        });
  
        const json = await response.json();
        return resolve(json);
      } catch (e) {
        return reject(e);
      }
    })
  }

  async submitAnalyzeImageJob(imageBinary, opts) {
    return new Promise(async (resolve, reject) => {
      const url = this.baseUrl + "recognizeText" + "?"
      + `mode=${this.mode}`;

      try {
        const response = await fetch(url, {
          method: 'POST',
          headers: {
            "Content-Type": "application/octet-stream",
            "Ocp-Apim-Subscription-Key": this.apiKey
          },
          body: imageBinary
        });

        if (response.headers.has('operation-location')) {
          return resolve(response.headers.get('operation-location'));
        } else {
          return reject(response.headers);
        }
      } catch (e) {
        return reject(e);
      }
    });
  }
  
  async analyzeImage(imageBinary, opts) {
    return new Promise(async (resolve, reject) => {
      const jobLocation = await this.submitAnalyzeImageJob(imageBinary, opts);

      let isDone = false;

      while (!isDone) {
        const result = await this.checkAnalyzeImageJob(jobLocation);
        if (result.status == 'NotStarted') {
          console.log('Recognition is not started yet');
          isDone = false;
          await delay(1000);
        } else if (result.status == 'Succeeded') {
          return resolve(result.recognitionResult);
        } else if (result.status == 'Running') {
          console.log('Recognition is running, awaiting result');
          isDone = false;
          await delay(1000);
        } else {
          return reject('Unknown status: ' + JSON.stringify(result));
        }
      }

      return reject('SHOULD NOT REACH');
    });
  }
}

module.exports = CognitiveServiceOCR;
