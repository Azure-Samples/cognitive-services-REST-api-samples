

import Foundation

@objc
class InkWord : InkRecognitionUnit {
    
    var alternates = [[String: Any]]()
    var text:String!
    
    @objc
    override init(json : [String: Any]) {
        self.text = json["recognizedText"] as? String ?? ""
        self.alternates = json["alternates"] as! [[String: Any]] 
        super.init(json:json)
    }
    
    @objc
    func getAlternates() -> [String] {
        var wordAlternates = [String]()
        for jsonAlternate in self.alternates {
            let wordAlternate = jsonAlternate["recognizedString"] as? String ?? ""
            wordAlternates.append(wordAlternate)
        }
        return wordAlternates
    }
}
