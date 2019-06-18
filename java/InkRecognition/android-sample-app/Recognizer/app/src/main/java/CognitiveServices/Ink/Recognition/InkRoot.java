package CognitiveServices.Ink.Recognition;

import android.util.DisplayMetrics;

import org.json.JSONArray;
import org.json.JSONObject;

import java.net.HttpURLConnection;
import java.security.InvalidParameterException;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Set;

class InkRoot {
    //Using different collections to isolate each type for type specific queries.
    private final Hashtable<Integer, InkRecognitionUnit> recognizedContainers = new Hashtable<>();
    private final ArrayList<InkWord> wordList = new ArrayList<>();
    private final Hashtable<Integer, InkRecognitionUnit> recognizedDrawings = new Hashtable<>();
    private final Hashtable<Integer, InkRecognitionUnit> recognizedUnits = new Hashtable<>();

    public InkRecognitionError getRecognitionError() {
        return recognitionError;
    }

    private InkRecognitionError recognitionError;

    public RecognitionResultStatus getResultStatus() {
        return resultStatus;
    }

    private RecognitionResultStatus resultStatus = RecognitionResultStatus.UNCHANGED;

    InkRoot(String json, DisplayMetrics metrics, int httpResponseCode)
    {
        try {
            JSONObject jsonResponse = new JSONObject(json);

            if (httpResponseCode == HttpURLConnection.HTTP_OK) {
                JSONArray jsonRecognitionUnits = jsonResponse.getJSONArray("recognitionUnits");
                for (int i = 0;
                     i < jsonRecognitionUnits.length() &&
                     resultStatus != RecognitionResultStatus.FAILED;
                     i++) {

                    String category = jsonRecognitionUnits.getJSONObject(i).getString("category");
                    String unitJSON = jsonRecognitionUnits.getJSONObject(i).toString();
                    int id = jsonRecognitionUnits.getJSONObject(i).getInt("id");
                    //Instantiate the recognition units
                    switch (category) {
                        case "inkWord":
                            InkWord word = new InkWord(unitJSON, metrics, this);
                            recognizedUnits.put(id, word);
                            wordList.add(word);
                            break;
                        case "inkBullet":
                            InkBullet bullet = new InkBullet(unitJSON, metrics, this);
                            recognizedUnits.put(id, bullet);
                            break;
                        case "inkDrawing":
                            InkDrawing drawing = new InkDrawing(unitJSON, metrics, this);
                            recognizedDrawings.put(id, drawing);
                            recognizedUnits.put(id, drawing);
                            break;
                        case "line":
                            InkLine line = new InkLine(unitJSON, metrics, this);
                            recognizedContainers.put(id, line);
                            recognizedUnits.put(id, line);
                            break;
                        case "listItem":
                            InkListItem listItem = new InkListItem(unitJSON, metrics, this);
                            recognizedContainers.put(id, listItem);
                            recognizedUnits.put(id, listItem);
                            break;
                        case "paragraph":
                            InkParagraph paragraph = new InkParagraph(unitJSON, metrics, this);
                            recognizedContainers.put(id, paragraph);
                            recognizedUnits.put(id, paragraph);
                            break;
                        case "writingRegion":
                            InkWritingRegion writingRegion = new InkWritingRegion(unitJSON, metrics, this);
                            recognizedContainers.put(id, writingRegion);
                            recognizedUnits.put(id, writingRegion);
                            break;
                        default:
                            recognitionError = new InkRecognitionError("unknown unit");
                            resultStatus = RecognitionResultStatus.FAILED;
                            break;
                    }
                }
                resultStatus = RecognitionResultStatus.UPDATED;
            } else {
                //build error report
                if (httpResponseCode != 0) {
                    recognitionError = new InkRecognitionError(jsonResponse);
                } else {
                    //build default error
                    recognitionError = new InkRecognitionError();
                }
                resultStatus = RecognitionResultStatus.FAILED;
            }
        }
        catch (Exception e) {
            recognitionError = new InkRecognitionError(e.getMessage());
        }
    }

    public ArrayList<InkWord> getInkWords()
    {
        return wordList;
    }

    public ArrayList<InkParagraph> getParagraphs()  {
        ArrayList<InkParagraph> paragraphs = new ArrayList<>();
        for (Integer key : recognizedContainers.keySet()) {
            InkRecognitionUnit entryValue = recognizedContainers.get(key);
            if (entryValue.getCategory() == InkRecognitionUnitCategory.INK_PARAGRAPH) {
                paragraphs.add((InkParagraph)entryValue);
            }
        }
        return paragraphs;
    }

    public ArrayList<InkDrawing> getInkDrawings() {
        ArrayList<InkDrawing> drawings = new ArrayList<>();
        Set<Integer> keys = recognizedDrawings.keySet();
        for (Integer key: keys) {
            drawings.add((InkDrawing)recognizedDrawings.get(key));
        }
        return drawings;
    }

    public ArrayList<InkRecognitionUnit> getRecognitionUnits(ArrayList<Integer> ids) {
        ArrayList<InkRecognitionUnit> nodes = new ArrayList<>();
        for (int i=0;
             i < ids.size();
             i++) {
            InkRecognitionUnit unit = recognizedUnits.get(ids.get(i));
            if (unit != null) {
                nodes.add(unit);
            }
        }
        return nodes;
    }

    public InkRecognitionUnit getRecognitionUnit(int id) throws InvalidParameterException {
        InkRecognitionUnit node = recognizedUnits.get(id);
        if (node == null) {
            throw new InvalidParameterException("unknown id");
        }
        return node;
    }
}
