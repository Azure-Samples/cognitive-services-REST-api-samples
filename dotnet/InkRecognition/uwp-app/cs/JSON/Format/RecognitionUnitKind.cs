using System.Runtime.Serialization;

namespace Contoso.NoteTaker.JSON.Format
{
    public enum RecognitionUnitKind
    {
        [EnumMember(Value = "writingRegion")]
        InkWritingRegion,

        [EnumMember(Value = "paragraph")]
        InkParagraph,

        [EnumMember(Value = "line")]
        InkLine,

        [EnumMember(Value = "inkWord")]
        InkWord,

        [EnumMember(Value = "inkDrawing")]
        InkDrawing,

        [EnumMember(Value = "listItem")]
        InkListItem,

        [EnumMember(Value = "inkBullet")]
        InkBullet,

        [EnumMember(Value = "unknown")]
        Unknown
    }
}
