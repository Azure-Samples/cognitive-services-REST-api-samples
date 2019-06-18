package CognitiveServices.Ink.Recognition;

import android.util.DisplayMetrics;

import org.json.JSONException;

class InkWritingRegion extends InkRecognitionUnit {
    InkWritingRegion(String paragraphJSON, DisplayMetrics metrics, InkRoot result) throws JSONException {
        super(paragraphJSON, metrics, result);
    }
}
