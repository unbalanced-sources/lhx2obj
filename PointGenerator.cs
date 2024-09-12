using Kaitai;

namespace lhx2obj
{
    public class PointGenerator : BaseGenerator
    {
        public PointGenerator(LhxElements.Element element) : base(element)
        {
        }

        protected override void GenerateInstructions()
        {
            LhxElements.Point _point = (LhxElements.Point)_element.Geometry;
            _generatedInstructions.Add("p " + (_point.PointNumOfPoint+1));
        }
    }
}