package CognitiveServices.Ink.Recognition;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.os.CountDownTimer;
import android.util.DisplayMetrics;
import android.view.MotionEvent;
import android.view.View;

public class NoteTaker extends View {
    private final Path path = new Path();
    private final Paint brush = new Paint();
    private InkStroke stroke;
    private final InkRecognizer inkRecognizer;
    private final float xdpi;
    private final float ydpi;
    private CountDownTimer analysisTimer = null;

    public NoteTaker(Context context) {
        super(context);
        String appKey = "<SUBSCRIPTION_KEY>";
        String destinationUrl = "https://api.cognitive.microsoft.com/inkrecognizer/v1.0-preview/recognize";
        inkRecognizer = new InkRecognizer(appKey, destinationUrl, context);
        brush.setAntiAlias(true);
        brush.setColor(Color.BLACK);
        brush.setStyle(Paint.Style.STROKE);
        brush.setStrokeJoin(Paint.Join.ROUND);
        brush.setStrokeWidth(3.0f);
        DisplayMetrics metrics = getResources().getDisplayMetrics();
        xdpi = getResources().getDisplayMetrics().xdpi;
        ydpi = getResources().getDisplayMetrics().ydpi;
        inkRecognizer.setMetrics(metrics);
    }

    private void startTimer() {
        analysisTimer = new CountDownTimer(2000, 1000) {
            public void onTick(long millFinish) {

            }

            public void onFinish()
            {
                inkRecognizer.Recognize();
            }
        }.start();
    }

    private void cancelTimer() {
        if (analysisTimer != null) {
            analysisTimer.cancel();
        }
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        float x = event.getX();
        float y = event.getY();

        float dataX = x/xdpi*25.4f;
        float dataY = y/ydpi*25.4f;
        switch(event.getAction()) {
            case MotionEvent.ACTION_DOWN:
                path.moveTo(x,y);
                stroke = new InkStroke();
                stroke.addPoint(dataX,dataY);
                cancelTimer();
                return true;
            case MotionEvent.ACTION_MOVE:
                path.lineTo(x,y);
                stroke.addPoint(dataX,dataY);
                break;
            case MotionEvent.ACTION_UP:
                inkRecognizer.addStroke(stroke);
                startTimer();
                break;
            default:
                return false;
        }
        postInvalidate();
        return false;

    }

    @Override
    protected void onDraw(Canvas canvas)
    {
        canvas.drawPath(path, brush);
    }
}
