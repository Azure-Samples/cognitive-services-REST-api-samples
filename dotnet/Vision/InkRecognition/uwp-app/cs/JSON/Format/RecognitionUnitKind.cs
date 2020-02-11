using System.Runtime.Serialization;

namespace Contoso.NoteTaker.JSON.Format
{
    public enum RecognitionUnitKind
    {
        [EnumMember(Value = "writingRegion")]
        WritingRegion,

        [EnumMember(Value = "paragraph")]
        Paragraph,

        [EnumMember(Value = "line")]
        Line,

        [EnumMember(Value = "inkWord")]
        InkWord,

        [EnumMember(Value = "inkDrawing")]
        InkDrawing,

        [EnumMember(Value = "listItem")]
        ListItem,

        [EnumMember(Value = "inkBullet")]
        InkBullet,

        [EnumMember(Value = "unknown")]
        Unknown
    }
}
