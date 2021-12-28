using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ShapeLibrary;
using SVGLibrary;
using static SVGLibrary.Path;

namespace SVGConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SVGDocument svgDocument = new SVGDocument();
            svgDocument.LoadFromFile("test.svg");
            SHPDocument shpDocument = new SHPDocument();
            foreach (DictionaryEntry kvp in svgDocument)
            {
                SVGElement element = (SVGElement)kvp.Value;
                if (element.GetType() == typeof(SVGLine))
                {
                    Debug.WriteLine("Add Line");
                    SVGLine line = (SVGLine)element;
                    SHPPoint p1 = shpDocument.GetPoint(Convert.ToDouble(line.X1), Convert.ToDouble(line.Y1), 0);
                    SHPPoint p2 = shpDocument.GetPoint(Convert.ToDouble(line.X1), Convert.ToDouble(line.Y1), 0);
                    SHPLine l1 = new SHPLine(p1, p2);
                    Console.WriteLine("Add line " + l1.ToString());
                    shpDocument.AddLine(l1);
                }
                else if (element.GetType() == typeof(SVGPath))
                {
                    Debug.WriteLine("Add Path");
                    SVGLibrary.SVGPath path = (SVGPath)element;
                    List<Segment> segments = Path.Decode(path.PathData);

                    bool start = true;
                    Path.Point point0 = new Point();
                    Path.Point point1 = new Point();
                    Path.Point point2 = new Point();
                    foreach (Segment segment in segments)
                    {
                        switch(segment.Type)
                        {
                            case Path.SegmentType.MoveToAbsolute:
                                {
                                    point1 = (Point)segment.Data;
                                    if (start == true)
                                    {
                                        point0 = point1;
                                    }
                                    break;
                                }
                            case Path.SegmentType.MoveToRelative:
                                {
                                    Point pointTemp = (Point)segment.Data;
                                    point1.X = point1.X + pointTemp.X;
                                    point1.Y = point1.Y + pointTemp.Y;
                                    if (start == true)
                                    {
                                        point0 = point1;
                                    }
                                    break;
                                }
                            case Path.SegmentType.LineToAbsolute:
                                {
                                    point2 = (Point)segment.Data;
                                    SHPPoint p1 = shpDocument.GetPoint(point1.X, point1.Y, 0);
                                    SHPPoint p2 = shpDocument.GetPoint(point2.X, point2.Y, 0);
                                    SHPLine l1 = new SHPLine(p1, p2);
                                    Console.WriteLine("Add line " + l1.ToString());
                                    shpDocument.AddLine(l1);
                                    point1 = point2;
                                    break;
                                }
                            case Path.SegmentType.LineToRelative:
                                {
                                    Point pointTemp = (Point)segment.Data;
                                    point2.X = point2.X + pointTemp.X;
                                    point2.Y = point2.Y + pointTemp.Y;
                                    SHPPoint p1 = shpDocument.GetPoint(point1.X, point1.Y, 0);
                                    SHPPoint p2 = shpDocument.GetPoint(point2.X, point2.Y, 0);
                                    SHPLine l1 = new SHPLine(p1, p2);
                                    Console.WriteLine("Add line " + l1.ToString());
                                    shpDocument.AddLine(l1);
                                    point1 = point2;
                                    break;
                                }
                            case Path.SegmentType.HorizontalLineToAbsolute:
                                {
                                    Distance distanceTemp = (Distance)segment.Data;
                                    point2.X = distanceTemp.D;
                                    SHPPoint p1 = shpDocument.GetPoint(point1.X, point1.Y, 0);
                                    SHPPoint p2 = shpDocument.GetPoint(point2.X, point2.Y, 0);
                                    SHPLine l1 = new SHPLine(p1, p2);
                                    Console.WriteLine("Add line " + l1.ToString());
                                    shpDocument.AddLine(l1);
                                    point1 = point2;
                                    break;
                                }
                            case Path.SegmentType.HorizontalLineToRelative:
                                {
                                    Distance distanceTemp = (Distance)segment.Data;
                                    point2.X = point2.X + distanceTemp.D;
                                    SHPPoint p1 = shpDocument.GetPoint(point1.X, point1.Y, 0);
                                    SHPPoint p2 = shpDocument.GetPoint(point2.X, point2.Y, 0);
                                    SHPLine l1 = new SHPLine(p1, p2);
                                    Console.WriteLine("Add line " + l1.ToString());
                                    shpDocument.AddLine(l1);
                                    point1 = point2;
                                    break;
                                }
                            case Path.SegmentType.VerticalLineToAbsolute:
                                {
                                    Distance distanceTemp = (Distance)segment.Data;
                                    point2.Y = distanceTemp.D;
                                    SHPPoint p1 = shpDocument.GetPoint(point1.X, point1.Y, 0);
                                    SHPPoint p2 = shpDocument.GetPoint(point2.X, point2.Y, 0);
                                    SHPLine l1 = new SHPLine(p1, p2);
                                    Console.WriteLine("Add line " + l1.ToString());
                                    shpDocument.AddLine(l1);
                                    point1 = point2;
                                    break;
                                }
                            case Path.SegmentType.VerticalLineToRelative:
                                {
                                    Distance distanceTemp = (Distance)segment.Data;
                                    point2.Y = point2.Y + distanceTemp.D;
                                    SHPPoint p1 = shpDocument.GetPoint(point1.X, point1.Y, 0);
                                    SHPPoint p2 = shpDocument.GetPoint(point2.X, point2.Y, 0);
                                    SHPLine l1 = new SHPLine(p1, p2);
                                    Console.WriteLine("Add line " + l1.ToString());
                                    shpDocument.AddLine(l1);
                                    point1 = point2;
                                    break;
                                }
                            case Path.SegmentType.ClosePathAbsolute:
                            case Path.SegmentType.ClosePathRelative:
                                {
                                    SHPPoint p1 = shpDocument.GetPoint(point1.X, point1.Y, 0);
                                    SHPPoint p2 = shpDocument.GetPoint(point0.X, point0.Y, 0);
                                    SHPLine l1 = new SHPLine(p1, p2);
                                    Console.WriteLine("Add line " + l1.ToString());
                                    shpDocument.AddLine(l1);
                                    point1 = point0;
                                    break;
                                }
                            default:
                                {
                                    throw new NotImplementedException("Unkown segment " + segment.Type);
                                }
                        }
                    }

                }
                else if (element.GetType() == typeof(SVGRect))
                {
                    Debug.WriteLine("Add Rectangle");
                    SVGRect rectangle = (SVGRect)element;
                    double x = Convert.ToDouble(rectangle.X);
                    double y = Convert.ToDouble(rectangle.Y);
                    double width = Convert.ToDouble(rectangle.Width);
                    double height = Convert.ToDouble(rectangle.Height);
                    SHPPoint p1 = shpDocument.GetPoint(x, y, 0);
                    SHPPoint p2 = shpDocument.GetPoint(x + width, y, 0);
                    SHPPoint p3 = shpDocument.GetPoint(x + width, y - height, 0);
                    SHPPoint p4 = shpDocument.GetPoint(x, y - height, 0);
                    SHPLine l1 = new SHPLine(p1, p2);
                    SHPLine l2 = new SHPLine(p2, p3);
                    SHPLine l3 = new SHPLine(p3, p4);
                    SHPLine l4 = new SHPLine(p4, p1);
                    shpDocument.AddLine(l1);
                    shpDocument.AddLine(l2);
                    shpDocument.AddLine(l3);
                    shpDocument.AddLine(l4);
                }
                else
                {
                    Debug.WriteLine("Unknow Element " + element.GetType());
                }
            }
        }
    }
}
