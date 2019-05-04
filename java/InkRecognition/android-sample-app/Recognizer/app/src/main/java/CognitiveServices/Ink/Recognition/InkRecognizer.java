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
    private InkRoot inkRoot; //the root holds the recognition units reported by the service
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

    private void buildResult(String jsonData, int httpResponseCode) {
        this.inkRoot = new InkRoot(jsonData, metrics, httpResponseCode);
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
            jsonStroke.put("language", stroke.language); //The language is an optional field which can be used when dealing with multi-language apps.
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
        String strokeKind;
        switch(kind) {
            case DRAWING:
                strokeKind = "inkDrawing";
                break;
            case WRITING:
                strokeKind = "inkWriting";
                break;
            default:
                strokeKind = "UnKnown";
                break;
        }
        return strokeKind;
    }

    private static class RecognitionRESTTask extends AsyncTask<InkRecognizer, Integer, InkRecognizer> {

        @SuppressWarnings("SpellCheckingInspection")
        @Override
        protected InkRecognizer doInBackground(InkRecognizer... params) {

            int responseCode;
            InkRecognizer inkRecognizer = params[0];

            try {
                URL url = new URL(inkRecognizer.url);
                HttpsURLConnection restConnection = (HttpsURLConnection) url.openConnection();
                restConnection.setRequestProperty("Content-Type","application/json");
                restConnection.setRequestProperty("Ocp-Apim-Subscription-Key", inkRecognizer.app_key);
                restConnection.setRequestMethod("PUT");
                restConnection.setDoOutput(true);
                OutputStreamWriter jsonStrokesWriter = new OutputStreamWriter(restConnection.getOutputStream());
                jsonStrokesWriter.write(inkRecognizer.getJSONStrokes());
                jsonStrokesWriter.flush();
                jsonStrokesWriter.close();
                responseCode = restConnection.getResponseCode();
                // read the output from the server
                if (responseCode == HttpURLConnection.HTTP_OK ||
                    responseCode == HttpURLConnection.HTTP_BAD_REQUEST) {
                    InputStreamReader streamReader = new InputStreamReader(restConnection.getInputStream());
                    inkRecognizer.buildResult(getResponseString(streamReader), responseCode);
                }
                else {
                    String responseError = "{}";
                    if (responseCode == HttpURLConnection.HTTP_UNAUTHORIZED) {
                        InputStreamReader streamReader = new InputStreamReader(restConnection.getErrorStream());
                        responseError = getResponseString(streamReader);
                    }
                    inkRecognizer.buildResult(responseError, responseCode);
                }
            }
            catch (Exception e) {
                e.printStackTrace();
                inkRecognizer.buildResult("Error Occurred", 0);
            }
            return inkRecognizer;
        }

        private String getResponseString(InputStreamReader streamReader) throws Exception
        {
            BufferedReader reader = new BufferedReader(streamReader);
            StringBuilder responseBody = new StringBuilder();
            String line;
            while ((line = reader.readLine()) != null) {
                responseBody.append(line).append("\n");
            }
            return responseBody.toString();
        }

        @Override
        protected void onPostExecute(InkRecognizer analyzer) {
            StringBuilder recognizedWords = new StringBuilder();
            InkRoot inkRoot = analyzer.getRecognitionRoot();
            if (inkRoot.getResultStatus() == RecognitionResultStatus.UPDATED) {
                ArrayList<InkWord> words = inkRoot.getInkWords();

                for (int i = 0; i < words.size(); i++) {
                    recognizedWords.append(words.get(i).getText());
                    recognizedWords.append(" ");
                }

                ArrayList<InkDrawing> drawings = inkRoot.getInkDrawings();
                recognizedWords.append("\r\nRecognized Shapes:\r\n");
                for (int i = 0; i < drawings.size(); i++) {
                    recognizedWords.append(drawings.get(i).getShape().toString()).append("\r\n");
                }
                if(analyzer.displayTree) {
                    InkRecognitionDetailsLogger.displayAnalysisTree(inkRoot);
                }
            } else {
                recognizedWords.append(inkRoot.getRecognitionError().toString());
            }
            Toast toast = Toast.makeText(analyzer.getContext(), recognizedWords.toString(), Toast.LENGTH_LONG);
            toast.show();
        }
    }
}
