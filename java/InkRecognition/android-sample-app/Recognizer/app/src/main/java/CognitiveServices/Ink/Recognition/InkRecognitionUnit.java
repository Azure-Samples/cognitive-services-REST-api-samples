package CognitiveServices.Ink.Recognition;

import android.util.DisplayMetrics;


import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

public class InkRecognitionUnit {

    private final ArrayList<Integer> strokeIds = new ArrayList<>();
    private int id;
    private InkRecognitionUnitCategory category;
    private final ArrayList<Integer> childIds = new ArrayList<>();
    private int parentId;
    private Rectangle boundingBox;
    private ArrayList<InkPoint> rotatedBoundingBox;
    private final float DOT_PER_INCH;
    private final InkRoot result;
    public static final float INCH_TO_MM = 25.4f;

    public InkRecognitionUnitCategory getCategory() {
        return category;
    }

    public ArrayList<Integer> getStrokeIds() {
        return strokeIds;
    }

    public int getId() {
        return id;
    }

    public ArrayList<InkRecognitionUnit> getChildren() {

        ArrayList<InkRecognitionUnit> childrenNodes;

        try {
            childrenNodes = result.getRecognitionUnits(childIds);
        }
        catch (Exception e) {
            System.out.print("Data Processing Error: " + e.getMessage());
            throw e;
        }
        return childrenNodes;
    }

    public InkRecognitionUnit getParent() {
        InkRecognitionUnit node;
        try {
            node = result.getRecognitionUnit(parentId);
        }
        catch (Exception e) {
            //An unknown data processing error occurred.
            System.out.print("Data Processing Error: " + e.getMessage());
            throw e;
        }
        return node;
    }

    public Rectangle getBoundingBox() {
        return boundingBox;
    }

    public ArrayList<InkPoint> getRotatedBoundingBox() {
        return rotatedBoundingBox;
    }

    InkRecognitionUnit(String jsonString, DisplayMetrics metrics, InkRoot result) throws JSONException {
        DOT_PER_INCH = metrics.xdpi;
        this.result = result;

        try {
            JSONObject obj = new JSONObject(jsonString);
            this.id = obj.getInt("id");
            this.category = getCategory(obj.getString("category"));
            this.parentId = obj.getInt("parentId");

            if (obj.has("childIds")) {
                JSONArray children = obj.getJSONArray("childIds");
                if (children.length() != 0) {
                    for (int i = 0; i < children.length(); i++) {
                        this.childIds.add(children.getInt(i));
                    }
                }
            }

            JSONArray strokes = obj.getJSONArray("strokeIds");
            if (strokes.length() != 0) {
                for (int i=0; i< strokes.length() ; i++) {
                    this.strokeIds.add(strokes.getInt(i));
                }
            }

            //setup bounding box
            JSONObject boundingRect = obj.getJSONObject("boundingRectangle");

            this.boundingBox = new Rectangle(millimetersToPixels((float)boundingRect.getDouble("topX")),
                    millimetersToPixels((float)boundingRect.getDouble("topY")),
                    millimetersToPixels((float)boundingRect.getDouble("width")),
                    millimetersToPixels((float)boundingRect.getDouble("height")));

            //setup rotated bounding box
            JSONArray rotatedBoundingRectPoints = obj.getJSONArray("rotatedBoundingRectangle");
            rotatedBoundingBox = new ArrayList<>();
            for (int i =0;
                 i <rotatedBoundingRectPoints.length();
                 i++) {
                JSONObject point = rotatedBoundingRectPoints.getJSONObject(i);
                this.rotatedBoundingBox.add(new InkPoint(millimetersToPixels((float)point.getDouble("x")), millimetersToPixels((float)point.getDouble("y"))));
            }
        }
        catch (Exception e) {
            System.out.print("Data Processing Error: " + e.getMessage());
            throw e;
        }
    }

    private InkRecognitionUnitCategory getCategory(String category) {

        InkRecognitionUnitCategory recognitionCategory;
        switch(category) {
            case "unknown":
                recognitionCategory = InkRecognitionUnitCategory.UNKNOWN;
                break;
            case "inkWord":
                recognitionCategory = InkRecognitionUnitCategory.INK_WORD;
                break;
            case "inkBullet":
                recognitionCategory = InkRecognitionUnitCategory.INK_BULLET;
                break;
            case "inkListItem":
                recognitionCategory = InkRecognitionUnitCategory.INK_LIST_ITEM;
                break;
            case "inkDrawing":
                recognitionCategory = InkRecognitionUnitCategory.INK_DRAWING;
                break;
            case "line":
                recognitionCategory = InkRecognitionUnitCategory.INK_LINE;
                break;
            case "paragraph":
                recognitionCategory = InkRecognitionUnitCategory.INK_PARAGRAPH;
                break;
            case "writingRegion":
                recognitionCategory = InkRecognitionUnitCategory.INK_WRITING_REGION;
                break;
                default:
                recognitionCategory = InkRecognitionUnitCategory.UNKNOWN;
                break;
        }
        return recognitionCategory;
    }

    float millimetersToPixels(float milliValue) {
        return (milliValue/INCH_TO_MM) * DOT_PER_INCH;
    }

}
