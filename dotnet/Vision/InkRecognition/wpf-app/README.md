---
topic: sample
languages:
  - C#
products:
  - WPF
  - azure
---

# Ink Recognizer Cognitive Service C# WPF Sample

![Build passing](https://img.shields.io/badge/build-passing-brightgreen.svg) ![License](https://img.shields.io/badge/license-MIT-green.svg)

Ink Recognizer Cognitive Service provides recognition of digital ink. It takes the digital ink stroke data as input and provides a document tree with individual recognition units as output. This project has sample code to demonstrate a few ways developers can take advantage of the service. This sample also shows a use case for [Xaml Island](https://docs.microsoft.com/en-us/windows/uwp/xaml-platform/xaml-host-controls) on InkCanvas, which import UWP controls into desktop applications.

## Features

This sample provides the following features:

* Collecting ink strokes from Xaml Island InkCanvas
* Creating JSON request following InkRecognizer service's schema.
* Calling the InkRecognizer REST API
* Parsing the JSON reponse to build the document tree

## Contents

| File/folder | Description |
|-------------|-------------|
| `src`       | Sample source code. |
| `README.md` | This README file. |
| `LICENSE`   | The license for the sample. |

## Getting Started

### Prerequisites

* [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio)
* [.Net Framework >= 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework)
* Windows SDK Version >= 10.0.18362. You can install it from Visual Studio Installer under **Indivial components** tab, or download from [Windows 10 SDK](https://developer.microsoft.com/en-us/windows/downloads/windows-10-sdk)
  * If you install the Windows SDK into a customized path, please edit **src\NoteTaker.csproj** to change the HintPath of **Windows.winmd** to the correct path
* Nuget Packages
  * [Newtonsoft.JSON](https://www.newtonsoft.com/json)
  * [Microsoft.Net.Http](https://www.nuget.org/packages/Microsoft.Net.Http/)
  * [Xaml Island for WPF](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls)
* Subscription key from [Azure Cognitive Services](https://docs.microsoft.com/en-us/azure/cognitive-services/authentication)
* Windows Build 1903 or higher is required for Xaml island features

### Build the Sample

1. Clone or download this sample repository `git clone https://github.com/Azure-Samples/cognitive-services-csharp-wpf-ink-recognition.git`
2. Start Visual Studio and select **File > Open > Project/Solution** and open **src\NoteTaker.sln**
3. Replace "[YOUR SUBSCRIPTION KEY]" in **src\MainWindows.xaml.cs** with valid subscription key
4. Select **Build > Build Solution**

### Run the sample

* To debug the sample, select **Debug > Start Debugging**. To run the sample without debugging, select **Debug > Start Without Debugging**.
* Write down something on the upper region of the app
* After one second of inactivity, the ink will be recognized and the result will be visible in the lower region of the app

## Resources

Additional resources related the project are located below

* [Learn more about Ink Recognizer](http://go.microsoft.com/fwlink/?LinkID=2084782)
* [Ink Recognizer API Reference](http://go.microsoft.com/fwlink/?LinkID=2085147)
* [Ink Recognizer JavaScript sample](https://github.com/Azure-Samples/cognitive-services-REST-api-samples/tree/master/javascript/InkRecognition/javascript-app)
* [Ink Recognizer UWP sample](https://github.com/Azure-Samples/cognitive-services-REST-api-samples/tree/master/dotnet/InkRecognition/uwp-app)
* [Ink Recognizer Java sample](https://github.com/Azure-Samples/cognitive-services-REST-api-samples/tree/master/java/InkRecognition/android-sample-app)
* [Ink Recognizer Swift sample](https://github.com/Azure-Samples/cognitive-services-REST-api-samples/tree/master/swift/InkRecognition)