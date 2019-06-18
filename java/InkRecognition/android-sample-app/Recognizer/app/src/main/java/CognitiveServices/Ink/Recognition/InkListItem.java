package CognitiveServices.Ink.Recognition;

import android.util.DisplayMetrics;

import org.json.JSONException;

class InkListItem extends InkRecognitionUnit {

    InkListItem(String listJSON, DisplayMetrics metrics, InkRoot result) throws JSONException {
        super(listJSON, metrics,result);
    }
}
