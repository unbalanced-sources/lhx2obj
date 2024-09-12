using Kaitai;
using System.Numerics;

namespace lhx2obj
{
    public class DPolyGenerator : BaseGenerator
    {
        private readonly List<Vector3> _dotCloud;

        private readonly LhxElements.Polygon _polygon;

        private readonly float _distance;

        public DPolyGenerator(LhxElements.Element element, List<Vector3> dotCloud, float distance) : base(element)
        {
            _dotCloud = dotCloud;
            _polygon  = (LhxElements.Polygon)element.Geometry;
            _distance = distance;
        }

        protected override void GenerateInstructions()
        {

            DescribeSide(-_distance, 1);
            DescribeSide(_distance, 2);

        }
    
        private void DescribeSide(float distance, int sideNumber)
        {
            string currInstrParams = "";
            //one side of polygon -> forced to create offsetted polygon and insert its coords and walk description            
            _generatedInstructions.Add("# " + sideNumber + " side offset polygon for 2-sided polygon");
            //1. create offset points
            var offsetPolygon = MathHelper.OffsetPolygon(_polygon, _dotCloud, distance);
            //2. add vertices section
            foreach (var dot in offsetPolygon)
            {
                _generatedInstructions.Add("v" + " " + dot.X + " " + dot.Y + " " + dot.Z);
            }
            //3. add face section (using negative numbers)
            currInstrParams = "";
            
            if(sideNumber == 1)
            {
                for (int i = offsetPolygon.Count; i >= 1; i--)                    
                {
                    currInstrParams = currInstrParams + " " + (-i);
                }

            }            
            else
            {
                for (int i = 1; i <= offsetPolygon.Count; i++)                    
                {
                    currInstrParams = currInstrParams + " " + (-i);
                }
            }
            
            
            //4. update lowdotscount
            _generatedInstructions.Add("f" + currInstrParams);
            _generatedInstructions.Add("# end of " + sideNumber + " side offset polygon for 2-sided polygon");
            return;
        }
    }
}