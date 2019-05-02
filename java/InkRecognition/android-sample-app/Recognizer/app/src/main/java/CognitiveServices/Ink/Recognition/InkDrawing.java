package CognitiveServices.Ink.Recognition;

import android.util.DisplayMetrics;

import org.json.JSONArray;
import org.json.JSONObject;
import java.util.ArrayList;
import java.util.HashMap;

class InkDrawing extends InkRecognitionUnit {

    private InkPoint center ;
    private double confidence;
    private Shape shape;
    private double rotatedAngle;
    private final ArrayList<Shape> alternates = new ArrayList<>();
    private ArrayList<InkPoint> points = new ArrayList<>();
    private static boolean initialized = false;
    private static final HashMap<String, Shape> shapeMap = new HashMap<>();

    public InkPoint getCenter() {
        return center;
    }

    public double getConfidence() {
        return confidence;
    }

    public Shape getShape() { return shape; }

    public double getRotatedAngle() {
        return rotatedAngle;
    }

    public ArrayList<Shape> getAlternates() {
        return alternates;
    }

    public ArrayList<InkPoint> getPoints() {
        return points;
    }


    InkDrawing(String drawingJSON, DisplayMetrics metrics, InkRoot result) throws Exception {
        super(drawingJSON, metrics, result);
        try {

            JSONObject obj = new JSONObject(drawingJSON);
            if (obj.has("center")) {
                Double xValue = obj.getJSONObject("center").getDouble("x");
                Double yValue = obj.getJSONObject("center").getDouble("y");
                this.center = new InkPoint(millimetersToPixels(xValue.floatValue()),millimetersToPixels(yValue.floatValue()));
            }

            this.confidence = obj.getDouble("confidence");
            this.shape = getShape(obj.getString("recognizedObject"));
            this.rotatedAngle = obj.getDouble("rotationAngle");
            if (obj.has("alternates")) {
                JSONArray jsonAlternates = obj.getJSONArray("alternates");
                for (int i = 0;
                     i < jsonAlternates.length();
                     i++) {
                    this.alternates.add(getShape(jsonAlternates.getString(i)));
                }
            }
            //extract points
            if (obj.has("points")) {
                JSONArray shapePoints = obj.getJSONArray("points");


                this.points = new ArrayList<>();

                for (int i =0;
                     i <shapePoints.length();
                     i++) {

                    JSONObject point = shapePoints.getJSONObject(i);
                    points.add(new InkPoint(millimetersToPixels((float)point.getDouble("x")), millimetersToPixels((float)point.getDouble("y"))));
                }
            }
        }
        catch (Exception e) {
                System.out.print("Data Processing Error: " + e.getMessage());
                throw e;
        }
    }

    private static Shape stringToShape(String shapeName)
    {
        if (!initialized) {
            shapeMap.put("drawing", Shape.DRAWING);
            shapeMap.put("circle", Shape.CIRCLE);
            shapeMap.put("square", Shape.SQUARE);
            shapeMap.put("rectangle", Shape.RECTANGLE);
            shapeMap.put("triangle", Shape.TRIANGLE);
            shapeMap.put("ellipse", Shape.ELLIPSE);
            shapeMap.put("isoscelesTriangle", Shape.ISOSCELESTRIANGLE);
            shapeMap.put("equilateralTriangle", Shape.EQUILATERALTRIANGLE);
            shapeMap.put("rightTriangle", Shape.RIGHTTRIANGLE);
            shapeMap.put("quadrilateral", Shape.QUADRILATERAL);
            shapeMap.put("diamond", Shape.DIAMOND);
            shapeMap.put("trapezoid", Shape.TRAPEZOID);
            shapeMap.put("parallelogram", Shape.PARALLELOGRAM);
            shapeMap.put("pentagon", Shape.PENTAGON);
            shapeMap.put("hexagon", Shape.HEXAGON);
            shapeMap.put("blockArrow", Shape.BLOCKARROW);
            shapeMap.put("heart", Shape.HEART);
            shapeMap.put("starSimple", Shape.STARSIMPLE);
            shapeMap.put("starCrossed", Shape.STARCROSSED);
            shapeMap.put("cloud", Shape.CLOUD);
            shapeMap.put("line", Shape.LINE);
            shapeMap.put("curve", Shape.CURVE);
            shapeMap.put("polyline", Shape.POLYLINE);
            initialized = true;
        }
        return shapeMap.get(shapeName);
    }

    private Shape getShape(String inputShape) {
        return stringToShape(inputShape);
    }
}


