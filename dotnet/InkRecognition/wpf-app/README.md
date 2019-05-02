---
topic: sample
languages:
  - C#
products:
  - WPF
  - azure
  - windows
---

# Ink Intelligence Cognitive Service C# WPF Sample

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
| `.gitignore` | Define what to ignore at commit time. |
| `CHANGELOG.md` | List of changes to the sample. |
| `CONTRIBUTING.md` | Guidelines for contributing to the sample. |
| `README.md` | This README file. |
| `LICENSE`   | The license for the sample. |

## Getting Started

### Prerequisites

* [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio)
* Windows SDK Version >= 10.0.18362. You can install it from Visual Studio Installer under **Indivial components** tab, or download from [Windows Insider Preview SDK](https://www.microsoft.com/en-us/software-download/windowsinsiderpreviewSDK)
  * If you install the Windows SDK into a customized path, please edit **src\NoteTaker.csproj** to change the HintPath of **Windows.winmd** to the correct path 
* Requires subscription key from [Azure Cognitive Services](https://docs.microsoft.com/en-us/azure/cognitive-services/authentication) 

## Build the Sample

1. Clone or download this sample repository `git clone https://github.com/Azure-Samples/cognitive-services-csharp-wpf-ink-recognition.git` 
2. Start Visual Studio and select **File > Open > Project/Solution** and open "NoteTaker.sln" 
3. Replace "[YOUR SUBSCRIPTION KEY]" in MainWindows.xaml.cs with valid subscription key
4. Press Ctrl+Shift+B, or select **Build > Build Solution**

## Run the sample

To debug the sample and then run it, press F5 or select Debug > Start Debugging. To run the sample without debugging, press Ctrl+F5 or select Debug > Start Without Debugging.