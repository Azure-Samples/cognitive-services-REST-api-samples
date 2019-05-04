package CognitiveServices.Ink.Recognition;

import android.support.annotation.NonNull;

import java.util.ArrayList;
import org.json.JSONArray;
import org.json.JSONObject;
import org.json.JSONException;

public class InkRecognitionError {

    private String errorCode;
    private String message;
    private String target;
    private final ArrayList<InkRecognitionError> details = new ArrayList<>();

    InkRecognitionError() {
        errorCode = "Network Error";
        message = "Exception Thrown During Network Connection";
        target = "Network";
    }

    InkRecognitionError(String processingError) {
        errorCode = "Data Processing Error";
        message = processingError;
        target = "Network";
    }

    InkRecognitionError(JSONObject jsonResponse) {
        try {
            if (jsonResponse.has("code")) {
                errorCode = jsonResponse.getString("code");
                message = jsonResponse.getString("message");
                target = jsonResponse.getString("target");
                JSONArray jsonDetails = jsonResponse.getJSONArray("details");
                for (int i = 0;
                     i < jsonDetails.length();
                     i++) {
                    details.add(new InkRecognitionError(jsonDetails.getJSONObject(i)));
                }
            } else {
                errorCode = "RequestError";
                message = jsonResponse.getJSONObject("error").getString("message");
                target = "Request";
            }
        }
        catch (JSONException e) {
            errorCode = "JSON Error";
            message = "Exception Thrown During JSON Error Handling";
            target = "JSON";
        }
    }

    @NonNull
    public String toString() {
        return "Code: " + errorCode +
               "Message: " + message +
               "Target: " + target +
               "Details:\r" + getErrorDetails();
    }

    private String getErrorDetails() {
        StringBuilder errorDetails = new StringBuilder();
        for (InkRecognitionError detailUnit : details) {
            errorDetails.append(detailUnit.toString());
        }
        return errorDetails.toString();
    }
}
