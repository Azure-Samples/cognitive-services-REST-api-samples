using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Foundation;
using Windows.UI.Input.Inking;

namespace Contoso.NoteTaker.Helpers
{
    class InkPointHelper
    {
        public static string InkPointsToString(IReadOnlyList<InkPoint> points)
        {
            var inkPointsString = new StringBuilder();
            for (var i = 0; i < points.Count(); i++)
            {
                float x = Convert.ToSingle(points[i].Position.X);
                float y = Convert.ToSingle(points[i].Position.Y);

                if (i == points.Count() - 1)
                {
                    inkPointsString.Append(x).Append(",").Append(y);
                }
                else
                {
                    inkPointsString.Append(x).Append(",").Append(y).Append(",");
                }
            }
            return inkPointsString.ToString();
        }

        public static List<InkPoint> ConvertPixelsToMillimeters(IReadOnlyList<InkPoint> pointsInPixels, float DpiX, float DpiY)
        {
            var transformedInkPoints = new List<InkPoint>();
            const float inchToMillimeterFactor = 25.4f; 

            foreach (var point in pointsInPixels)
            {
                var transformedX = (point.Position.X / DpiX) * inchToMillimeterFactor;
                var transformedY = (point.Position.Y / DpiY) * inchToMillimeterFactor;
                var transformedPoint = new Point(transformedX, transformedY);
                var transformedInkPoint = new InkPoint(transformedPoint, point.Pressure);

                transformedInkPoints.Add(transformedInkPoint);
            }
            return transformedInkPoints;
        }
    }
}
