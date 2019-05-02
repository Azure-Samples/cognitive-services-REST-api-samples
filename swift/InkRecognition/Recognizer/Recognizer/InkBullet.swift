
import Foundation

@objc
class InkBullet : InkRecognitionUnit {
    
    var text:String!
    
    @objc
    override init(json : [String:Any]) {
        self.text = json["recognizedText"] as? String ?? ""
        super.init(json:json)
    }
}
