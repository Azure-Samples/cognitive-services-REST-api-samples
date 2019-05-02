
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
    var uiVIew: InkRenderer!
    
    @objc
    init (url: String, appKey: String) {
        self.appKey = appKey
        self.url = url
    }
    
    @objc
    public func setMetrics() {
      //see what needs to be done with ths
    }
    
    @objc
    public func recognize (view: UIView) {
        self.uiVIew = view as? InkRenderer
        analyzeInk();
    }
    
    private func displayResults() {
        var recognizedWords  = String()
        if inkRoot.resultStatus == RecognitionResultStatus.UPDATED || inkRoot.resultStatus == RecognitionResultStatus.UNCHANGED {
            let words = inkRoot.getWords()
            
            for word in words {
                
                recognizedWords.append(word)
                recognizedWords.append(" ")
            }
            
            let shapes = inkRoot.getDrawings()
            recognizedWords.append(" \r")
            recognizedWords.append("Recognized Shape: \r")
            for shape in shapes {
                if  shape.shapeName != nil {
                    recognizedWords.append(shape.shapeName)
                    recognizedWords.append("\r")
                }
            }
        } else if inkRoot.resultStatus == RecognitionResultStatus.FAILED {
            recognizedWords.append(inkRoot.errorStore.toString())
        } else {
            return
        }
        self.uiVIew.displayResults(words: recognizedWords)
    }    
    
    @objc
    public func addStroke(stroke: InkStroke) {
        strokes.append(stroke)
        strokeIdStore[stroke.id] = strokes.count-1;
    }
    
    @objc
    public func buildResults(jsonData: [String: Any], statusCode: Int) {
        inkRoot = InkRoot(jsonString: jsonData, statusCode: statusCode)
    }
    
    @objc
    public func removeStroke(id: Int) {
        let index: Int = strokeIdStore[id] ?? 0
        
        strokes.remove(at: index)
    }
    
    @objc
    public func getJSONStrokes() -> String {
        let jsonStrokes = JSONEncoder()
        do {
            let inputStrokes = try jsonStrokes.encode(InputInkStrokes(language:"en-US", unit:"mm"))
            var strJSONStrokes = try JSONSerialization.jsonObject(with: inputStrokes, options: .mutableContainers) as! [String:Any]
            var strokesJSON = [[String:Any]]()
            let jsonEncoder = JSONEncoder()
            for stroke in strokes {
                do {
                    let oneStroke = try JSONSerialization.jsonObject(with:jsonEncoder.encode(stroke), options: .mutableContainers) as! [String: Any]
                    strokesJSON.append(oneStroke)
                } catch {
                    
                }
            }
            strJSONStrokes["strokes"] = strokesJSON
            let data = try JSONSerialization.data(withJSONObject: strJSONStrokes)
            return String(data: data, encoding: String.Encoding.utf8) ?? ""
        }
        catch {
            
        }
        return "";
    }
    
    private func strokeKindToString(kind: StrokeKind) -> String {
        var strokeKind : String = "Unknown"
        switch kind {
        case StrokeKind.DRAWING:
            strokeKind = "inkDrawing"
        case StrokeKind.WRITING:
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
            
            do {
            guard let json = try JSONSerialization.jsonObject(with: data!, options: .mutableContainers) as? [String:Any] else {return}
                print(json)
                self.buildResults(jsonData: json, statusCode: response.statusCode)
                DispatchQueue.main.async { [weak self] in
                    self?.displayResults()
                }
            } catch _ {
                
            }
        }
        dataTask.resume()
        
    }
}
