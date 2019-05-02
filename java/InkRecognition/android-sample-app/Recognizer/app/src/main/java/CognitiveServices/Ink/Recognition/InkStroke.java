package CognitiveServices.Ink.Recognition;

import CognitiveServices.Ink.Recognition.InkPoint;

import java.util.ArrayList;
class InkStroke {
    final int strokeId;
    final ArrayList<InkPoint> inkPoints = new ArrayList<>();
    final String language;
    final StrokeKind kind;

    private static int num = 0;
    @SuppressWarnings("unused")
    InkStroke(int id, String language, StrokeKind kind) {
        this.strokeId = id;
        this.language = language;
        this.kind = kind;
    }

    InkStroke() {
        this.strokeId = getNextNum();
        System.out.println(this.strokeId);
        this.language = "en-US";
        this.kind = StrokeKind.UNKNOWN;
    }

    public void addPoint(double x, double y) {
        InkPoint point = new InkPoint(x,y);
        inkPoints.add(point);
    }

    private int getNextNum()
    {
        return ++num;
    }

}
