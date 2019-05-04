

import Foundation
import UIKit

@objc
class InkPoint : NSObject, Decodable {
    var x : Float = 0;
    var y : Float = 0;
    
    //Values from the iphone XR. Change to match your target devices with a table of values for each device.
    static let ppi : Float = 326.0
    static let mmPerInch : Float = 25.4
    @objc
    init(x: Float, y: Float) {
        self.x = x
        self.y = y
    }
    
    @objc
    static func millimeterToCGFloat(mmPosition: Float) -> CGFloat {
        return CGFloat(mmPosition/InkPoint.mmPerInch*InkPoint.ppi)
    }
    
    @objc
    init(point: CGPoint) {
        self.x = Float(point.x)/InkPoint.ppi*InkPoint.mmPerInch
        self.y = Float(point.y)/InkPoint.ppi*InkPoint.mmPerInch
    }
}

