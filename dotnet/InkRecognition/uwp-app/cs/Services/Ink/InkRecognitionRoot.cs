using Contoso.NoteTaker.JSON.Format;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.NoteTaker.Services.Ink
{
    public class InkRecognitionRoot
    {
        Dictionary<UInt64, InkLine> recognizedLines;
        Dictionary<UInt64, InkDrawing> recognizedDrawings;
        Dictionary<UInt64, InkRecognitionUnit> recognizedUnits;
        Dictionary<UInt64, List<InkRecognitionUnit>> childrenOfRecognizedUnits;

        public InkRecognitionRoot(InkRecognitionResponse response)
        {
            recognizedLines = new Dictionary<UInt64, InkLine>();
            recognizedDrawings = new Dictionary<UInt64, InkDrawing>();
            recognizedUnits = new Dictionary<UInt64, InkRecognitionUnit>();
            childrenOfRecognizedUnits = new Dictionary<UInt64, List<InkRecognitionUnit>>();

            foreach (var recoUnit in response.RecognitionUnits)
            {
                switch (recoUnit.Kind)
                {
                    case RecognitionUnitKind.InkLine:
                        var line = recoUnit as InkLine;
                        recognizedLines.Add(recoUnit.Id, line);
                        break;
                    case RecognitionUnitKind.InkDrawing:
                        var drawing = recoUnit as InkDrawing;
                        recognizedDrawings.Add(recoUnit.Id, drawing);
                        break;
                }
                recognizedUnits.Add(recoUnit.Id, recoUnit);
            }

            // mapping of parent to children
            foreach (var recoUnit in response.RecognitionUnits)
            {
                var childUnits = new List<InkRecognitionUnit>();
                if (recoUnit.ChildIds != null)
                {
                    foreach (var childId in recoUnit.ChildIds)
                    {
                        var childUnit = recognizedUnits[childId];
                        childUnits.Add(childUnit);
                    }
                }
                childrenOfRecognizedUnits.Add(recoUnit.Id, childUnits);
            }
        }

        public IEnumerable<InkLine> GetLines()
        {
            var lines = recognizedLines.Values.ToList().AsReadOnly();
            return lines;
        }

        public IEnumerable<InkDrawing> GetShapes()
        {
            var shapes = recognizedDrawings.Values.ToList().AsReadOnly();
            return shapes;
        }

        public IEnumerable<InkRecognitionUnit> GetChildNodesOf(UInt64 id)
        {
            if (childrenOfRecognizedUnits.ContainsKey(id))
            {
                var childrenNodes = childrenOfRecognizedUnits[id];
                return childrenNodes.AsReadOnly();
            }
            throw new ArgumentException("Invalid id");
        }
    }
}
