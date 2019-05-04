
import Foundation
import UIKit
@objc
public enum InkRecognitionUnitCategory: Int {
    case unknown,
    inkWord,
    inkDrawing,
    inkBullet,
    listItem,
    paragraph,
    line,
    writingRegion
}

@objc
class InkBoundingRectangle : NSObject, Decodable {
    var topX : CGFloat = 0
    var topY : CGFloat = 0
    var width : CGFloat = 0
    var height : CGFloat = 0
    
    init(x: Float, y: Float, width: Float, height: Float) {
        self.topX = InkPoint.millimeterToCGFloat(mmPosition: x)
        self.topY = InkPoint.millimeterToCGFloat(mmPosition: y)
        self.width = InkPoint.millimeterToCGFloat(mmPosition: width)
        self.height = InkPoint.millimeterToCGFloat(mmPosition: height)
    }
}

@objc
class InkRecognitionUnit  : NSObject {
    private var categoryString : String
    private var boundingRectangle : InkBoundingRectangle!
    private var rotatedBoundingRectangle = [CGPoint]()
    private var childIds = [Int]();
    private var parentId = -1
    private var strokeIds : [Int]
    public var id : Int = 0
    private var dotsPerInch : Float = 0.0
    private var result: InkRoot!
    
    @objc
    public var category : InkRecognitionUnitCategory {
        get {
            var recognitionCategory: InkRecognitionUnitCategory
            switch (self.categoryString) {
            case "inkWord":
                recognitionCategory = InkRecognitionUnitCategory.inkWord
            case "inkDrawing":
                recognitionCategory = InkRecognitionUnitCategory.inkDrawing
            case "inkBullet":
                recognitionCategory = InkRecognitionUnitCategory.inkBullet
            case "line":
                recognitionCategory = InkRecognitionUnitCategory.line
            case "listItem":
                recognitionCategory = InkRecognitionUnitCategory.listItem
            case "paragraph":
                recognitionCategory = InkRecognitionUnitCategory.paragraph
            case "writingRegion":
                recognitionCategory = InkRecognitionUnitCategory.writingRegion
            default:
                recognitionCategory = InkRecognitionUnitCategory.unknown
            }
            return recognitionCategory;
        }
    }
    
    @objc
    public var children : [InkRecognitionUnit] {
        return result.getNodes(ids: childIds)
    }
    
    @objc
    public var parent: InkRecognitionUnit {
        return result.getNode(id: parentId)
    }
    
    @objc
    public var boundingBox: InkBoundingRectangle {
        return self.boundingRectangle
    }
    
    @objc
    public var rotatedBoundingBox: [CGPoint] {
        return self.rotatedBoundingRectangle
    }
    
    @objc
    init(json:[String: Any]) {
        self.id = json["id"] as! Int
        self.parentId = json["parentId"] as! Int
        
        if let idsForChildren = json["childIds"] as? [Int] {
            self.childIds = idsForChildren
        }
        self.categoryString = json["category"] as! String
        self.strokeIds = json["strokeIds"] as! [Int]
        
        let jsonBoundingRect = json["boundingRectangle"] as! [String: Any]
        self.boundingRectangle = InkBoundingRectangle(x: jsonBoundingRect["topX"] as! Float, y: jsonBoundingRect["topY"] as!Float,width: jsonBoundingRect["width"] as! Float,height: jsonBoundingRect["height"] as! Float)
        
        let jsonRotatedRectPoints = json["rotatedBoundingRectangle"] as! [[String: Any]]
        
        for point in jsonRotatedRectPoints {
            let xValue = point["x"] as! Float
            let yValue = point["y"] as! Float
            let pointX = InkPoint.millimeterToCGFloat(mmPosition: xValue)
            let pointY = InkPoint.millimeterToCGFloat(mmPosition: yValue)
            self.rotatedBoundingRectangle.append(CGPoint(x: pointX, y: pointY))
        }
    }
}
