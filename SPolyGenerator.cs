using Kaitai;

namespace lhx2obj
{
    public class SPolyGenerator : BaseGenerator
    {
        public SPolyGenerator(LhxElements.Element element) : base(element){}

        protected override void GenerateInstructions()
        {
            LhxElements.Polygon polygon = (LhxElements.Polygon)_element.Geometry;
            string currInstrParams = "";
                    
            for (int i = polygon.NumOfPoints; i >= 1; i--)
            {
               currInstrParams = currInstrParams + " " + (polygon.Points.ElementAt(i-1)+1);
            }
            _generatedInstructions.Add("f" + currInstrParams);
             
        }
    }
}