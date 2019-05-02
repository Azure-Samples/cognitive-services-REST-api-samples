

import Foundation

@objc
enum RecognitionResultStatus : Int {
    case UNCHANGED,
    UPDATED,
    FAILED
}

@objc
class InkRecognitionError : NSObject {
    var code : String!
    var message : String!
    var target : String!
    var details = [InkRecognitionError]()

    @objc
    init(jsonString: [String: Any]) {
        code = jsonString["code"] as? String ?? ""
        message = jsonString["message"] as? String ?? ""
        target = jsonString["target"] as? String ?? ""
        if let detailsList = jsonString["details"] as? [[String: Any]] {
            for detailUnit in detailsList {
               details.append(InkRecognitionError(jsonString: detailUnit))
            }
        }
    }
    
    @objc
    public func toString() -> String {
        return String("Code: " + code + "Message: " + message + "Target: " + target + "Details:\r" + getErrorDetails())
    }
    
    @objc
    public func getErrorDetails() -> String {
        var errorDetails = String()
        for detailUnit in details {
            errorDetails.append(detailUnit.toString())
        }
        return errorDetails
    }
}

@objc
class InkRoot : NSObject {
    
    var resultJSON : [String: Any]
    var recognizedContainers = [Int: InkRecognitionUnit]()
    var recognizedWords = [Int: InkWord]()
    var recognizedDrawings = [Int: InkDrawing]()
    var recognitionUnits = [Int: InkRecognitionUnit]()
    var orderedWords : [String] = []
    var errorStore : InkRecognitionError!
    var resultStatus: RecognitionResultStatus = RecognitionResultStatus.UNCHANGED

    @objc
    init(jsonString: [String: Any], statusCode: Int) {
        resultJSON = jsonString
        
        
        if statusCode >= 400 {
            errorStore = InkRecognitionError(jsonString: resultJSON)
            resultStatus = RecognitionResultStatus.FAILED
            return
        }
        
        let jsonUnits = jsonString["recognitionUnits"] as? [[String: Any]]
        for jsonUnit in jsonUnits! {
            let category = jsonUnit["category"] as? String
            let index = jsonUnit["id"] as! Int
            switch category {
            case "inkWord":
                let inkWord = InkWord(json: jsonUnit)
                recognizedWords[index] = inkWord
                recognitionUnits[index] = inkWord
                orderedWords.append(inkWord.text)
            case "inkDrawing":
                let inkShape = InkDrawing(json: jsonUnit)
                recognizedDrawings[index] = inkShape
                recognitionUnits[index] = inkShape
            case "line":
                let inkLine = InkLine(json: jsonUnit)
                recognizedContainers[index] = inkLine
                recognitionUnits[index] = inkLine
            case "inkBullet":
                let inkBullet = InkBullet(json: jsonUnit)
                recognitionUnits[index] = inkBullet
            case "paragraph":
                let inkParagraph = InkParagraph(json: jsonUnit)
                recognizedContainers[index] = inkParagraph
                recognitionUnits[index] = inkParagraph
            case "writingRegion":
                let inkWritingRegion = InkWritingRegion(json: jsonUnit)
                recognizedContainers[index] = inkWritingRegion
                recognitionUnits[index] = inkWritingRegion
            default:
                _ = "";
            }
        }
        resultStatus = RecognitionResultStatus.UNCHANGED        
    }
    
    @objc
    public func getInkWords() -> [InkWord] {
        var words = [InkWord]()
        recognizedWords.values.forEach {value in
            words.append(value)
        }
        return words
    }
    
    @objc
    public func getWords() ->[String] {
        var words = [String]()
        for word in orderedWords {
            words.append(word)
        }
        return words        
    }
    
    @objc
    public func getNodes(ids: [Int]) -> [InkRecognitionUnit] {
        var recognizedUnits = [InkRecognitionUnit]()
        for id in ids {
            recognizedUnits.append(recognitionUnits[id]!)
        }
        return recognizedUnits
    }
    
    @objc
    public func getNode(id: Int) -> InkRecognitionUnit {
        let recognizedUnit = recognitionUnits[id]!
        return recognizedUnit
    }
    
    @objc
    public func getParagraphs() -> [InkParagraph] {
        var paragraphs = [InkParagraph]()
        recognizedContainers.values.forEach {value in
            if let paragraph = value as? InkParagraph {
                paragraphs.append(paragraph)
            }
        }
        return paragraphs
    }
    
    @objc
    public func getDrawings() -> [InkDrawing] {
        var shapes = [InkDrawing]()
        recognizedDrawings.values.forEach {value in
            shapes.append(value)
        }
        return shapes
    }
}
