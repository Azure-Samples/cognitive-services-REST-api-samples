---
topic: sample
languages:
  - javascript
---

# InkRecognizer Cognitive Service JavaScript Sample 
Ink Recognizer Cognitive Service provides recognition of digital ink. It takes the digital ink stroke data as input and provides a document tree with individual recognition units as output. This project has sample code to demonstrate a few ways developers can take advantage of the service.

![Build passing](https://img.shields.io/badge/build-passing-brightgreen.svg) ![License](https://img.shields.io/badge/license-MIT-green.svg)

## Features

This project provides the following features:

* Capturing very basic inking input.
* Creating the JSON payload using the Ink Recognizer Service JSON schema.
* Asynchronously calling the Ink Recognizer REST APIs with the JSON payload
* Parsing the JSON response from the service to build the document tree.

## Contents

| File/folder | Description |
|-------------|-------------|
| `src`       | Sample source code. |
| `README.md` | This README file. |
| `LICENSE`   | The license for the sample. |

## Getting Started

### Prerequisites
Request a subscription key.

### Quick Start
1. Clone or download this sample.
2. Replace <SUBSCRIPTION_KEY> in config.js with your valid subscription key.
3. Open sample.html in browser.
