using Kaitai;
using System.Numerics;

namespace lhx2obj
{
    public static class MathHelper
    {
        public static List<Vector3> OffsetPolygon(LhxElements.Polygon polygon, List<Vector3> dotCloud, float offsetDistance)
        {
            Vector3 left   = CreateDiffVector(polygon.Points[0], polygon.Points[1], dotCloud);
            Vector3 right  = CreateDiffVector(polygon.Points[1], polygon.Points[2], dotCloud);
            Vector3 normal = Vector3.Cross(Vector3.Normalize(left), Vector3.Normalize(right));

            var offset = new List<Vector3>();

            foreach (var d in polygon.Points)
            {
                var offsetPoint = new Vector3();
                offsetPoint.X = dotCloud.ElementAt(d).X - (offsetDistance * normal.X);
                offsetPoint.Y = dotCloud.ElementAt(d).Y - (offsetDistance * normal.Y);
                offsetPoint.Z = dotCloud.ElementAt(d).Z - (offsetDistance * normal.Z);    
                offset.Add(offsetPoint);
            }

            return offset;        
        }
    
        public static Vector3 CreateVector(int index, List<Vector3> dotCloud)
        {
           return new Vector3(dotCloud.ElementAt(index).X,
                              dotCloud.ElementAt(index).Y,
                              dotCloud.ElementAt(index).Z);
        }

        public static Vector3 CreateDiffVector(int startIndex, int endIndex, List<Vector3> dotCloud)
        {
           return new Vector3(dotCloud.ElementAt(endIndex).X - dotCloud.ElementAt(startIndex).X,
                              dotCloud.ElementAt(endIndex).Y - dotCloud.ElementAt(startIndex).Y,
                              dotCloud.ElementAt(endIndex).Z - dotCloud.ElementAt(startIndex).Z);
        }

        public static Vector3 Vector3FromTuple((float first, float second, float third)tuple)
        {
            return new Vector3(tuple.first, tuple.second, tuple.third);
        }

        public static Vector3 Vector3FromDot(LhxPoints.Dot dot)
        {
            return new Vector3(dot.X, dot.Y, dot.Z);
        }

    }

}