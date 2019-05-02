package CognitiveServices.Ink.Recognition;

import java.io.OutputStreamWriter;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import javax.net.ssl.HttpsURLConnection;
import android.os.AsyncTask;
import android.util.DisplayMetrics;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONObject;
import org.json.JSONException;
import java.util.ArrayList;
import android.content.Context;

public class InkRecognizer {

    private final ArrayList<InkStroke> strokes = new ArrayList<>();
    private final String app_key;
    private final String url;
    private InkRoot inkRoot;
    private DisplayMetrics metrics;
    private final boolean displayTree = true;

    private Context getContext() {
        return context;
    }

    private final Context context;

    public void setMetrics(DisplayMetrics metrics) {
        this.metrics = metrics;
    }



    InkRecognizer(String appKey, String destinationUrl, Context context) {
        this.app_key = appKey;
        this.url = destinationUrl;
        this.context = context;
    }

    public void Recognize() {
        //only attempt to analyze if we have strokes
        if (strokes.size() != 0) {
            new RecognitionRESTTask().execute(this);
        }
    }

    public void addStroke(InkStroke stroke) {
        strokes.add(stroke);
    }

    private InkRoot getRecognitionRoot()
    {
        return inkRoot;
    }


    private void buildResult(String jSONData, int httpResponseCode) {
        this.inkRoot = new InkRoot(jSONData, metrics, httpResponseCode);
    }

    private String getJSONStrokes() throws JSONException {
        JSONObject jsonAnalysisRequest = new JSONObject();
        jsonAnalysisRequest.put("unit", "mm");
        jsonAnalysisRequest.put("language", "en-US");

        JSONArray jsonStrokes = new JSONArray();
        for (int i=0 ; i<strokes.size(); i++) {
            InkStroke stroke = strokes.get(i);
            JSONObject jsonStroke = new JSONObject();
            jsonStroke.put("id", stroke.strokeId);
            jsonStroke.put("language", stroke.language);
            if (stroke.kind != StrokeKind.UNKNOWN) {
                jsonStroke.put("kind", strokeKindToString(stroke.kind));
            }
            StringBuilder points = new StringBuilder();
            for (int r=0;
                    r <  stroke.inkPoints.size();
                    r++) {
                points.append(stroke.inkPoints.get(r).x);
                points.append(",");
                points.append(stroke.inkPoints.get(r).y);
                if (r <stroke.inkPoints.size()-1) {
                    points.append(",");
                }
            }
            jsonStroke.put("points", points.toString());
            jsonStrokes.put(jsonStroke);
        }
        jsonAnalysisRequest.put("strokes", jsonStrokes);
        return jsonAnalysisRequest.toString();
    }

    private String strokeKindToString(StrokeKind kind) {
        String strokeKind = "UnKnown";
        switch(kind) {
            case DRAWING:
                strokeKind = "inkDrawing";
                break;
            case WRITING:
                strokeKind = "inkWriting";
                break;
        }
        return strokeKind;
    }

    private static class RecognitionRESTTask extends AsyncTask<InkRecognizer, Integer, InkRecognizer> {

        @SuppressWarnings("SpellCheckingInspection")
        @Override
        protected InkRecognizer doInBackground(InkRecognizer... params) {

            int responseCode;

            try {
                URL url = new URL(params[0].url);
                HttpsURLConnection restConnection = (HttpsURLConnection) url.openConnection();
                restConnection.setRequestProperty("Content-Type","application/json");
                restConnection.setRequestProperty("Ocp-Apim-Subscription-Key",params[0].app_key);
                restConnection.setRequestMethod("PUT");
                restConnection.setDoOutput(true);
                OutputStreamWriter jsonStrokesWriter = new OutputStreamWriter(restConnection.getOutputStream());
                jsonStrokesWriter.write(params[0].getJSONStrokes());
                jsonStrokesWriter.flush();
                jsonStrokesWriter.close();
                responseCode = restConnection.getResponseCode();
                // read the output from the server
                if (responseCode == HttpURLConnection.HTTP_OK ||
                        responseCode == HttpURLConnection.HTTP_BAD_REQUEST) {
                    InputStreamReader streamReader = new InputStreamReader(restConnection.getInputStream());
                    BufferedReader reader = new BufferedReader(streamReader);
                    StringBuilder stringBuilder = new StringBuilder();

                    String line;
                    while ((line = reader.readLine()) != null) {
                        stringBuilder.append(line).append("\n");
                    }

                    params[0].buildResult(stringBuilder.toString(), responseCode);

                }
                else {

                    params[0].buildResult("Error Occurred", responseCode);
                }
            }
            catch (Exception e) {
                e.printStackTrace();
                params[0].buildResult("Error Occurred", 0);
            }
            return params[0];
        }

        @Override
        protected void onPostExecute(InkRecognizer analyzer) {
            StringBuilder recognisedWords = new StringBuilder();
            InkRoot inkRoot = analyzer.getRecognitionRoot();
            if (inkRoot.getResultStatus() == RecognitionResultStatus.UPDATED) {
                ArrayList<InkWord> words = inkRoot.getInkWords();

                for (int i = 0; i < words.size(); i++) {
                    recognisedWords.append(words.get(i).getText());
                    recognisedWords.append(" ");
                }
                ArrayList<InkDrawing> drawings = inkRoot.getInkDrawings();
                recognisedWords.append("\r\nRecognized Shapes:\r\n");
                for (int i = 0; i < drawings.size(); i++) {
                    recognisedWords.append(drawings.get(i).getShape().toString()).append("\r\n");
                }
                if(analyzer.displayTree) {
                    InkRecognitionDetailsLogger.displayAnalysisTree(inkRoot);
                }
            } else {
                recognisedWords.append(inkRoot.getRecognitionError().toString());
            }
            Toast toast = Toast.makeText(analyzer.getContext(), recognisedWords.toString(), Toast.LENGTH_LONG);
            toast.show();
        }
    }
}
