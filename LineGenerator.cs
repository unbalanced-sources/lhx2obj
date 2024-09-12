using Kaitai;

namespace lhx2obj
{
    public class LineGenerator : BaseGenerator
    {
        public LineGenerator(LhxElements.Element element) : base(element)
        {
        }

        protected override void GenerateInstructions()
        {
            LhxElements.Line _line = (LhxElements.Line)_element.Geometry;
            _generatedInstructions.Add("l " + (_line.Points[0]+1) + " " + (_line.Points[1]+1));            
        }
    }
}