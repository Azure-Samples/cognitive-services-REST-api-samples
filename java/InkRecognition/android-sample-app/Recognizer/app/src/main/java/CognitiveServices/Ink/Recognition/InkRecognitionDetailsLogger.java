package CognitiveServices.Ink.Recognition;

import android.util.Log;

import java.util.ArrayList;

/*
  This class shows the values you can get from the different types of nodes returned from the
  service. The values can be used to beautify the user interface.
*/

class InkRecognitionDetailsLogger {

    private static final String TAG = "InkRecognizer";

    public static void displayAnalysisTree(InkRoot root) {
        ArrayList<InkParagraph> paragraphs = root.getParagraphs();
        ArrayList<InkDrawing> drawings = root.getInkDrawings();
        for (InkParagraph paragraph : paragraphs) {
            ArrayList<InkRecognitionUnit> paragraphOffspring  = paragraph.getChildren();
            for (InkRecognitionUnit sibling : paragraphOffspring) {
                displayRecognitionUnit(sibling);
            }
        }

        for (InkDrawing drawing : drawings) {
            InkRecognitionDetailsLogger.displayInkDrawingProperties(drawing);
        }

    }

    private static void displayRecognitionUnit(InkRecognitionUnit recognitionUnit) {

        Log.d(TAG,Integer.toString(recognitionUnit.getId()));
        for (int id : recognitionUnit.getStrokeIds()) {
            Log.d(TAG,"__________________________________________\n\r");
            Log.d(TAG,"Id: " + id);
            Log.d(TAG,"RecognitionUnit Category: " + recognitionUnit.getCategory().toString());
            switch(recognitionUnit.getCategory()) {
                case INK_LINE:
                    InkLine line  = (InkLine)recognitionUnit;
                    InkRecognitionDetailsLogger.displayLineProperties(line);
                    break;
                case INK_WORD:
                    InkWord word  = (InkWord)recognitionUnit;
                    InkRecognitionDetailsLogger.displayInkWordProperties(word);
                    break;
                case INK_BULLET:
                    InkBullet bullet  = (InkBullet)recognitionUnit;
                    InkRecognitionDetailsLogger.displayBulletProperties(bullet);
                    break;
                default:
                    break;
            }
            Log.d(TAG,"Parent Category: " + recognitionUnit.getParent().getCategory());
            Log.d(TAG,"BoundingBox X: " + recognitionUnit.getBoundingBox().getX() + "\n\r");
            Log.d(TAG,"BoundingBox Y: " + recognitionUnit.getBoundingBox().getY() + "\n\r");
            Log.d(TAG,"BoundingBox Width: " + recognitionUnit.getBoundingBox().getWidth()+ "\n\r");
            Log.d(TAG,"BoundingBox Height: " + recognitionUnit.getBoundingBox().getHeight()+ "\n\r");
            Log.d(TAG,"Category: " + recognitionUnit.getCategory().toString());
            Log.d(TAG,"Rotated Bounding Box Point 1: " + recognitionUnit.getRotatedBoundingBox().get(0).x
                    + " "+ recognitionUnit.getRotatedBoundingBox().get(0).y+ "\n\r");
            Log.d(TAG,"Point 2: " + recognitionUnit.getRotatedBoundingBox().get(1).x
                    + " "+ recognitionUnit.getRotatedBoundingBox().get(1).y+ "\n\r");
            Log.d(TAG,"Point 3: " + recognitionUnit.getRotatedBoundingBox().get(2).x
                    + " "+ recognitionUnit.getRotatedBoundingBox().get(2).y+ "\n\r");
            Log.d(TAG,"Point 4: " + recognitionUnit.getRotatedBoundingBox().get(3).x
                    + " "+ recognitionUnit.getRotatedBoundingBox().get(3).y+ "\n\r");
        }

        for (InkRecognitionUnit childUnit : recognitionUnit.getChildren()) {
            displayRecognitionUnit(childUnit);
        }

    }

    private static void displayInkDrawingProperties(InkDrawing drawing) {

        if (drawing.getCenter() != null) {
            Log.d(TAG, "center: " + drawing.getCenter().x + " " +
                    drawing.getCenter().y);
        }
        Log.d(TAG, "rotated angle: "+ drawing.getRotatedAngle());
        Log.d(TAG, "confidence: "+ drawing.getConfidence());
        for (Shape alternate : drawing.getAlternates()) {
            Log.d(TAG," "+ alternate + "\n\r");
        }

        for (InkPoint point : drawing.getPoints()) {
            Log.d(TAG, "Point: " + point.x + " " +
                    point.y);
        }
        Log.d(TAG,"alternates: \n\r");
        for (Shape alternate : drawing.getAlternates()) {
            Log.d(TAG," "+ alternate + "\n\r");
        }
    }

    private static void displayInkWordProperties(InkWord word) {

        Log.d(TAG,"Text :" + word.getText());
        Log.d(TAG,"alternates: \n\r");
        for (String alternate : word.getAlternates()) {
            Log.d(TAG," "+ alternate + "\n\r");
        }
    }

    private static void displayLineProperties(InkLine line) {
        Log.d(TAG, Float.toString(line.getIndentLevel()));
        Log.d(TAG, line.getText());
        Log.d(TAG,"alternates: \n\r");
        for (String alternate : line.getAlternates()) {
            Log.d(TAG," "+ alternate + "\n\r");
        }
    }

    private static void displayBulletProperties(InkBullet bullet) {
        Log.d(TAG," "+ bullet.getText() + "\n\r");
    }
}
