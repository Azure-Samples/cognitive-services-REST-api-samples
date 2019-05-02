
import Foundation
@objc
class InkLine : InkRecognitionUnit {
    
    var alternates = [[String:Any]]()
    var indentLevel = 0.0
    var text:String!
    
    @objc
    override init(json : [String: Any])
    {
        self.text = json["recognizedText"] as? String ?? ""
        if let alternates = json["alternates"] as? [[String: Any]] {
            self.alternates = alternates
        }
        super.init(json:json)
    }
    
    @objc
    func getAlternates() -> [String]
    {
        var wordAlternates = [String]()
        for jsonAlternate in self.alternates {
            let wordAlternate = jsonAlternate["recognizedString"] as? String ?? ""
            wordAlternates.append(wordAlternate)
        }
        return wordAlternates
    }
}
