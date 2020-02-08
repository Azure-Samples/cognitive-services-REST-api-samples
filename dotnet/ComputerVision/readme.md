---
topic: sample
languages:
  - C# (dotnet)
products:
  - azure
  - cognitive services
---

# Sample Solution that uses the Computer Vision Rest APIs

The projects in this code are quickstarts that show how to use Computer Vision Rest API for local and remote images.

## Contents

| File/folder | Description |
|-------------|-------------|
| AnalyzeImage       | Project that analyzes image on specified visual parameters |
| DetectObjects | Project that detects objects in an image |
| BatchReadFile | Project that looks for printed or handwritten text in an image or PDF|
| OCR | Project that performs OCR on the image|
| DescribeImage| Project for captioning an image|
| GetAreaOfInterest| Project to find one important region in an image |
| RecognizeDomainSpecificContent| Project that recognizes celebrities and landmarks in an image |
| RecognizeText| Project that recognizes text in an image |
| TagImage| Project to tag an image|
| GetThumbnail | Gets a recommended thumbnail for an image given a target size |
| ComputerVision.sln | The Visual Studio solution with the above projects|
| readme.md | This README file. |


## Prerequisites

- [Visual Studio 2015](https://visualstudio.microsoft.com/) or higher
- [An Azure Subscription Key](https://azure.microsoft.com/en-us/try/cognitive-services/?api=computer-vision) 

## Setup

1. Clone or download this sample repository
2. Open the solution ComputerVision.sln in Visual Studio
3. Download the images from [this repo](https://github.com/Azure-Samples/cognitive-services-sample-data-files/blob/master/ComputerVision/Images/) and add the Images folder as content to each project. Alternatively modify the `imageFilePath` in all projects to reflect the path of an appropriate local image.

## Running the samples

1. Right-click on the relavant project (detectObjects, extractText, etc) and click on 'Set as StartUp Project'
2. In 'Program.cs', add your Azure Computer Vision subscription key and endpoint to your environment variables with the variable names: COMPUTER_VISION_SUBSCRIPTION_KEY and COMPUTER_VISION_ENDPOINT.
4. (Optional) Change the remote image URL and location of local image
5. (Optional) Comment out visual parameters not needed for AnalyzeImage 
6. Hit F5 or build the solution

## Resources
- Computer Vision documentation: https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/home
- Computer Vision API: (https://westus.dev.cognitive.microsoft.com/docs/services/5adf991815e1060e6355ad44/operations/56f91f2e778daf14a499e1fa)
