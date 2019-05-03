package CognitiveServices.Ink.Recognition;

import android.util.DisplayMetrics;
import org.json.JSONException;
import org.json.JSONObject;

class InkBullet extends InkRecognitionUnit {

    public String getText() {
        return text;
    }

    private String text;

    InkBullet(String bulletJSON, DisplayMetrics metrics, InkRoot result) throws JSONException {
        super(bulletJSON, metrics, result);
        JSONObject jsonBullet = new JSONObject(bulletJSON);
        this.text = jsonBullet.getString("recognizedText");
    }
}
