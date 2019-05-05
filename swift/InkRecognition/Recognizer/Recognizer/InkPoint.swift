

import Foundation
import UIKit

@objc
class InkPoint: NSObject, Decodable {
    var x: Float = 0;
    var y: Float = 0;
    
    //Value for the iphone XR. Change to match your target devices with a table of values for each device.
    static let iPhoneXRPPI : Float = 326.0
    static let iPhoneXRScalingFactor: Float = 2.0
    static let mmPerInch : Float = 25.4
    @objc
    init(x: Float, y: Float) {
        self.x = x
        self.y = y
    }
    
    @objc
    static func millimeterToCGFloat(mmValue: Float) -> CGFloat {
        return CGFloat(mmValue/InkPoint.mmPerInch*InkPoint.iPhoneXRPPI)
    }
    
    @objc
    init(point: CGPoint) {
        //Scale to pixels and then convert to millimeters
        self.x = Float(point.x)*InkPoint.iPhoneXRScalingFactor/InkPoint.iPhoneXRPPI*InkPoint.mmPerInch
        self.y = Float(point.y)*InkPoint.iPhoneXRScalingFactor/InkPoint.iPhoneXRPPI*InkPoint.mmPerInch
    }
}

