using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SVGLibrary
{
    public class Path
    {
        /*
         * svg_path::= wsp* moveto? (moveto drawto_command*)?
         *
         *   drawto_command::=
         *       moveto
         *       | closepath
         *       | lineto
         *       | horizontal_lineto
         *       | vertical_lineto
         *       | curveto
         *       | smooth_curveto
         *       | quadratic_bezier_curveto
         *       | smooth_quadratic_bezier_curveto
         *       | elliptical_arc

         * moveto::= ( "M" | "m" ) wsp* coordinate_pair_sequence
         * closepath::= ("Z" | "z")
         * lineto::= ("L"|"l") wsp* coordinate_pair_sequence
         * horizontal_lineto::= ("H"|"h") wsp* coordinate_sequence
         * vertical_lineto::= ("V"|"v") wsp* coordinate_sequence
         * curveto::= ("C"|"c") wsp* curveto_coordinate_sequence
         * curveto_coordinate_sequence::= coordinate_pair_triplet | (coordinate_pair_triplet comma_wsp? curveto_coordinate_sequence)
         * smooth_curveto::= ("S"|"s") wsp* smooth_curveto_coordinate_sequence
         * smooth_curveto_coordinate_sequence::= coordinate_pair_double | (coordinate_pair_double comma_wsp? smooth_curveto_coordinate_sequence)
         * quadratic_bezier_curveto::= ("Q"|"q") wsp* quadratic_bezier_curveto_coordinate_sequence
         * quadratic_bezier_curveto_coordinate_sequence::= coordinate_pair_double | (coordinate_pair_double comma_wsp? quadratic_bezier_curveto_coordinate_sequence)
         * smooth_quadratic_bezier_curveto::= ("T"|"t") wsp* coordinate_pair_sequence
         * elliptical_arc::= ( "A" | "a" ) wsp* elliptical_arc_argument_sequence
         * 
         * elliptical_arc_argument_sequence::= elliptical_arc_argument | (elliptical_arc_argument comma_wsp? elliptical_arc_argument_sequence)
         * elliptical_arc_argument::= number comma_wsp? number comma_wsp? number comma_wsp flag comma_wsp? flag comma_wsp? coordinate_pair
         * coordinate_pair_double::= coordinate_pair comma_wsp? coordinate_pair
         * coordinate_pair_triplet::= coordinate_pair comma_wsp? coordinate_pair comma_wsp? coordinate_pair
         *
         * coordinate_pair_sequence::= coordinate_pair | (coordinate_pair comma_wsp? coordinate_pair_sequence)
         * coordinate_sequence::= coordinate | (coordinate comma_wsp? coordinate_sequence)
         * coordinate_pair::= coordinate comma_wsp? coordinate 
         * coordinate::= sign? number
         * 
         * sign::= "+"|"-"
         * number ::= ([0-9])+
         * flag::=("0"|"1")
         * comma_wsp::=(wsp+ ","? wsp*) | ("," wsp*)
         * wsp ::= (#x9 | #x20 | #xA | #xC | #xD)
         * 
         */


        #region Fields

        string _pathData = "";
        int _ptr = 0;
        int _nextPtr = 0;
        SegmentType _currentSegment;

        public enum SegmentType
        {
            None = -1,
            Number = 0,
            MoveToAbsolute = 1,
            MoveToRelative = 2,
            ClosePathAbsolute = 3,
            ClosePathRelative = 4,
            LineToAbsolute = 5,
            LineToRelative = 6,
            HorizontalLineToAbsolute = 7,
            HorizontalLineToRelative = 8,
            VerticalLineToAbsolute = 9,
            VerticalLineToRelative = 10,



            Delimiter = 95,
            Space = 96,
            Comma = 97,
            EndOfPath = 99
        }


        public struct Point
        {
            double _x;
            double _y;
            public Point(double x, double y)
            {
                _x = x;
                _y = y;
            }
            public double X
            {
                set
                {
                    _x = value;
                }
                get
                {
                    return (_x);
                }
            }
            public double Y
            {
                set
                {
                    _y = value;
                }
                get
                {
                    return (_y);
                }
            }

            public override string ToString()
            {
                return ("X=" + _x + ",Y=" + _y);
            }
        }

        public struct Segment
        {
            SegmentType _segmentType;
            object _data;
            public Segment(SegmentType segmentType, object data)
            {
                _segmentType = segmentType;
                _data = data;
            }

            public SegmentType Type
            {
                set
                {
                    _segmentType = value;
                }
                get
                {
                    return (_segmentType);
                }
            }

            public object Data
            {
                set
                {
                    _data = value;
                }
                get
                {
                    return (_data);
                }
            }
            public override string ToString()
            {
                return ("type=" + _segmentType.ToString() + ",data=" + _data.ToString());
            }
        }

        #endregion
        #region Constructors

        public Path()
        {
        }

        public Path(string pathData)
        {
            _pathData = pathData;
        }

        #endregion
        #region Peroperties
        #endregion
        #region Methods

        public List<Segment> Decode()
        {
            return (Decode(_pathData));
        }

        public List<Segment> Decode(string pathData)
        {
            List<Segment> segments = new List<Segment>();
            Point point = new Point(0, 0);
            SegmentType type = SegmentType.None;
            int item = 0;
            bool exit = false;
            _pathData = pathData;
            //Segment element;

            NextElement();
            do
            {
                Debug.WriteLine("Decode Loop");
                type = GetElement();
                switch (type)
                {
                    case SegmentType.MoveToRelative:
                        {
                            Debug.WriteLine("Decode MoveToRelative");
                            NextElement();
                            GetCoordinates(type, segments);
                            break;
                        }
                    case SegmentType.MoveToAbsolute:
                        {
                            Debug.WriteLine("Decode MoveToAbsolute");
                            NextElement();
                            GetCoordinates(type, segments);
                            break;
                        }
                    case SegmentType.ClosePathAbsolute:
                    case SegmentType.ClosePathRelative:
                        {
                            Debug.WriteLine("Decode ClosePathAbsolute");
                            break;
                        }
                    default:
                        {
                            throw new NotSupportedException("Not supported " + type.ToString());
                        }
                }
            }
            while (GetElement() != SegmentType.EndOfPath);
            return (segments);
        }

        private void GetCoordinates(SegmentType type, List<Segment> segments)
        {
            Debug.WriteLine("In GetCoordinates()");
            Point point = new Point(0, 0);
            Segment segment;
            do
            {
                Debug.WriteLine("GetCoordinates Loop");
                if (GetElement() == SegmentType.Number)
                {
                    point = new Point();
                    point.X = GetNumber();
                    NextElement();
                    if (GetElement() == SegmentType.Comma)
                    {
                        NextElement();
                        if (GetElement() == SegmentType.Number)
                        {
                            point.Y = GetNumber();
                            segment = new Segment(type, point);
                            segments.Add(segment);
                            Debug.WriteLine("Add point=" + point.ToString());
                            NextElement();
                        }
                        else
                        {
                            throw new Exception("Expected number");
                        }
                    }
                    else
                    {
                        throw new Exception("Expected comma");
                    }
                }
            }
            while (GetElement() == Path.SegmentType.Number);
            Debug.WriteLine("Out GetCoordinates()");
        }


        SegmentType GetElement()
        {
            Debug.WriteLine("Segment=" + _currentSegment);
            return (_currentSegment);
        }

        private void NextElement()
        {
            if ((_ptr + 1 >= _pathData.Length) || (_pathData[_ptr] == '\0'))
            {
                _currentSegment = SegmentType.EndOfPath;
            }
            else
            {
                _ptr = _nextPtr;
                while (_pathData[_ptr] == ' ')
                {
                    ++_ptr;
                }
                if ((_ptr + 1 >= _pathData.Length) || (_pathData[_ptr] == '\0'))
                {
                    _currentSegment = SegmentType.EndOfPath;
                }
                else
                {
                    _currentSegment = GetNextElement();
                }
            }
        }

        private SegmentType GetNextElement()
        {
            SegmentType elementType = SegmentType.None;
            if (_pathData[_ptr] == ',')
            {
                elementType = SegmentType.Comma;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == ' ')
            {
                elementType = SegmentType.Space;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == 'm')
            {
                elementType = SegmentType.MoveToRelative;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == 'M')
            {
                elementType = SegmentType.MoveToAbsolute;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == 'z')
            {
                elementType = SegmentType.ClosePathRelative;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == 'Z')
            {
                elementType = SegmentType.ClosePathAbsolute;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == 'l')
            {
                elementType = SegmentType.LineToRelative;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == 'L')
            {
                elementType = SegmentType.LineToAbsolute;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == 'h')
            {
                elementType = SegmentType.HorizontalLineToRelative;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == 'H')
            {
                elementType = SegmentType.HorizontalLineToAbsolute;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == 'v')
            {
                elementType = SegmentType.VerticalLineToRelative;
                _nextPtr = _ptr + 1;
            }
            else if (_pathData[_ptr] == 'v')
            {
                elementType = SegmentType.VerticalLineToAbsolute;
                _nextPtr = _ptr + 1;
            }
            else if ((_pathData[_ptr] == '.') || (_pathData[_ptr] == '-') || ((_pathData[_ptr]>='0') && (_pathData[_ptr] <= '9')))
            {
                elementType = SegmentType.Number;
                for (int i=0; i<10; i++)
                {
                    if ((_ptr + i) >= _pathData.Length)
                    {
                        _nextPtr = _pathData.Length - 1;
                        break;
                    }
                    else if ((_pathData[_ptr + i] == '.') || (_pathData[_ptr + i] == '-') || ((_pathData[_ptr + i] >= '0') && (_pathData[_ptr + i] <= '9')))
                    {
                        _nextPtr = _ptr + i;
                    }
                    else
                    {
                        _nextPtr = _ptr + i;
                        break;
                    }
                }
            }
            return (elementType);
        }


        public double GetNumber()
        {
            bool terminal = false;
            string data = "";
            double number = 0;
            int i = 0;
            do
            {
                if ((_ptr == _pathData.Length) || (_pathData[_ptr] == '\0'))
                {
                    terminal = true;
                }
                else if ((_pathData[_ptr + i] >= '0') && (_pathData[_ptr + i] >= '0'))
                {
                    data = data + _pathData[_ptr + i];
                    i++;
                }
                else if (_pathData[_ptr + i] == '.')
                {
                    data = data + _pathData[_ptr + i];
                    i++;
                }
                else if (_pathData[_ptr + i] == '-')
                {
                    data = data + _pathData[_ptr + i];
                    i++;
                }
                else
                {
                    terminal = true;
                }
            }
            while ((terminal == false) && ((_ptr + i) < _pathData.Length) && (i < 10));

            try
            {
                number = Convert.ToDouble(data);
            }
            catch
            {
                number = 0;
            }
            return (number);
        }

        #endregion
        #region Private


        #endregion
    }
}
