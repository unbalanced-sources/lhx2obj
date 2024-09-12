using Kaitai;
using System.Numerics;

namespace lhx2obj{

    public class AdvLineGenerator : BaseGenerator
    {

        private readonly List<Vector3> _dotCloud;

        private readonly LhxElements.Line _line;

        private readonly float _width;

        public AdvLineGenerator(LhxElements.Element element, List<Vector3> dotCloud, float sideWidth) : base(element)
        {
            _dotCloud = dotCloud;
            _line = (LhxElements.Line)element.Geometry;
            _width = sideWidth/2;
        }

        protected override void GenerateInstructions()
        {
            Vector3 mainVector;
            int startDot, endDot;
            Vector3 v1 = MathHelper.CreateVector(_line.Points[0], _dotCloud);
            Vector3 v2 = MathHelper.CreateVector(_line.Points[1], _dotCloud);
            startDot = _line.Points[0];
            endDot   = _line.Points[1];
            


            // 1. Get vector from line
            mainVector = MathHelper.CreateDiffVector(startDot, endDot, _dotCloud);


            // 2. Get it's length
            float mainVectorLength = mainVector.Length();

            // 3. Create box points (8).
            Vector4 adder = new Vector4(mainVectorLength, 0, 0, 0);
            Vector4[] points = {
                Vector4.UnitY * _width + Vector4.UnitW,           Vector4.UnitZ * _width + Vector4.UnitW, 
               -Vector4.UnitY * _width + Vector4.UnitW,          -Vector4.UnitZ * _width + Vector4.UnitW, 
                Vector4.UnitY * _width + adder + Vector4.UnitW,   Vector4.UnitZ * _width + adder + Vector4.UnitW, 
               -Vector4.UnitY * _width + adder + Vector4.UnitW,  -Vector4.UnitZ * _width + adder + Vector4.UnitW 
            };

            /*
            4. Get angles (normalize vectors!)
            4.1. planeA -> xy0 : x=1
            4.2. totalA -> xyz : xy0 */
            Vector3 xy0Vector = new Vector3(mainVector.X, mainVector.Y, 0);

            // angle between line vector and it's projection vector to 0xy plane
            double totalA = Math.Acos(Vector3.Dot( Vector3.Normalize(mainVector) , Vector3.Normalize(xy0Vector)));
            if (double.IsNaN(totalA))
            {
                totalA = (mainVector.Z >= 0) ? 1.5708 : -1.5708;
                if (Vector3.Dot( Vector3.Normalize(mainVector) , Vector3.Normalize(xy0Vector)) > 1) totalA = Math.Acos(1);
                if (Vector3.Dot( Vector3.Normalize(mainVector) , Vector3.Normalize(xy0Vector)) < -1) totalA = Math.Acos(-1);
                
                
            }
            else
            {
                totalA = ( mainVector.Z >= 0) ? totalA : - totalA;
            }

            //plane angle between line projection vector and 0x unit vector
            double planeA = Math.Acos(Vector3.Dot( Vector3.Normalize(xy0Vector) , Vector3.UnitX));
            if (double.IsNaN(planeA))
            {
                planeA = (mainVector.Y >= 0) ? 1.5708 : -1.5708;
            } 
            else
            {
                planeA = ( mainVector.Y >= 0) ? planeA : -planeA; 
            }
            
            

             

            //5. Rotate all points
            //6. Translate all points

            Matrix4x4 rotateY = new Matrix4x4((float)Math.Cos(-totalA),  0,  (float)-Math.Sin(-totalA),   0,
                                              0,                     1,  0,                               0,         
                                              (float)Math.Sin(-totalA), 0,  (float)Math.Cos(-totalA),    0,
                                              0,                      0, 0,                                1 );

            Matrix4x4 rotateZ = new Matrix4x4((float)Math.Cos(planeA), (float)Math.Sin(planeA), 0,  0,
                                              (float)-Math.Sin(planeA),(float)Math.Cos(planeA), 0,  0,
                                              0,                    0,                          1,  0,
                                              0,                    0,                          0,  1);

            float dx = _dotCloud.ElementAt(startDot).X, 
                  dy = _dotCloud.ElementAt(startDot).Y,  
                  dz = _dotCloud.ElementAt(startDot).Z;

            Matrix4x4 moveXYZ  = new Matrix4x4( 1,  0,  0,  0,
                                                0,  1,  0,  0,
                                                0,  0,  1,  0,
                                                dx, dy, dz, 1  );

            foreach (Vector4 dot in points)
            {
                Vector4 result;
                // result = dot * _width;
                // rotate from 0x to 0z to the angle, equal to angle between line vector and
                // it's projection vector to 0xy plane (total angle)
                result = Vector4.Transform(dot, rotateY);
                // rotate around Z to final position. equal to plane angle between line projection
                // vector and 0x unit vector
                result = Vector4.Transform(result, rotateZ);
                //translate to the start of line
                result = Vector4.Transform(result, moveXYZ);

                _generatedInstructions.Add("v " + " " + result.X + " " + result.Y + " " + result.Z );

            }
            
            _generatedInstructions.Add("f -7 -3 -4 -8");
            _generatedInstructions.Add("f -8 -4 -1 -5");
            _generatedInstructions.Add("f -5 -1 -2 -6");
            _generatedInstructions.Add("f -6 -2 -3 -7");

        }
    }
}