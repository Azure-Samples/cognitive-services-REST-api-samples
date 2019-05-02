
import UIKit

class ViewController: UIViewController {

    @IBOutlet weak var recognitionResult: UILabel!
    override func viewDidLoad() {
        self.recognitionResult.text = ""
        super.viewDidLoad()        
    }
}

