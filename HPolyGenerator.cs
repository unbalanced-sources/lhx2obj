using Kaitai;

namespace lhx2obj
{

    public class HPolyGenerator : BaseGenerator
    {
        public HPolyGenerator(LhxElements.Element element) : base(element){}
        
        
        protected override void GenerateInstructions()
        {
            LhxElements.Polygon polygon = (LhxElements.Polygon)_element.Geometry;
            string currInstrParams = "";
            for (int i = 0; i < polygon.NumOfPoints; i++)
            {
               currInstrParams = currInstrParams + " " + (polygon.Points.ElementAt(i)+1);
            }
            _generatedInstructions.Add("p" + currInstrParams);
        }
    }
}