using Kaitai;

namespace lhx2obj
{
    public abstract class BaseGenerator
    {
        protected readonly LhxElements.Element _element;
        protected List<string> _generatedInstructions;

        public BaseGenerator(LhxElements.Element element)
        {
            _element = element;
            _generatedInstructions = new List<string>();
        }

        protected abstract void GenerateInstructions();

        public List<string> GeneratedInstructions()
        {
            GenerateInstructions();
            return (_generatedInstructions);
        }
    }
}