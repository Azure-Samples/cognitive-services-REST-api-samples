

import Foundation

@objc
public enum Shape  : Int {
    case DRAWING,
    SQUARE,
    RECTANGLE,
    CIRCLE,
    ELLIPSE,
    TRIANGLE,
    ISOSCELESTRIANGLE,
    EQUILATERALTRIANGLE,
    RIGHTTRIANGLE,
    QUADRILATERAL,
    DIAMOND,
    TRAPEZOID,
    PARALLELOGRAM,
    PENTAGON,
    HEXAGON,
    BLOCKARROW,
    HEART,
    STARSIMPLE,
    STARCROSSED,
    CLOUD,
    LINE,
    CURVE,
    POLYLINE
}

@objc
class InkDrawing : InkRecognitionUnit {
    
    private var center: InkPoint!
    private var confidence: Double!
    var shape:Shape!
    var shapeName : String!
    var rotationAngle: Double!
    var alternates = [Shape]()
    var points = [InkPoint]()
    let supportedShapes = ["drawing": Shape.DRAWING,
                          "circle": Shape.CIRCLE,
                          "square": Shape.SQUARE,
                          "rectangle": Shape.RECTANGLE,
                          "triangle": Shape.TRIANGLE,
                          "ellipse": Shape.ELLIPSE,
                          "isoscelesTriangle": Shape.ISOSCELESTRIANGLE,
                          "equilateralTriangle": Shape.EQUILATERALTRIANGLE,
                          "rightTriangle": Shape.RIGHTTRIANGLE,
                          "quadrilateral": Shape.QUADRILATERAL,
                          "diamond": Shape.DIAMOND,
                          "trapezoid": Shape.TRAPEZOID,
                          "parallelogram": Shape.PARALLELOGRAM,
                          "pentagon": Shape.PENTAGON,
                          "hexagon": Shape.HEXAGON,
                          "blockArrow": Shape.BLOCKARROW,
                          "heart": Shape.HEART,
                          "starSimple": Shape.STARSIMPLE,
                          "starCrossed": Shape.STARCROSSED,
                          "cloud": Shape.CLOUD,
                          "line": Shape.LINE,
                          "curve": Shape.CURVE,
                          "polyline": Shape.POLYLINE]
    
    @objc
    override init(json : [String: Any]) {
        super.init(json: json)
        
        let jsonCenter = json["center"] as? [String: Any]
        let xValue = jsonCenter!["x"] as! Double
        let yValue = jsonCenter!["y"] as! Double
        self.center = InkPoint(x: xValue, y: yValue)
        //extract the "beautified" points. These can be used to draw a more strucutured version of the shape.
        if let shapePoints = json["points"] as? [[String:Any]] {
            for shapePoint in shapePoints {
                let x = shapePoint["x"] as! Double
                let y = shapePoint["y"] as! Double
                self.points.append(InkPoint(x: x, y: y));
            }
        }
        self.rotationAngle = json["rotationAngle"] as? Double ?? 0.0
        self.shapeName = (json["recognizedObject"] as! String) 
        self.shape = stringToShape(shape: shapeName)
        self.confidence = json["confidence"] as? Double
        if let alternates = json["alternates"] as? [[String: Any]] {
            for alternate in alternates {
                let alternateName = alternate["recognizedObject"] as! String
                self.alternates.append(stringToShape(shape: alternateName))
            }
        }
    }
    
    private func stringToShape(shape: String) -> Shape {
        if let concreteShape  = supportedShapes[shape] {
            return concreteShape
        }
        return Shape.DRAWING        
    }
}
