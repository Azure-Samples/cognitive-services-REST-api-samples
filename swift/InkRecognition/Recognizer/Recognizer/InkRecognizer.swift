
import Foundation
import UIKit

struct InputInkStrokes: Encodable {
    var language = "en-US"
    var unit = "mm"
    
    init(language: String, unit: String) {
        self.language = language
        self.unit = unit
    }
}

@objc
class InkRecognizer : NSObject {
    
    var strokeIdStore = [Int: Int]()
    var strokes = [InkStroke]()
    var appKey: String = ""
    var url:String = ""
    var inkRoot: InkRoot!
    weak var inkView: InkRendererView!
    
    @objc
    init (url: String, appKey: String) {
        self.appKey = appKey
        self.url = url
    }
    
    @objc
    public func setMetrics() {
        //These values are for the iphone XR. You can have a data structure
        //to hold the values for the target devices you want to support
        
    }
    
    @objc
    public func recognize (view: InkRendererView) {
        self.inkView = view
        analyzeInk();
    }
    
    private func displayResults() {
        var recognizedWords  = String()
        
        if inkRoot.resultStatus == RecognitionResultStatus.updated || inkRoot.resultStatus == RecognitionResultStatus.unchanged {
            let words = inkRoot.getWords()
            
            for word in words {
                
                recognizedWords.append(word)
                recognizedWords.append(" ")
            }
            
            let shapes = inkRoot.getDrawings()
            recognizedWords.append(" \n")
            recognizedWords.append("Recognized Shape: \n")
            for shape in shapes {
                if  shape.shapeName != nil {
                    recognizedWords.append(shape.shapeName)
                    recognizedWords.append("\n")
                }
            }
        } else if inkRoot.resultStatus == RecognitionResultStatus.failed {
            recognizedWords.append(inkRoot.errorStore.toString())
        } else {
            return
        }
        self.inkView.displayResults(words: recognizedWords)
    }
    
    @objc
    public func addStroke(stroke: InkStroke) {
        strokes.append(stroke)
        strokeIdStore[stroke.id] = strokes.count-1;
    }
    
    @objc
    public func createRecognitionUnits(jsonData: [String: Any], statusCode: Int) {
        inkRoot = InkRoot(jsonString: jsonData, statusCode: statusCode)
    }
    
    @objc
    public func getJSONStrokes() -> String {
        //This function uses the JSON encoder to encode the strokes into json taking advantage of the encodable protocol
        let jsonStrokes = JSONEncoder()
        
        guard let inputStrokes = try? jsonStrokes.encode(InputInkStrokes(language:"en-US", unit:"mm")) else { return ""}
        guard var strJSONStrokes = try? JSONSerialization.jsonObject(with: inputStrokes, options: .mutableContainers) as! [String:Any] else {return ""}
        var strokesJSON = [[String:Any]]()
        let jsonEncoder = JSONEncoder()
        for stroke in strokes {
            guard let oneStroke = try? JSONSerialization.jsonObject(with:jsonEncoder.encode(stroke), options: .mutableContainers) as! [String: Any] else {return ""}
            strokesJSON.append(oneStroke)
        }
        strJSONStrokes["strokes"] = strokesJSON
        guard let data = try? JSONSerialization.data(withJSONObject: strJSONStrokes) else {return ""}
        return String(data: data, encoding: String.Encoding.utf8) ?? ""
    }
    
    private func strokeKindToString(kind: StrokeKind) -> String {
        var strokeKind : String = "Unknown"
        switch kind {
        case StrokeKind.drawing:
            strokeKind = "inkDrawing"
        case StrokeKind.writing:
            strokeKind = "inkWriting"
        default:
            strokeKind = "Unknown"
        }
        return strokeKind
    }
    
    @objc
    public func getResults()-> InkRoot {
        return inkRoot
    }
    
    private func analyzeInk() {
        let url = URL(string: self.url)
        var request = URLRequest(url: url! as URL)
        request.setValue("kong", forHTTPHeaderField: "apim-subscription-id")
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")
        request.setValue(self.appKey, forHTTPHeaderField: "Ocp-Apim-Subscription-Key")
        request.httpMethod = "PUT"
        
        let jsonStrokes = getJSONStrokes()
        let jsonPayload = jsonStrokes.data(using: String.Encoding.utf8)
        
        let dataTask = URLSession.shared.uploadTask(with: request, from: jsonPayload) { data, response, error  in
            if let error = error {
                
                print ("error: \(error)")
                return
            }
            guard let response = response as? HTTPURLResponse,
                (200...444).contains(response.statusCode) else {
                    print ("server error")
                    return
            }
            guard let json = try? JSONSerialization.jsonObject(with: data!, options: .mutableContainers) as? [String:Any] else {return}
            if let json = json {
                print(json)
                self.createRecognitionUnits(jsonData: json, statusCode: response.statusCode)
                DispatchQueue.main.async { [weak self] in
                    self?.displayResults()
                }
            }
        }
        dataTask.resume()
    }
}
