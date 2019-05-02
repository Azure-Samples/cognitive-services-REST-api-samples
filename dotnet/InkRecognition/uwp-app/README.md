---
topic: sample
languages:
  - csharp
products:
  - uwp
  - windows
  - azure
---

# Ink Recognizer Cognitive Service C# UWP Sample

![Build passing](https://img.shields.io/badge/build-passing-brightgreen.svg) ![License](https://img.shields.io/badge/license-MIT-green.svg)

Ink Recognizer Cognitive Service provides recognition of digital ink. It takes the digital ink stroke data as input and provides a document tree with individual recognition units as output. This project has sample code to demonstrate a few ways developers can take advantage of the service.

## Features

This project framework provides the following features:

* Capturing very basic inking input.
* Creating the JSON payload using the JSON schema used by Ink Recognizer.
* Calling the Ink Recognizer REST APIs with the JSON payload
* Parsing the JSON response from the service, build the document tree and parse it.

## Contents

| File/folder | Description |
|-------------|-------------|
| `src`       | Sample source code. |
| `README.md` | This README file. |
| `LICENSE`   | The license for the sample. |

## Prerequisites

- Install [Visual Studio] (https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio) 
- Requires subscription key from [Azure Cognitive Services] (https://docs.microsoft.com/en-us/azure/cognitive-services/authentication) 

## Build the sample

1. Clone or download this sample repository
2. Start Visual Studio and select **File > Open > Project/Solution**
3. Replace "[YOUR SUBSCRIPTION KEY]" in NoteTaker.xaml.cs with valid subscription key  
4. Press Ctrl+Shift+B, or select **Build > Build Solution**

## Run the sample

The next steps depend on whether you just want to deploy the sample or you want to both deploy and run it.

### Deploying the sample
Select Build > Deploy Solution.

### Debugging and running the sample
To debug the sample and then run it, press F5 or select Debug > Start Debugging. To run the sample without debugging, press Ctrl+F5 or select Debug > Start Without Debugging.
