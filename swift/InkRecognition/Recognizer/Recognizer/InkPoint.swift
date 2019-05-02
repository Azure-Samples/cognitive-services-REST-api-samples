

import Foundation
import UIKit

@objc
class InkPoint : NSObject, Decodable {
    var x : Double = 0;
    var y : Double = 0;
    
    @objc
    init(x: Double, y: Double) {
        self.x = x
        self.y = y
    }
    
    @objc
    init(point: CGPoint) {
        self.x = Double(point.x)
        self.y = Double(point.y)
    }
}
