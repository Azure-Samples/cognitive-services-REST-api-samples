package CognitiveServices.Ink.Recognition;

public class Rectangle {
    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }

    public double getWidth() {
        return width;
    }

    public double getHeight() {
        return height;
    }

    private final double x;
    private final double y;
    private final double width;
    private final double height;
    Rectangle(double x, double y, double width, double height) {
        this.x = x;
        this.y=y;
        this.width =width;
        this.height = height;
    }
}
