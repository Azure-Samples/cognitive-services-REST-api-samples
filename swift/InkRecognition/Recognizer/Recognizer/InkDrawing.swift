

import Foundation

@objc
public enum Shape  : Int {
    case drawing,
    square,
    rectangle,
    circle,
    ellipse,
    triangle,
    isoscelesTriangle,
    equilateralTriangle,
    rightTriangle,
    quadrilateral,
    diamond,
    trapezoid,
    parallelogram,
    pentagon,
    hexagon,
    blockArrow,
    heart,
    starSimple,
    starcrossed,
    cloud,
    line,
    curve,
    polyline
}

@objc
class InkDrawing : InkRecognitionUnit {
    
    private var center: InkPoint!
    private var confidence: Float!
    var shape:Shape!
    var shapeName : String!
    var rotationAngle: Float!
    var alternates = [Shape]()
    var points = [InkPoint]()
    let supportedShapes = ["drawing": Shape.drawing,
                           "circle": Shape.circle,
                           "square": Shape.square,
                           "rectangle": Shape.rectangle,
                           "triangle": Shape.triangle,
                           "ellipse": Shape.ellipse,
                           "isoscelesTriangle": Shape.isoscelesTriangle,
                           "equilateralTriangle": Shape.equilateralTriangle,
                           "rightTriangle": Shape.rightTriangle,
                           "quadrilateral": Shape.quadrilateral,
                           "diamond": Shape.diamond,
                           "trapezoid": Shape.trapezoid,
                           "parallelogram": Shape.parallelogram,
                           "pentagon": Shape.pentagon,
                           "hexagon": Shape.hexagon,
                           "blockArrow": Shape.blockArrow,
                           "heart": Shape.heart,
                           "starSimple": Shape.starSimple,
                           "starCrossed": Shape.starcrossed,
                           "cloud": Shape.cloud,
                           "line": Shape.line,
                           "curve": Shape.curve,
                           "polyline": Shape.polyline]
    
    @objc
    override init(json : [String: Any]) {
        super.init(json: json)
        
        let jsonCenter = json["center"] as! [String: Any]
        let xValue = jsonCenter["x"] as! Float
        let yValue = jsonCenter["y"] as! Float
        self.center = InkPoint(x: xValue, y: yValue)
        //extract the "beautified" points. These can be used to draw a more strucutured version of the shape.
        if let shapePoints = json["points"] as? [[String:Any]] {
            for shapePoint in shapePoints {
                let x = shapePoint["x"] as! Float
                let y = shapePoint["y"] as! Float
                self.points.append(InkPoint(x: x, y: y));
            }
        }
        self.rotationAngle = json["rotationAngle"] as? Float ?? 0.0
        self.shapeName = (json["recognizedObject"] as! String)
        self.shape = stringToShape(shape: shapeName)
        self.confidence = json["confidence"] as? Float
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
        return Shape.drawing
    }
}
