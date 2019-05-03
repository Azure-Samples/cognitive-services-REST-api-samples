package CognitiveServices.Ink.Recognition;

import android.util.DisplayMetrics;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

class InkWord extends InkRecognitionUnit {

    public ArrayList<String> getAlternates() {
        return alternates;
    }

    private final ArrayList<String> alternates = new ArrayList<>();

    public String getText() {
        return text;
    }

    private String text;

    InkWord(String wordJSON, DisplayMetrics metrics, InkRoot result) throws JSONException {
        super(wordJSON, metrics, result);
        JSONObject jsonWord = new JSONObject(wordJSON);
        JSONArray jsonAlternates = jsonWord.getJSONArray("alternates");

        for (int i=0;
             i < jsonAlternates.length();
             i++) {
            alternates.add(jsonAlternates.getJSONObject(i).getString("recognizedString"));
        }

        this.text = jsonWord.getString("recognizedText");
    }
}
