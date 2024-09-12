using System.IO;

namespace Kaitai
{
    public class MtlManager
    {
       
        IDictionary<byte, string> _colorMaps;
        IDictionary<byte, string> _colorNames;
        IDictionary<Tuple<byte, byte>, string> _materialsDict; //color+opaqueness = name of materials
        string _modelName;
        string _matFileName;

        public MtlManager(string matFileName, bool lowMesh = true)
        {
 
            _colorMaps = new Dictionary<byte,string>(){
                {0,"0 0 0"},  {1,"0 0 0.666667"}, {2,"0 0.666667 0"}, {3,"0 0.666667 0.666667"}, 
                {4,"0.666667 0 0"},  {5,"0.666667 0 0.666667"},  {6,"0.666667 0.333333 0"},  {7,"0.666667 0.666667 0.666667"}, 
                {8,"0.333333 0.333333 0.333333"},  {9,"0.333333 0.333333 1"},  {10,"0.333333 1 0.333333"}, {11,"0.333333 1 1"}, 
                {12,"1 0.333333 0.333333"}, {13,"1 0.333333 1"}, {14,"1 1 0.333333"}, {15,"1 1 1"} 

            };

            _colorNames = new Dictionary<byte,string>(){
                {0,"black"},  {1,"blue"},  {2,"green"},  {3,"cyan"}, 
                {4,"red"},  {5,"magenta"},  {6,"yellow"},  {7,"bgray"}, 
                {8,"gray"},  {9,"bblue"},  {10,"bgreen"}, {11,"bcyan"}, 
                {12,"bred"}, {13,"bmagenta"}, {14,"byellow"}, {15,"white"} 

            };

            _materialsDict = new Dictionary<Tuple<byte, byte>, string>();
            
            _matFileName = matFileName;

            File.WriteAllText(_matFileName,"");  //touch and erase current mat lib
        }

        public string AssignMaterial(byte color, byte opaqueness)
        {
            string materialname;
            var buffer = new List<string>();
            var key = Tuple.Create(color, opaqueness);

            if(_materialsDict.ContainsKey(key))  //if already in
            {
                return _materialsDict[key];
            }

            //form material description
            materialname = _colorNames[color] + opaqueness;
            buffer.Add("newmtl " + materialname); //header
            buffer.Add("Ka " + _colorMaps[color]); //Ka line
            buffer.Add("Kd " + _colorMaps[color]); //Kd line
            buffer.Add("illum 1");                //illum model: 1 - Ka and Kd only
            buffer.Add("d " + ((float)opaqueness/255));        //Opaqueness, works on any illumination model (unlike transparency Tr)
            buffer.Add(" ");                      //just separator

            //save it to file
            File.AppendAllLines(_matFileName, buffer);

            //save it to dictionary
            _materialsDict.Add(key, materialname);

            return materialname;

        }
        
    
    }
        
}