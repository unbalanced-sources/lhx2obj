using CommandLine;

namespace lhx2obj{

    public class Options
    {
         [Option('p',"point",Separator = ':', Max = 2, 
        HelpText = "(default: \"S\") Way to process POINT element. (S)traightforward .obj \"point\" record or (G)enerated primitive - a 20-face icosahedron with default radius of 1f. You can provide your own float radius for generated primitive after semicolon."
        )]
        public IEnumerable<string> pointOpts { get; set; }

        [Option('l',"line",Separator = ':', Max = 2,
        HelpText = "(default: \"S\") Way to process LINE element. (S)traightforward .obj \"line\" record or (G)enerated primitive - as a column with default width of 1f. You can provide your own float width for generated primitive after semicolon."
        )]
        public IEnumerable<string> lineOpts { get; set; }

        [Option('s',"sphere", Default = "G",
        HelpText = "(default: \"G\") Way to process SPHERE element. (M)ention as an .obj comment or (G)enerated as a 20-face icosahedron with mentioned radius."
        )]
        public string sphereOpts { get; set; }

        [Option('d',"doubleSide",Separator = ':', Max = 2,
        HelpText = "(default: \"G:0.1f\") Way to process DOUBLE-SIDED FACE element. (S)ingle .obj \"face\" record with random normal or (G)enerated pair of .obj \"face\" records with opposite normals located on default distance of 0.1f along normals from original double-sided polygon. You can provide your own float distance for generated faces after semicolon."
        )]
        public IEnumerable<string> dSideOpts { get; set; }

        [Option('r',"reverseSide", Default = "G",
        HelpText = "(default: \"G\") Way to process REVERSE-NORMALS FACE. It is an usual face, but can share coords with ordinary face that faces opposite direction. (S)kip or (G)enerate."
        )]
        public string revSideOpt { get; set; }

        [Option('y', "yUp", Default = false,
         HelpText = "(default: \"false\") If true, then coordinate system treated as if Y axis is up, otherwise Z axis is up.")]
        public bool yUp { get; set; }


        [Option('f',"infile", Required = true, HelpText = "Path + model name to be processed" ) ]
        public string inModelName { get; set; }

        [Option("outfile", HelpText = "Directory and model name to put processed file. Not required. If skipped - initial will be used." ) ]
        public string outModelName { get; set; }

    }

}