---
topic: sample
languages:
  - java
products:
  - android
  - azure
---

# Ink Recognizer Cognitive Service Java (on Android) Sample 
![Build passing](https://img.shields.io/badge/build-passing-brightgreen.svg) ![License](https://img.shields.io/badge/license-MIT-green.svg)

Ink Recognizer Cognitive Service provides recognition of digital ink. It takes the digital ink stroke data as input and provides a document tree with individual recognition units as output. This project has sample code to demonstrate a few ways developers can take advantage of the service.
 
## Features

This project provides the following features:

* Capturing very basic inking input.
* Creating the JSON payload using the Ink Recognizer Service JSON schema.
* Asynchronously calling the Ink Recognizer REST APIs with the JSON payload
* Parsing the JSON response from the service to build the document tree.

## Contents

| File/folder | Description |
|-------------|-------------|
| `Recognizer`       | Sample source code. |
| `README.md` | This README file. |
| `LICENSE`   | The license for the sample. |

## Getting Started

### Prerequisites

Android Studio 3.2 on Windows 10/macOS El-capitan


### Quickstart
(Add steps to get up and running quickly)

1. git clone https://github.com/Azure-Samples/cognitive-services-java-android-ink-recognition
2. cd [respository name]
3. Launch Android Studio and select "Open an existing Android Studio project" option.
4. Navigate to the project directory and select the project file.
5. Replace <SUBSCRIPTION_KEY> in NoteTaker.java with your valid subscription key.


## Demo

1. Connect to a virtual device in Android studio
2. Build and run the project
3. Write something on the screen of the virtual device once it is loaded.
4. After 2 seconds of inactivity, the ink will be recognized and the result will be visible in a toast control.

## Resources

Additional resources related the project are located below

- [Learn more about Ink Recognizer](http://go.microsoft.com/fwlink/?LinkID=2084782)
- [Ink Recognizer API Reference](http://go.microsoft.com/fwlink/?LinkID=2085147)
- [Ink Recognizer JavaScript sample](https://github.com/azure-samples/cognitive-services-javascript-ink-recognition)
- [Ink Recognizer UWP sample](https://github.com/azure-samples/cognitive-services-csharp-ink-recognition)
