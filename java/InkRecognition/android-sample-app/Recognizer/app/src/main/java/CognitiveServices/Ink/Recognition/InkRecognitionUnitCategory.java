package CognitiveServices.Ink.Recognition;

import android.support.annotation.NonNull;

public enum InkRecognitionUnitCategory {
    UNKNOWN("Unknown"),
    INK_BULLET("Bullet"),
    INK_LIST_ITEM("ListItem"),
    INK_WORD("Word"),
    INK_DRAWING ("Drawing"),
    INK_PARAGRAPH("Paragraph"),
    INK_LINE ("Line"),
    INK_WRITING_REGION("WritingRegion");

    private final String category;
    InkRecognitionUnitCategory(String category) {
        this.category = category;
    }

    @NonNull
    public String toString(){
        return category;
    }
}
