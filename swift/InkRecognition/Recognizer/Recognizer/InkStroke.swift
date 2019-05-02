
import Foundation

@objc
enum StrokeKind : Int {
    case DRAWING,
    WRITING,
    UNKNOWN
}

@objc
class InkStroke : NSObject, Encodable {
    var language: String = "en-US"
    var id: Int!
    var kind: StrokeKind
    var inkPoints = [InkPoint]()
    var points : String = ""
    static var num :Int = 0
    var strKind : String = "unknown"
    
    
    enum CodingKeys : String, CodingKey {
        case language
        case id
        case points
        case strKind
    }
    
    @objc
    init( language: String, kind: StrokeKind = StrokeKind.UNKNOWN) {
        id = InkStroke.getNextNumber()
        self.language = language
        self.kind = kind
        switch kind {
        case StrokeKind.DRAWING:
            strKind = "inkDrawing"
        case StrokeKind.WRITING:
            strKind = "inkWriting"
        default:
            print("kind is unknown")
            break
        }
    }
    
    @objc
    func addPoint(point: InkPoint) {
        inkPoints.append(point)
        if points == "" {
            points.append(String(point.x) + "," + String(point.y))
        } else {
            points.append("," + String(point.x) + "," + String(point.y))
        }
    }
    
    @objc
    func removePoint(index: Int) {
        inkPoints.remove(at: index)
    }
    
    func encode(to encoder: Encoder) throws {
        var container = encoder.container(keyedBy: CodingKeys.self)
        try container.encode(language, forKey: .language)
        try container.encode(id, forKey: .id)
        try container.encode(points, forKey: .points)
        try container.encode(strKind, forKey: .strKind)
        
    }
    
    static func getNextNumber() -> Int {
        num += 1
        return num
    }
}


