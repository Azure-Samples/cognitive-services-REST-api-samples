---
topic: sample
languages:
  - C# (dotnet)
products:
  - azure
  - cognitive services
---

# Sample Solution that uses the Computer Vision Rest APIs

The projects in this code are quickstarts that show how to use Computer Vision Rest API for local and remote images

## Contents

| File/folder | Description |
|-------------|-------------|
| AnalyzeImage       | Analyzes image on specified visual parameters |
| DetectObjects | Detects objects in an image |
| ExtractText | Looks for printed or handwritten text in an image|
| Images | Sample images to run the projects |
| OCR | Performs OCR on the image|
| ComputerVision.sln | The Visual Studio solution with the above as projects|
| readme.md | This README file. |


## Prerequisites

- [Visual Studio 2015](https://visualstudio.microsoft.com/) or higher
- [An Azure Subscription Key](https://azure.microsoft.com/en-us/try/cognitive-services/?api=computer-vision) 

## Setup

1. Clone or download this sample repository
2. Open the solution ComputerVision.sln in Visual Studio

## Running the samples

1. Right-click on the relavant project (detectObjects, extractText, etc) and click on 'Set as StartUp Project'
2. In 'Program.cs' , update the line ```private const string subscriptionKey = "<your training key here>";``` with your subscription key. For example, if your subscription key is `0123456789abcdef0123456789ABCDEF`, then the line should look like ``` private const string subscriptionKey = "0123456789abcdef0123456789ABCDEF"; ```
3. Update the endpoint with the region you generated your endpoint for. For example, if you are using the westcentralus endpoint, you should change the line ``` computerVision.Endpoint = "https://westus.api.cognitive.microsoft.com"; ``` to  ``` computerVision.Endpoint = "https://westcentralus.api.cognitive.microsoft.com"; ```
4. (Optional) Change the remote image URL and location of local image
5. (Optional) Comment out visual parameters not needed for AnalyzeImage 
6. Hit F5 or build the solution

## Resources
- An explanation of the various concepts and visual features in the API can be found [here](https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/home)
- A detailed documentations of the API can be found [here](https://westus.dev.cognitive.microsoft.com/docs/services/5adf991815e1060e6355ad44/operations/56f91f2e778daf14a499e1fa)
