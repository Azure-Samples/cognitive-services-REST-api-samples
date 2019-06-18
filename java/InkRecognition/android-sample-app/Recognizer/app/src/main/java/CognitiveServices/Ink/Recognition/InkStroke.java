package CognitiveServices.Ink.Recognition;

import android.util.DisplayMetrics;
import java.util.ArrayList;

class InkStroke {
    final int strokeId;
    final ArrayList<InkPoint> inkPoints = new ArrayList<>();
    final String language;
    final StrokeKind kind;
    private final float xdpi;
    private final float ydpi;
    private static int num = 0;

    @SuppressWarnings("unused")
    InkStroke(int id, String language, StrokeKind kind, DisplayMetrics metrics) {
        this.xdpi = metrics.xdpi;
        this.ydpi = metrics.ydpi;
        this.strokeId = id;
        this.language = language;
        this.kind = kind;
    }

    InkStroke(DisplayMetrics metrics) {
        this.xdpi = metrics.xdpi;
        this.ydpi = metrics.ydpi;
        this.strokeId = getNextNum();
        this.language = "en-US";
        this.kind = StrokeKind.UNKNOWN;
    }

    public void addPoint(float x, float y) {
        x = x / xdpi * InkRecognitionUnit.INCH_TO_MM;
        y = y / ydpi * InkRecognitionUnit.INCH_TO_MM;
        InkPoint point = new InkPoint(x, y);
        inkPoints.add(point);
    }

    private int getNextNum()
    {
        return ++num;
    }
}