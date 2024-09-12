// See https://aka.ms/new-console-template for more information
using Kaitai;
using lhx2obj;
using CommandLine;
using System.Numerics;
using System.IO;
using System.Globalization;

var _args = args;
Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
if (_args.Count() == 0)
{
    _args = new string[]{"--help"};
} 

// foreach (var s in args)
// {
//     Console.WriteLine(s);

// }


Parser.Default.ParseArguments<Options>(_args)
                .WithParsed(Dispatch)
                .WithNotParsed(HandleParseError);



// -------------- functions -------------------------- 

void Dispatch(Options opts){

    //Exceptions
    string outDir = Path.GetDirectoryName(opts.outModelName);
    if (outDir!=null && !Directory.Exists(outDir))
    {
        Directory.CreateDirectory(outDir);
    }

    LhxPoints   points   = LhxPoints.FromFile(opts.inModelName + ".pnt");
    LhxElements elements = LhxElements.FromFile(opts.inModelName + ".bin");

    ParseMesh(opts, GetDotCloud(points.DotcloudLow, opts.yUp), elements.LowModelChunk.Elements);

    if (points.DotcloudMed != null)
    {
        //search for dotcloud chunk in the garbage
        foreach (LhxElements.Chunk chunk in elements.Chunks)
        {
            if(chunk.Data is LhxElements.MedModelConnection)
            {
                LhxElements.MedModelConnection medModelChunk = (LhxElements.MedModelConnection)chunk.Data;
                ParseMesh(opts, GetDotCloud(points.DotcloudMed, opts.yUp), medModelChunk.Elements, false); 
                break;
            }
        }
       
    }
}

List<Vector3> GetDotCloud(LhxPoints.Dotcloud dotCloud, bool yUp){
    
    List<Vector3> _dotCloud = new List<Vector3>();

    foreach (LhxPoints.Dot d in dotCloud.Dots)
    {
        Vector3 dot;

        if (yUp)
        {
            dot = new Vector3(d.X, d.Z, -d.Y);
        }
        else
        {
            dot = new Vector3(d.X, -d.Y, d.Z);
        }
        _dotCloud.Add(dot);
    }        
    
    return _dotCloud;

}

bool ParseMesh(Options opts, List<Vector3> dotcloud, List<LhxElements.Element> elements, bool isLowMesh = true){

    Options _opts = opts;
    
    //deal with default opts values
    if (_opts.outModelName == null)  _opts.outModelName = _opts.inModelName;
    
    char pointsMode    = 'S';
    float pointsRadius = 1.0f;
    char linesMode     = 'S';
    float linesWidth   = 1.0f;
    char spheresMode   = 'G';
    char dSidePolsMode = 'G';
    float dSideDist    = 0.1f;
    char revPolsMode   = 'S';

    
    if (_opts.pointOpts.Count()!=0) pointsMode   = Char.ToUpper(_opts.pointOpts.ElementAt(0)[0]);//get char of command
    if (_opts.pointOpts.Count() >1) pointsRadius = Convert.ToSingle(_opts.pointOpts.ElementAt(1),
                                                                    CultureInfo.InvariantCulture);//get radius
                                                                

    if (_opts.lineOpts.Count() !=0) linesMode  = Char.ToUpper(_opts.lineOpts.ElementAt(0)[0]);//get char of command
    if (_opts.lineOpts.Count() >1)  linesWidth = Convert.ToSingle(_opts.lineOpts.ElementAt(1),
                                                                CultureInfo.InvariantCulture);//get width

    if (_opts.dSideOpts.Count()!=0) dSidePolsMode =  Char.ToUpper(_opts.dSideOpts.ElementAt(0)[0]);//get char of command
    if (_opts.dSideOpts.Count() >1) dSideDist     =  Convert.ToSingle(_opts.dSideOpts.ElementAt(1),
                                                                CultureInfo.InvariantCulture);//get distance
    spheresMode = Char.ToUpper(_opts.sphereOpts[0]);
    revPolsMode = Char.ToUpper(_opts.revSideOpt[0]);
   
    string outObjFileName;
    string outMatFileName;
    string outModelName = Path.GetFileNameWithoutExtension(_opts.outModelName);
    //correctirng modelname and obj file path for med-level meshes
    if (isLowMesh)
    {
        outObjFileName = _opts.outModelName + ".obj";
        outMatFileName = _opts.outModelName + ".mtl";
    }
    else
    {
        outModelName = outModelName + "-med";
        outObjFileName = _opts.outModelName + "-med.obj";
        outMatFileName = _opts.outModelName + "-med.mtl";
    }


    //var outModelPath = Path.GetDirectoryName(outModel);
   

    var verticesInstructions = new List<string>();
    var connectionInstructions = new List<string>();

    
    //Points part
    verticesInstructions.Add("mtllib " + outModelName + ".mtl");

    foreach (var d in dotcloud)
    {
        verticesInstructions.Add("v " + d.X.ToString() + " " + d.Y.ToString() + " " + d.Z.ToString());
    }        
    File.WriteAllLines(outObjFileName,verticesInstructions);

    verticesInstructions.Clear();
    verticesInstructions.Add("");
    verticesInstructions.Add("#*** Start of connections part ***");
    verticesInstructions.Add("");

    File.AppendAllLines(outObjFileName, verticesInstructions);

    //Connections part
    var mtlManager = new MtlManager(outMatFileName, isLowMesh); 
    foreach (LhxElements.Element element in elements)
    {   
        //assigning materials
        string mtlName = mtlManager.AssignMaterial(element.ElementColorCode, element.Opaqueness);
        connectionInstructions.Add("usemtl " + mtlName); //assign material to element
        bool isSupported = true;

        List<string> prologue = new List<string>();
        BaseGenerator baseGenerator = null;
        List<string> epilogue = new List<string>();

        // creating geometry
        switch (element.ElementType)
        {
            case LhxElements.TypeOfElement.Point:
                switch (pointsMode)
                {
                    case 'S':
                        baseGenerator = new PointGenerator(element);
                        break;

                    case 'G':
                        LhxElements.Point p   = (LhxElements.Point)element.Geometry;
                        prologue.Add("# Begin POINT -> SPHERE generated replacement. Initial parameters were:");
                        prologue.Add("# Radius " + pointsRadius + ", center point vertex number: " + (p.PointNumOfPoint+1));
                        baseGenerator = new SphereGenerator(element, pointsRadius, dotcloud);
                        epilogue.Add("# End POINT -> SPHERE generated replacement.");
                        break;

                    default:
                        Console.WriteLine("Unsupported points mode: " + pointsMode);
                        break;
                }
                break;

            case LhxElements.TypeOfElement.SeparateLine:
                switch (linesMode)
                {
                    case 'S':
                        baseGenerator = new LineGenerator(element);                        
                        break;
                        
                    case 'G':
                        prologue.Add("# Begin SEPARATE LINE -> COLUMN generated replacement. Initial instruction:");
                        LineGenerator seplineGenerator = new LineGenerator(element);
                        prologue.Add("# " + seplineGenerator.GeneratedInstructions()[0]);
                        baseGenerator = new AdvLineGenerator(element, dotcloud, linesWidth);
                        epilogue.Add("# End SEPARATE LINE generated replacement.");
                        break;
                    
                    default:
                        Console.WriteLine("Unsupported lines mode: " + linesMode);
                        break;
                }
                break;

            case LhxElements.TypeOfElement.OnPolygonLine:
                switch (linesMode)
                {
                    case 'S':
                        baseGenerator = new LineGenerator(element);                        
                        break;
                        
                    case 'G':
                        prologue.Add("# Begin ON-POLYGON LINE -> COLUMN generated replacement. Initial instruction:");
                        LineGenerator onPolylineGenerator = new LineGenerator(element);
                        prologue.Add("# " + onPolylineGenerator.GeneratedInstructions()[0]);
                        baseGenerator = new AdvLineGenerator(element, dotcloud, linesWidth);
                        epilogue.Add("# End ON-POLYGON LINE generated replacement.");
                        break;
                    
                    default:
                        Console.WriteLine("Unsupported lines mode: " + linesMode);
                        break;
                }
                break;            

            //no tricks with hidden poly
            case LhxElements.TypeOfElement.HiddenPolygon:
                baseGenerator = new HPolyGenerator(element);
                break;
            
            //single-side face is always the same
            case LhxElements.TypeOfElement.SingleSidedPolygon:
                baseGenerator = new SPolyGenerator(element);
                break;

            case LhxElements.TypeOfElement.ReversedNormalsPolygon:
                switch (revPolsMode)
                {
                    case 'S':
                        prologue.Add("# Skipping REVERSED NORMALS FACE:");
                        SPolyGenerator spg = new SPolyGenerator(element);
                        epilogue.Add("# " + spg.GeneratedInstructions()[0]);
                       // isSupported = false;
                        break;
                    case 'G':
                        prologue.Add("# Adding REVERSED NORMALS FACE");
                        baseGenerator = new SPolyGenerator(element);
                        break;
                    default:
                        Console.WriteLine("Unsupported reverse normals face mode: " + revPolsMode);
                        break;
                }
                break;
            
            case LhxElements.TypeOfElement.DoubleSidedPolygon:
                switch (dSidePolsMode)
                {
                    case 'S':
                        prologue.Add("# Begin DOUBLE-SIDED FACE -> SINGLE FACE with random normal generated replacement. Initial instruction:");
                        baseGenerator = new SPolyGenerator(element);
                        epilogue.Add("# End DOUBLE-SIDED FACE -> SINGLE FACE with random normal generated replacement.");
                        break;
                    case 'G':
                        prologue.Add("# Begin DOUBLE-SIDED FACE -> PAIR OF EQIDISTANT FACES generated replacement. Initial instruction:");
                        SPolyGenerator spg4 = new SPolyGenerator(element);
                        prologue.Add("# " + spg4.GeneratedInstructions()[0]);
                        baseGenerator = new DPolyGenerator(element, dotcloud, dSideDist);
                        epilogue.Add("# End DOUBLE-SIDED FACE -> PAIR OF EQIDISTANT FACES generated replacement.");
                        break;
                    default:
                        Console.WriteLine("Unsupported double-sided face mode: " + revPolsMode);                    
                        break;
                }
                break;

            case LhxElements.TypeOfElement.Sphere:
                switch (spheresMode)
                {
                    case 'M':
                        LhxElements.Sphere sph   = (LhxElements.Sphere)element.Geometry;
                        prologue.Add("# Mentioning SPHERE instruction. Parameters were:");
                        prologue.Add("# Center point vertex number: " + (sph.PointOfCenter+1) + ", radius: " + sph.Radius);
                        epilogue.Add("# End SPHERE instruction mentioning.");
                        break;
                    case 'G':
                        LhxElements.Sphere sphere   = (LhxElements.Sphere)element.Geometry;
                        prologue.Add("# Begin SPHERE -> 20-SIDED ICOSAHEDRON generated replacement. Initial parameters were:");
                        prologue.Add("# Center point vertex number: " + (sphere.PointOfCenter+1) + ", radius: " + sphere.Radius);
                        baseGenerator = new SphereGenerator(element, dotcloud);
                        epilogue.Add("# End SPHERE -> 20-SIDED ICOSAHEDRON generated replacement.");
                        break;
                    default:
                        Console.WriteLine("Unsupported sphere mode: " + spheresMode);   
                        break;
                }
                break;

            default:
                Console.WriteLine(element.ElementType.ToString() + " is not yet supported"); 
                isSupported = false;
                break;
        }
        if(isSupported)
        {
            if ( prologue.Count != 0   ) connectionInstructions.AddRange(prologue);
            if ( baseGenerator != null ) connectionInstructions.AddRange(baseGenerator.GeneratedInstructions());
            if ( epilogue.Count != 0   ) connectionInstructions.AddRange(epilogue);
            connectionInstructions.Add(""); //separate added block
        }
    }         

    File.AppendAllLines(outObjFileName, connectionInstructions);

    return true;
}

static void HandleParseError(IEnumerable<Error> errs)
{
        if (errs.IsVersion())
    {
            Console.WriteLine("Version Request");
        return;
    }
    if (errs.IsHelp())
    {
            Console.WriteLine("Help Request");
        return;
    }
}