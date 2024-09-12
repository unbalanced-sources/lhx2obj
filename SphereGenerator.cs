using Kaitai;
using System.Numerics;

namespace lhx2obj
{
    public class SphereGenerator : BaseGenerator
    {
        
        private readonly (float, float, float) [] _unitIcoPoints = 
        {
            (0.0f, 0.0f, -1.0f), (-0.89443f, 0.0f, -0.447215f), (-0.276394f, -0.850653f, -0.447215f), 
            (0.723609f, -0.525733f, -0.447215f), (0.723609f, 0.525733f, -0.447215f), (-0.276394f, 0.850653f, -0.447215f), 
            (-0.723609f, -0.525733f, 0.447215f), (0.276394f, -0.850653f, 0.447215f), (0.89443f, 0.0f, 0.447215f), 
            (0.276394f, 0.850653f, 0.447215f), (-0.723609f, 0.525733f, 0.447215f), (0.0f, 0.0f, 1.0f)
        };

        private readonly (int, int, int) [] _unitIcoLinks = 
        {
            (1,2,3), (1,3,4), (1,4,5), (1,5,6), (1,6,2), 
            (2,11,7), (2,7,3), (3,7,8), (3,8,4), (4,8,9), (4,9,5), (5,9,10), (5,10,6), (6,10,11), (6,11,2), 
            (12,8,7), (12,9,8), (12,10,9), (12,11,10), (12,7,11)
        };

        private readonly List<Vector3> _dotCloud;

        private readonly Vector3 _sphereCenter;
        private readonly float _sphereRadius;

        public SphereGenerator(LhxElements.Element element, List<Vector3> dotCloud) : base(element)
        {
            _dotCloud = dotCloud;
            LhxElements.Sphere _sphere = (LhxElements.Sphere)_element.Geometry;
            _sphereRadius = _sphere.Radius;
            _sphereCenter = _dotCloud.ElementAt(_sphere.PointOfCenter);
        }

        /**
        * Overload with Point type
        */
        public SphereGenerator(LhxElements.Element element, float radius, List<Vector3> dotCloud) : base(element)
        {
            _dotCloud = dotCloud;
            LhxElements.Point _point = (LhxElements.Point)_element.Geometry;
            _sphereCenter = _dotCloud.ElementAt(_point.PointNumOfPoint);
            _sphereRadius = radius;
        }

        protected override void GenerateInstructions()
        {
            //float radius = _sphere.Radius;
            //Vector3 center = MathHelper.Vector3FromDot(_dotCloud.Dots[_sphere.PointOfCenter]);
            Vector3 triC;
            


            //generating points
            foreach ((int first, int second, int third) trianglePointNums in _unitIcoLinks)
            {
                triC = Vector3.Add(Vector3.Multiply(MathHelper.Vector3FromTuple(_unitIcoPoints[ trianglePointNums.first-1 ]), _sphereRadius), _sphereCenter);
                _generatedInstructions.Add("v " + " " + triC.X + " " + triC.Y + " " + triC.Z);
                triC = Vector3.Add(Vector3.Multiply(MathHelper.Vector3FromTuple(_unitIcoPoints[ trianglePointNums.second-1 ]), _sphereRadius), _sphereCenter);
                _generatedInstructions.Add("v " + " " + triC.X + " " + triC.Y + " " + triC.Z);
                triC = Vector3.Add(Vector3.Multiply(MathHelper.Vector3FromTuple(_unitIcoPoints[ trianglePointNums.third-1 ]), _sphereRadius), _sphereCenter);
                _generatedInstructions.Add("v " + " " + triC.X + " " + triC.Y + " " + triC.Z);

                _generatedInstructions.Add("f" + " " + (-1) + " "  + (-2) + " " + (-3) );
            }

        }



    }

}