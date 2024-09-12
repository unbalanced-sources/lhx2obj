// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using System.Collections.Generic;

namespace Kaitai
{
    public partial class LhxElements : KaitaiStruct
    {
        public static LhxElements FromFile(string fileName)
        {
            return new LhxElements(new KaitaiStream(fileName));
        }


        public enum TypeOfElement
        {
            DoubleSidedPolygon = 0,
            SeparateLine = 1,
            Point = 2,
            Sphere = 3,
            StrangePointer = 4,
            HiddenPolygon = 8,
            SingleSidedPolygon = 16,
            OnPolygonLine = 17,
            ReversedNormalsPolygon = 24,
        }
        public LhxElements(KaitaiStream p__io, KaitaiStruct p__parent = null, LhxElements p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _lowModelChunk = new LowModelConnection(m_io, this, m_root);
            _chunks = new List<Chunk>();
            {
                var i = 0;
                while (!m_io.IsEof) {
                    _chunks.Add(new Chunk(m_io, this, m_root));
                    i++;
                }
            }
        }
        public partial class Chunk : KaitaiStruct
        {
            public static Chunk FromFile(string fileName)
            {
                return new Chunk(new KaitaiStream(fileName));
            }

            public Chunk(KaitaiStream p__io, LhxElements p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _leadByte = m_io.ReadU1();
                switch (LeadByte) {
                case 0: {
                    _data = new Alias(m_io, this, m_root);
                    break;
                }
                case 24: {
                    _data = new Alias(m_io, this, m_root);
                    break;
                }
                case 7: {
                    _data = new StrangeGarb2(m_io, this, m_root);
                    break;
                }
                case 3: {
                    _data = new Group(m_io, this, m_root);
                    break;
                }
                case 16: {
                    _data = new Alias(m_io, this, m_root);
                    break;
                }
                default: {
                    _data = new MedModelConnection(m_io, this, m_root);
                    break;
                }
                }
            }
            private byte _leadByte;
            private KaitaiStruct _data;
            private LhxElements m_root;
            private LhxElements m_parent;
            public byte LeadByte { get { return _leadByte; } }
            public KaitaiStruct Data { get { return _data; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements M_Parent { get { return m_parent; } }
        }
        public partial class Point : KaitaiStruct
        {
            public static Point FromFile(string fileName)
            {
                return new Point(new KaitaiStream(fileName));
            }

            public Point(KaitaiStream p__io, LhxElements.Element p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _pointNumOfPoint = m_io.ReadU1();
            }
            private byte _pointNumOfPoint;
            private LhxElements m_root;
            private LhxElements.Element m_parent;
            public byte PointNumOfPoint { get { return _pointNumOfPoint; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements.Element M_Parent { get { return m_parent; } }
        }
        public partial class Polygon : KaitaiStruct
        {
            public static Polygon FromFile(string fileName)
            {
                return new Polygon(new KaitaiStream(fileName));
            }

            public Polygon(KaitaiStream p__io, LhxElements.Element p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _strange3 = m_io.ReadU1();
                _numOfPoints = m_io.ReadU1();
                _points = new List<byte>((int) (NumOfPoints));
                for (var i = 0; i < NumOfPoints; i++)
                {
                    _points.Add(m_io.ReadU1());
                }
            }
            private byte _strange3;
            private byte _numOfPoints;
            private List<byte> _points;
            private LhxElements m_root;
            private LhxElements.Element m_parent;
            public byte Strange3 { get { return _strange3; } }
            public byte NumOfPoints { get { return _numOfPoints; } }
            public List<byte> Points { get { return _points; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements.Element M_Parent { get { return m_parent; } }
        }
        public partial class Line : KaitaiStruct
        {
            public static Line FromFile(string fileName)
            {
                return new Line(new KaitaiStream(fileName));
            }

            public Line(KaitaiStream p__io, LhxElements.Element p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _points = new List<byte>((int) (2));
                for (var i = 0; i < 2; i++)
                {
                    _points.Add(m_io.ReadU1());
                }
            }
            private List<byte> _points;
            private LhxElements m_root;
            private LhxElements.Element m_parent;
            public List<byte> Points { get { return _points; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements.Element M_Parent { get { return m_parent; } }
        }
        public partial class StrangeGarb2 : KaitaiStruct
        {
            public static StrangeGarb2 FromFile(string fileName)
            {
                return new StrangeGarb2(new KaitaiStream(fileName));
            }

            public StrangeGarb2(KaitaiStream p__io, LhxElements.Chunk p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _numOfIds = m_io.ReadU1();
                _ids = new List<ushort>((int) (NumOfIds));
                for (var i = 0; i < NumOfIds; i++)
                {
                    _ids.Add(m_io.ReadU2le());
                }
                _zeroes = new List<byte>((int) (6));
                for (var i = 0; i < 6; i++)
                {
                    _zeroes.Add(m_io.ReadU1());
                }
                _strangeNumbers = new List<byte>((int) (3));
                for (var i = 0; i < 3; i++)
                {
                    _strangeNumbers.Add(m_io.ReadU1());
                }
            }
            private byte _numOfIds;
            private List<ushort> _ids;
            private List<byte> _zeroes;
            private List<byte> _strangeNumbers;
            private LhxElements m_root;
            private LhxElements.Chunk m_parent;
            public byte NumOfIds { get { return _numOfIds; } }
            public List<ushort> Ids { get { return _ids; } }
            public List<byte> Zeroes { get { return _zeroes; } }
            public List<byte> StrangeNumbers { get { return _strangeNumbers; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements.Chunk M_Parent { get { return m_parent; } }
        }
        public partial class Sphere : KaitaiStruct
        {
            public static Sphere FromFile(string fileName)
            {
                return new Sphere(new KaitaiStream(fileName));
            }

            public Sphere(KaitaiStream p__io, LhxElements.Element p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _radius = m_io.ReadU2le();
                _pointOfCenter = m_io.ReadU1();
            }
            private ushort _radius;
            private byte _pointOfCenter;
            private LhxElements m_root;
            private LhxElements.Element m_parent;
            public ushort Radius { get { return _radius; } }
            public byte PointOfCenter { get { return _pointOfCenter; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements.Element M_Parent { get { return m_parent; } }
        }
        public partial class StrangeGarb1 : KaitaiStruct
        {
            public static StrangeGarb1 FromFile(string fileName)
            {
                return new StrangeGarb1(new KaitaiStream(fileName));
            }

            public StrangeGarb1(KaitaiStream p__io, KaitaiStruct p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _twoZeroes = m_io.ReadU2le();
                _ids = new List<byte>((int) (5));
                for (var i = 0; i < 5; i++)
                {
                    _ids.Add(m_io.ReadU1());
                }
            }
            private ushort _twoZeroes;
            private List<byte> _ids;
            private LhxElements m_root;
            private KaitaiStruct m_parent;
            public ushort TwoZeroes { get { return _twoZeroes; } }
            public List<byte> Ids { get { return _ids; } }
            public LhxElements M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Element : KaitaiStruct
        {
            public static Element FromFile(string fileName)
            {
                return new Element(new KaitaiStream(fileName));
            }

            public Element(KaitaiStream p__io, KaitaiStruct p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _elementType = ((LhxElements.TypeOfElement) m_io.ReadU1());
                _somePlaneElementId1 = m_io.ReadU2le();
                _elementColorCode = m_io.ReadU1();
                _opaqueness = m_io.ReadU1();
                switch (ElementType) {
                case LhxElements.TypeOfElement.Point: {
                    _geometry = new Point(m_io, this, m_root);
                    break;
                }
                case LhxElements.TypeOfElement.DoubleSidedPolygon: {
                    _geometry = new Polygon(m_io, this, m_root);
                    break;
                }
                case LhxElements.TypeOfElement.SeparateLine: {
                    _geometry = new Line(m_io, this, m_root);
                    break;
                }
                case LhxElements.TypeOfElement.OnPolygonLine: {
                    _geometry = new Line(m_io, this, m_root);
                    break;
                }
                case LhxElements.TypeOfElement.ReversedNormalsPolygon: {
                    _geometry = new Polygon(m_io, this, m_root);
                    break;
                }
                case LhxElements.TypeOfElement.StrangePointer: {
                    _geometry = new Sp(m_io, this, m_root);
                    break;
                }
                case LhxElements.TypeOfElement.SingleSidedPolygon: {
                    _geometry = new Polygon(m_io, this, m_root);
                    break;
                }
                case LhxElements.TypeOfElement.Sphere: {
                    _geometry = new Sphere(m_io, this, m_root);
                    break;
                }
                case LhxElements.TypeOfElement.HiddenPolygon: {
                    _geometry = new Polygon(m_io, this, m_root);
                    break;
                }
                }
            }
            private TypeOfElement _elementType;
            private ushort _somePlaneElementId1;
            private byte _elementColorCode;
            private byte _opaqueness;
            private KaitaiStruct _geometry;
            private LhxElements m_root;
            private KaitaiStruct m_parent;
            public TypeOfElement ElementType { get { return _elementType; } }
            public ushort SomePlaneElementId1 { get { return _somePlaneElementId1; } }
            public byte ElementColorCode { get { return _elementColorCode; } }

            /// <summary>
            /// usually 255
            /// </summary>
            public byte Opaqueness { get { return _opaqueness; } }
            public KaitaiStruct Geometry { get { return _geometry; } }
            public LhxElements M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Alias : KaitaiStruct
        {
            public static Alias FromFile(string fileName)
            {
                return new Alias(new KaitaiStream(fileName));
            }

            public Alias(KaitaiStream p__io, LhxElements.Chunk p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _realId = m_io.ReadU2le();
                _aliasId1 = m_io.ReadU2le();
                _aliasId2 = m_io.ReadU2le();
            }
            private ushort _realId;
            private ushort _aliasId1;
            private ushort _aliasId2;
            private LhxElements m_root;
            private LhxElements.Chunk m_parent;
            public ushort RealId { get { return _realId; } }
            public ushort AliasId1 { get { return _aliasId1; } }
            public ushort AliasId2 { get { return _aliasId2; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements.Chunk M_Parent { get { return m_parent; } }
        }
        public partial class MedModelConnection : KaitaiStruct
        {
            public static MedModelConnection FromFile(string fileName)
            {
                return new MedModelConnection(new KaitaiStream(fileName));
            }

            public MedModelConnection(KaitaiStream p__io, LhxElements.Chunk p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _numOfPoints = m_io.ReadU1();
                _pointsAddressPrefix = m_io.ReadU4le();
                _pointcloudAddress = new Address(m_io, this, m_root);
                _strange1 = m_io.ReadU2le();
                _numOfPolygons = m_io.ReadU1();
                _strange2 = m_io.ReadU1();
                _elements = new List<Element>((int) (M_Parent.LeadByte));
                for (var i = 0; i < M_Parent.LeadByte; i++)
                {
                    _elements.Add(new Element(m_io, this, m_root));
                }
            }
            private byte _numOfPoints;
            private uint _pointsAddressPrefix;
            private Address _pointcloudAddress;
            private ushort _strange1;
            private byte _numOfPolygons;
            private byte _strange2;
            private List<Element> _elements;
            private LhxElements m_root;
            private LhxElements.Chunk m_parent;
            public byte NumOfPoints { get { return _numOfPoints; } }

            /// <summary>
            /// usually 00 00 00 00
            /// </summary>
            public uint PointsAddressPrefix { get { return _pointsAddressPrefix; } }

            /// <summary>
            /// if zeroes - points are in the separate file
            /// </summary>
            public Address PointcloudAddress { get { return _pointcloudAddress; } }
            public ushort Strange1 { get { return _strange1; } }
            public byte NumOfPolygons { get { return _numOfPolygons; } }
            public byte Strange2 { get { return _strange2; } }
            public List<Element> Elements { get { return _elements; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements.Chunk M_Parent { get { return m_parent; } }
        }
        public partial class Address : KaitaiStruct
        {
            public static Address FromFile(string fileName)
            {
                return new Address(new KaitaiStream(fileName));
            }

            public Address(KaitaiStream p__io, KaitaiStruct p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _segment = m_io.ReadU2le();
                _offset = m_io.ReadU2le();
            }
            private ushort _segment;
            private ushort _offset;
            private LhxElements m_root;
            private KaitaiStruct m_parent;
            public ushort Segment { get { return _segment; } }
            public ushort Offset { get { return _offset; } }
            public LhxElements M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Sp : KaitaiStruct
        {
            public static Sp FromFile(string fileName)
            {
                return new Sp(new KaitaiStream(fileName));
            }

            public Sp(KaitaiStream p__io, LhxElements.Element p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _id1 = m_io.ReadU1();
                _id2 = m_io.ReadU1();
                _pointOfCenter = m_io.ReadU1();
            }
            private byte _id1;
            private byte _id2;
            private byte _pointOfCenter;
            private LhxElements m_root;
            private LhxElements.Element m_parent;
            public byte Id1 { get { return _id1; } }
            public byte Id2 { get { return _id2; } }
            public byte PointOfCenter { get { return _pointOfCenter; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements.Element M_Parent { get { return m_parent; } }
        }
        public partial class LowModelConnection : KaitaiStruct
        {
            public static LowModelConnection FromFile(string fileName)
            {
                return new LowModelConnection(new KaitaiStream(fileName));
            }

            public LowModelConnection(KaitaiStream p__io, LhxElements p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _numOfElements = m_io.ReadU1();
                _numOfPoints = m_io.ReadU1();
                _pointsAddressPrefix = m_io.ReadU4le();
                _pointcloudAddress = new Address(m_io, this, m_root);
                _strange1 = m_io.ReadU2le();
                _numOfPolygons = m_io.ReadU1();
                _strange2 = m_io.ReadU1();
                _elements = new List<Element>((int) (NumOfElements));
                for (var i = 0; i < NumOfElements; i++)
                {
                    _elements.Add(new Element(m_io, this, m_root));
                }
            }
            private byte _numOfElements;
            private byte _numOfPoints;
            private uint _pointsAddressPrefix;
            private Address _pointcloudAddress;
            private ushort _strange1;
            private byte _numOfPolygons;
            private byte _strange2;
            private List<Element> _elements;
            private LhxElements m_root;
            private LhxElements m_parent;
            public byte NumOfElements { get { return _numOfElements; } }
            public byte NumOfPoints { get { return _numOfPoints; } }

            /// <summary>
            /// usually 00 00 00 00
            /// </summary>
            public uint PointsAddressPrefix { get { return _pointsAddressPrefix; } }

            /// <summary>
            /// if zeroes - points are in the separate file
            /// </summary>
            public Address PointcloudAddress { get { return _pointcloudAddress; } }
            public ushort Strange1 { get { return _strange1; } }
            public byte NumOfPolygons { get { return _numOfPolygons; } }
            public byte Strange2 { get { return _strange2; } }
            public List<Element> Elements { get { return _elements; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements M_Parent { get { return m_parent; } }
        }
        public partial class Group : KaitaiStruct
        {
            public static Group FromFile(string fileName)
            {
                return new Group(new KaitaiStream(fileName));
            }

            public Group(KaitaiStream p__io, LhxElements.Chunk p__parent = null, LhxElements p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _numOfGroupedIds = m_io.ReadU1();
                _groupedId = new List<ushort>((int) (NumOfGroupedIds));
                for (var i = 0; i < NumOfGroupedIds; i++)
                {
                    _groupedId.Add(m_io.ReadU2le());
                }
            }
            private byte _numOfGroupedIds;
            private List<ushort> _groupedId;
            private LhxElements m_root;
            private LhxElements.Chunk m_parent;
            public byte NumOfGroupedIds { get { return _numOfGroupedIds; } }
            public List<ushort> GroupedId { get { return _groupedId; } }
            public LhxElements M_Root { get { return m_root; } }
            public LhxElements.Chunk M_Parent { get { return m_parent; } }
        }
        private LowModelConnection _lowModelChunk;
        private List<Chunk> _chunks;
        private LhxElements m_root;
        private KaitaiStruct m_parent;
        public LowModelConnection LowModelChunk { get { return _lowModelChunk; } }
        public List<Chunk> Chunks { get { return _chunks; } }
        public LhxElements M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
