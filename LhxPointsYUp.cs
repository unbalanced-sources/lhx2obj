// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using System.Collections.Generic;

namespace Kaitai
{
    public partial class LhxPointsYUp : KaitaiStruct
    {
        public static LhxPointsYUp FromFile(string fileName)
        {
            return new LhxPointsYUp(new KaitaiStream(fileName));
        }

        public LhxPointsYUp(KaitaiStream p__io, KaitaiStruct p__parent = null, LhxPointsYUp p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _header = new HeaderInfo(m_io, this, m_root);
            __raw_dotcloudLow = m_io.ReadBytes((Header.Set1EndOffset - Header.Set1StartOffset));
            var io___raw_dotcloudLow = new KaitaiStream(__raw_dotcloudLow);
            _dotcloudLow = new Dotcloud(io___raw_dotcloudLow, this, m_root);
            if ( ((Header.Set1BspEndOffset == 0) && (Header.Set2StartOffset == 0)) ) {
                _dotcloudLowBsp1 = new List<byte>();
                {
                    var i = 0;
                    while (!m_io.IsEof) {
                        _dotcloudLowBsp1.Add(m_io.ReadU1());
                        i++;
                    }
                }
            }
            if (Header.Set1BspEndOffset != 0) {
                _dotcloudLowBsp2 = new List<byte>((int) ((Header.Set1BspEndOffset - Header.Set1EndOffset)));
                for (var i = 0; i < (Header.Set1BspEndOffset - Header.Set1EndOffset); i++)
                {
                    _dotcloudLowBsp2.Add(m_io.ReadU1());
                }
            }
            if ( ((Header.Set1BspEndOffset == 0) && (Header.Set2StartOffset != 0)) ) {
                _dotcloudLowBsp3 = new List<byte>((int) ((Header.Set2StartOffset - Header.Set1EndOffset)));
                for (var i = 0; i < (Header.Set2StartOffset - Header.Set1EndOffset); i++)
                {
                    _dotcloudLowBsp3.Add(m_io.ReadU1());
                }
            }
            if ( ((Header.Set1BspEndOffset != 0) && (Header.Set2StartOffset == 0)) ) {
                _dotcloudLowAddon2 = new List<byte>();
                {
                    var i = 0;
                    while (!m_io.IsEof) {
                        _dotcloudLowAddon2.Add(m_io.ReadU1());
                        i++;
                    }
                }
            }
            if ( ((Header.Set1BspEndOffset != 0) && (Header.Set2StartOffset != 0)) ) {
                _dotcloudLowAddon3 = new List<byte>((int) ((Header.Set2StartOffset - Header.Set1BspEndOffset)));
                for (var i = 0; i < (Header.Set2StartOffset - Header.Set1BspEndOffset); i++)
                {
                    _dotcloudLowAddon3.Add(m_io.ReadU1());
                }
            }
            if (Header.Set2StartOffset != 0) {
                __raw_dotcloudMed = m_io.ReadBytes((Header.Set2EndOffset - Header.Set2StartOffset));
                var io___raw_dotcloudMed = new KaitaiStream(__raw_dotcloudMed);
                _dotcloudMed = new Dotcloud(io___raw_dotcloudMed, this, m_root);
            }
            if ( ((Header.Set2StartOffset != 0) && (Header.Set2BspEndOffset == 0)) ) {
                _dotcloudMedBsp2 = new List<byte>();
                {
                    var i = 0;
                    while (!m_io.IsEof) {
                        _dotcloudMedBsp2.Add(m_io.ReadU1());
                        i++;
                    }
                }
            }
            if ( ((Header.Set2StartOffset != 0) && (Header.Set2BspEndOffset != 0)) ) {
                _dotcloudMedBsp3 = new List<byte>((int) ((Header.Set2BspEndOffset - Header.Set2EndOffset)));
                for (var i = 0; i < (Header.Set2BspEndOffset - Header.Set2EndOffset); i++)
                {
                    _dotcloudMedBsp3.Add(m_io.ReadU1());
                }
            }
            if ( ((Header.Set2StartOffset != 0) && (Header.Set2BspEndOffset != 0)) ) {
                _dotcloudMedAddon2 = new List<byte>();
                {
                    var i = 0;
                    while (!m_io.IsEof) {
                        _dotcloudMedAddon2.Add(m_io.ReadU1());
                        i++;
                    }
                }
            }
        }
        public partial class HeaderInfo : KaitaiStruct
        {
            public static HeaderInfo FromFile(string fileName)
            {
                return new HeaderInfo(new KaitaiStream(fileName));
            }

            public HeaderInfo(KaitaiStream p__io, LhxPointsYUp p__parent = null, LhxPointsYUp p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _zerostart = m_io.ReadU2le();
                _set1StartOffset = m_io.ReadU2le();
                _set2StartOffset = m_io.ReadU2le();
                _set1EndOffset = m_io.ReadU2le();
                _set2EndOffset = m_io.ReadU2le();
                _set1BspEndOffset = m_io.ReadU2le();
                _set2BspEndOffset = m_io.ReadU2le();
                _strange3 = m_io.ReadU2le();
                _strange4 = m_io.ReadU2le();
            }
            private ushort _zerostart;
            private ushort _set1StartOffset;
            private ushort _set2StartOffset;
            private ushort _set1EndOffset;
            private ushort _set2EndOffset;
            private ushort _set1BspEndOffset;
            private ushort _set2BspEndOffset;
            private ushort _strange3;
            private ushort _strange4;
            private LhxPointsYUp m_root;
            private LhxPointsYUp m_parent;
            public ushort Zerostart { get { return _zerostart; } }

            /// <summary>
            /// Number of bytes to skip from the beginning of file.
            /// </summary>
            public ushort Set1StartOffset { get { return _set1StartOffset; } }

            /// <summary>
            /// Number of bytes to skip from the beginning of file. Equals zero if there is no set 2
            /// </summary>
            public ushort Set2StartOffset { get { return _set2StartOffset; } }

            /// <summary>
            /// Number of bytes to skip from the beginning of file.
            /// </summary>
            public ushort Set1EndOffset { get { return _set1EndOffset; } }

            /// <summary>
            /// Number of bytes to skip from the beginning of file. Equals zero if there is no set 2
            /// </summary>
            public ushort Set2EndOffset { get { return _set2EndOffset; } }
            public ushort Set1BspEndOffset { get { return _set1BspEndOffset; } }
            public ushort Set2BspEndOffset { get { return _set2BspEndOffset; } }
            public ushort Strange3 { get { return _strange3; } }
            public ushort Strange4 { get { return _strange4; } }
            public LhxPointsYUp M_Root { get { return m_root; } }
            public LhxPointsYUp M_Parent { get { return m_parent; } }
        }
        public partial class Dotcloud : KaitaiStruct
        {
            public static Dotcloud FromFile(string fileName)
            {
                return new Dotcloud(new KaitaiStream(fileName));
            }

            public Dotcloud(KaitaiStream p__io, LhxPointsYUp p__parent = null, LhxPointsYUp p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _dots = new List<Dot>();
                {
                    var i = 0;
                    while (!m_io.IsEof) {
                        _dots.Add(new Dot(m_io, this, m_root));
                        i++;
                    }
                }
            }
            private List<Dot> _dots;
            private LhxPointsYUp m_root;
            private LhxPointsYUp m_parent;
            public List<Dot> Dots { get { return _dots; } }
            public LhxPointsYUp M_Root { get { return m_root; } }
            public LhxPointsYUp M_Parent { get { return m_parent; } }
        }
        public partial class Dot : KaitaiStruct
        {
            public static Dot FromFile(string fileName)
            {
                return new Dot(new KaitaiStream(fileName));
            }

            public Dot(KaitaiStream p__io, LhxPointsYUp.Dotcloud p__parent = null, LhxPointsYUp p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _x = m_io.ReadS2le();
                _y = m_io.ReadS2le();
                _z = m_io.ReadS2le();
            }
            private short _x;
            private short _y;
            private short _z;
            private LhxPointsYUp m_root;
            private LhxPointsYUp.Dotcloud m_parent;
            public short X { get { return _x; } }
            public short Y { get { return _y; } }
            public short Z { get { return _z; } }
            public LhxPointsYUp M_Root { get { return m_root; } }
            public LhxPointsYUp.Dotcloud M_Parent { get { return m_parent; } }
        }
        private HeaderInfo _header;
        private Dotcloud _dotcloudLow;
        private List<byte> _dotcloudLowBsp1;
        private List<byte> _dotcloudLowBsp2;
        private List<byte> _dotcloudLowBsp3;
        private List<byte> _dotcloudLowAddon2;
        private List<byte> _dotcloudLowAddon3;
        private Dotcloud _dotcloudMed;
        private List<byte> _dotcloudMedBsp2;
        private List<byte> _dotcloudMedBsp3;
        private List<byte> _dotcloudMedAddon2;
        private LhxPointsYUp m_root;
        private KaitaiStruct m_parent;
        private byte[] __raw_dotcloudLow;
        private byte[] __raw_dotcloudMed;
        public HeaderInfo Header { get { return _header; } }
        public Dotcloud DotcloudLow { get { return _dotcloudLow; } }

        /// <summary>
        /// bsp 1 till the end of file -&gt; s110s200
        /// </summary>
        public List<byte> DotcloudLowBsp1 { get { return _dotcloudLowBsp1; } }

        /// <summary>
        /// bsp 1 between set 1 and addon 1 -&gt; s111s200 s111s210 s111s210
        /// </summary>
        public List<byte> DotcloudLowBsp2 { get { return _dotcloudLowBsp2; } }

        /// <summary>
        /// bsp 1 between s1 and s2 -&gt; s110s210 s110s211
        /// </summary>
        public List<byte> DotcloudLowBsp3 { get { return _dotcloudLowBsp3; } }

        /// <summary>
        /// addon 1 till end of file -&gt; s111s200
        /// </summary>
        public List<byte> DotcloudLowAddon2 { get { return _dotcloudLowAddon2; } }

        /// <summary>
        /// addon between bsp 1 and set 2 -&gt; s111s210 s111s211
        /// </summary>
        public List<byte> DotcloudLowAddon3 { get { return _dotcloudLowAddon3; } }
        public Dotcloud DotcloudMed { get { return _dotcloudMed; } }

        /// <summary>
        /// bsp 2 till end of file -&gt; s110s210 s111s210
        /// </summary>
        public List<byte> DotcloudMedBsp2 { get { return _dotcloudMedBsp2; } }

        /// <summary>
        /// bsp 2 between set 2 and addon 2
        /// </summary>
        public List<byte> DotcloudMedBsp3 { get { return _dotcloudMedBsp3; } }

        /// <summary>
        /// addon 2 till end of file -&gt; s110s211 s111s211
        /// </summary>
        public List<byte> DotcloudMedAddon2 { get { return _dotcloudMedAddon2; } }
        public LhxPointsYUp M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
        public byte[] M_RawDotcloudLow { get { return __raw_dotcloudLow; } }
        public byte[] M_RawDotcloudMed { get { return __raw_dotcloudMed; } }
    }
}
