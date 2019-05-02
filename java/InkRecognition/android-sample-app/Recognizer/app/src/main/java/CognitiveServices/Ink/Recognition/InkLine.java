package CognitiveServices.Ink.Recognition;

import android.util.DisplayMetrics;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import java.util.ArrayList;

class InkLine extends InkRecognitionUnit {

    private float indentLevel = 0.0f;
    private String text;
    private final ArrayList<String> alternates = new ArrayList<>();

    public float getIndentLevel() {
        return indentLevel;
    }

    public String getText() {
        return text;
    }

    public ArrayList<String> getAlternates() {
        return alternates;
    }

    InkLine(String lineJSON, DisplayMetrics metrics, InkRoot result) throws JSONException {
        super(lineJSON, metrics, result);
        JSONObject jsonLine = new JSONObject(lineJSON);
        JSONArray jsonAlternates = jsonLine.getJSONArray("alternates");
        if (jsonLine.has("indentLevel")) {
            this.indentLevel = jsonLine.getInt("indentLevel");
        }
        this.text = jsonLine.getString("recognizedText");

        for (int i=0;
             i < jsonAlternates.length();
             i++) {
            alternates.add(jsonAlternates.getJSONObject(i).getString("recognizedString"));

        }

    }

}
