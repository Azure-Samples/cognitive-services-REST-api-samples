

import Foundation

@objc
public enum InkRecognitionUnitCategory: Int {
    case UNKNOWN,
    INKWORD,
    INKDRAWING,
    INKBULLET,
    LISTITEM,
    PARAGRAPH,
    LINE,
    WRITINGREGION
}

@objc
class InkBoundingRectangle : NSObject, Decodable {
    var topX : Double = 0
    var topY : Double = 0
    var width : Double = 0
    var height : Double = 0
    
    init(x: Double, y: Double, width: Double, height: Double) {
        self.topX = x
        self.topY = y
        self.width = width
        self.height = height
    }
}

@objc
class InkRecognitionUnit  : NSObject {
    private var categoryString : String
    private var boundingRectangle : InkBoundingRectangle!
    private var rotatedBoundingRectangle = [InkPoint]()
    private var childIds = [Int]();
    private var parentId = -1
    private var strokeIds : [Int]
    public var id : Int = 0
    private var dotsPerInch : Float = 0.0
    private var result: InkRoot!
    
    @objc
    public var category : InkRecognitionUnitCategory {
        get{
            var recognitionCategory: InkRecognitionUnitCategory
            switch(self.categoryString) {
            case "inkWord":
                recognitionCategory = InkRecognitionUnitCategory.INKWORD
            case "inkDrawing":
                recognitionCategory = InkRecognitionUnitCategory.INKDRAWING
            case "inkBullet":
                recognitionCategory = InkRecognitionUnitCategory.INKBULLET
            case "line":
                recognitionCategory = InkRecognitionUnitCategory.LINE
            case "paragraph":
                recognitionCategory = InkRecognitionUnitCategory.PARAGRAPH
            case "writingRegion":
                recognitionCategory = InkRecognitionUnitCategory.WRITINGREGION
            default:
                recognitionCategory = InkRecognitionUnitCategory.UNKNOWN
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
    public var rotatedBoundingBox :[InkPoint] {
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
        self.boundingRectangle = InkBoundingRectangle(x: jsonBoundingRect["topX"] as! Double, y: jsonBoundingRect["topY"] as!Double,width: jsonBoundingRect["width"] as! Double,height: jsonBoundingRect["height"] as! Double)
        
        let jsonRotatedRectPoints = json["rotatedBoundingRectangle"] as! [[String: Any]]
        
        for point in jsonRotatedRectPoints {
            let xValue = point["x"] as! Double
            let yValue = point["y"] as! Double
            self.rotatedBoundingRectangle.append( InkPoint(x: xValue, y: yValue))
        }
    }
}
