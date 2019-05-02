using Contoso.NoteTaker.JSON.Format;
using Contoso.NoteTaker.Services.Ink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoteTaker
{
    public class OutputWriter
    {
        public static string PrintLines(InkRecognitionRoot root)
        {
            var lines = root.GetLines();
            return PrintRecoUnits(root, lines);
        }

        public static string PrintShapes(InkRecognitionRoot root)
        {
            var shapes = root.GetShapes();
            return PrintRecoUnits(root, shapes);
        }

        public static string Print(InkRecognitionRoot root)
        {
            var output = new StringBuilder();
            // Print lines
            output.Append(PrintLines(root));
            // Print shapes
            output.Append(PrintShapes(root));

            return output.ToString();
        }

        public static string PrintError(string errMsg)
        {
            return String.Format("ERROR: {0}", errMsg);
        }

        private static string PrintRecoUnits(InkRecognitionRoot root, IEnumerable<InkRecognitionUnit> recoUnits)
        {
            var recognizedText = new StringBuilder();
            foreach (var recoUnit in recoUnits)
            {
                switch (recoUnit.Kind)
                {
                    case RecognitionUnitKind.InkWord:
                        var word = recoUnit as InkWord;
                        recognizedText.Append(word.RecognizedText).Append(" ");
                        break;

                    case RecognitionUnitKind.InkLine:
                        var line = recoUnit as InkLine;
                        var lineChildren = root.GetChildNodesOf(line.Id).ToList();
                        recognizedText.Append(PrintLineChildren(root, lineChildren)).AppendLine();
                        break;

                    case RecognitionUnitKind.InkDrawing:
                        var shape = recoUnit as InkDrawing;
                        var shapeKind = String.Format("[{0}] ", shape.RecognizedShape);
                        recognizedText.Append(shapeKind);
                        break;

                    case RecognitionUnitKind.InkBullet:
                        var bullet = recoUnit as InkBullet;
                        recognizedText.Append(bullet.RecognizedText);
                        break;
                }
            }
            return recognizedText.ToString();
        }

        private static string PrintLineChildren(InkRecognitionRoot root, IEnumerable<InkRecognitionUnit> lineChildren)
        {
            return PrintRecoUnits(root, lineChildren);
        }
    }
}
