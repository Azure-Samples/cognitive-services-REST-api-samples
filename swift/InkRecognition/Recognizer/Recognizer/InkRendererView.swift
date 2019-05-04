
import Foundation
import UIKit

class InkRendererView : UIView {
    
    var lines : [Line] = []
    var lastPoint: CGPoint!
    var swiped = false
    var color = UIColor(red: 0.30, green: 0.40, blue: 0.0, alpha: 1.0)
    var penColor : CGColor!
    var inkRecognizer : InkRecognizer
    var inkStroke : InkStroke? = InkStroke(language: "en-US")
    var timer : Timer!
    
    required init(coder aDecoder : NSCoder) {
        self.inkRecognizer = InkRecognizer(url: "https://api.cognitive.microsoft.com/inkrecognizer/v1.0-preview/recognize", appKey: "<SUBSCRIPTION KEY HERE>")
        super.init(coder: aDecoder)!
        penColor = color.cgColor
    }
    
    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        guard let touch = touches.first else {
            return
        }
        if let timer = timer {
            timer.invalidate()
        }
        swiped = false
        lastPoint = touch.location(in: self)
        if inkStroke == nil {
            inkStroke = InkStroke(language: "en-US")
        }
        inkStroke!.addPoint(point: InkPoint(point: lastPoint))
        
    }
    
    func parentViewController() -> UIViewController {
        var responder: UIResponder? = self
        while responder != nil && !(responder is UIViewController) {
            responder = responder?.next
        }
        return responder as! UIViewController
    }
    
    func displayResults(words: String) {
        let controller = parentViewController()
        let viewController = controller as! ViewController
        viewController.recognitionResult.lineBreakMode = NSLineBreakMode.byWordWrapping
        viewController.recognitionResult.numberOfLines = 0;
        viewController.recognitionResult.text = words
    }
    
    override func touchesMoved(_ touches: Set<UITouch>, with event: UIEvent?) {
        guard let touch = touches.first else {
            return
        }
        swiped = true
        let currentPoint = touch.location(in: self)
        lines.append(Line(start: lastPoint, end: currentPoint))
        lastPoint = currentPoint
        inkStroke!.addPoint(point: InkPoint(point: lastPoint))
        self.setNeedsDisplay()
    }
    
    override func touchesEnded(_ touches: Set<UITouch>, with event: UIEvent?) {
        guard let touch = touches.first else { return }
        let currentPoint = touch.location(in: self)
        inkStroke!.addPoint(point: InkPoint(point: currentPoint))
        inkRecognizer.addStroke(stroke: inkStroke!)
        inkStroke = nil
        //Once a stroke is collected, recognition is triggered after 2 seconds of inactivity.
        timer = Timer.scheduledTimer(withTimeInterval: 2.0, repeats: false) { [weak self]
            timer in
            self?.inkRecognizer.recognize(view:  self!)
        }
    }
    
    override func draw(_ rect: CGRect) {
        let context : CGContext = UIGraphicsGetCurrentContext()!
        context.beginPath()
        for line in lines {
            context.move(to: line.start)
            context.addLine(to: line.end)
        }
        context.setLineCap(CGLineCap.round)
        context.setStrokeColor(penColor)
        context.setLineWidth(3)
        context.strokePath()
    }
}
