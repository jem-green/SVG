using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ShapeLibrary;
using SVGLibrary;
using static SVGLibrary.PathSegment;

namespace SVGConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SVGDocument svgDocument = new SVGDocument();
            svgDocument.Load("test.svg");
            SHPDocument shpDocument = new SHPDocument();
            foreach (DictionaryEntry kvp in svgDocument)
            {
                Element element = (Element)kvp.Value;
                if (element.GetType() == typeof(Line))
                {
                    Debug.WriteLine("Add Line");
                    Line line = (Line)element;
                    SHPPoint p1 = shpDocument.GetPoint(Convert.ToDouble(line.X1), Convert.ToDouble(line.Y1), 0);
                    SHPPoint p2 = shpDocument.GetPoint(Convert.ToDouble(line.X1), Convert.ToDouble(line.Y1), 0);
                    SHPLine l1 = new SHPLine(p1, p2);
                    Console.WriteLine("Add line " + l1.ToString());
                    shpDocument.AddLine(l1);
                }
                else if (element.GetType() == typeof(Path))
                {
                    Debug.WriteLine("Add Path");
                    SVGLibrary.Path path = (Path)element;
                    List<SVGLibrary.PathSegment.Segment> segments = SVGLibrary.PathSegment.Decode(path.PathData);

                    bool start = true;
                    SVGLibrary.PathSegment.Point point0 = new Point();
                    SVGLibrary.PathSegment.Point point1 = new Point();
                    SVGLibrary.PathSegment.Point point2 = new Point();
                    foreach (SVGLibrary.PathSegment.Segment segment in segments)
                    {
                        switch(segment.Type)
                        {
                            case SVGLibrary.PathSegment.SegmentType.MoveToAbsolute:
                                {
                                    point1 = (Point)segment.Data;
                                    if (start == true)
                                    {
                                        point0 = point1;
                                    }
                                    break;
                                }
                            case SVGLibrary.PathSegment.SegmentType.MoveToRelative:
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
                            case SVGLibrary.PathSegment.SegmentType.LineToAbsolute:
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
                            case SVGLibrary.PathSegment.SegmentType.LineToRelative:
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
                            case SVGLibrary.PathSegment.SegmentType.HorizontalLineToAbsolute:
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
                            case SVGLibrary.PathSegment.SegmentType.HorizontalLineToRelative:
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
                            case SVGLibrary.PathSegment.SegmentType.VerticalLineToAbsolute:
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
                            case SVGLibrary.PathSegment.SegmentType.VerticalLineToRelative:
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
                            case SVGLibrary.PathSegment.SegmentType.ClosePathAbsolute:
                            case SVGLibrary.PathSegment.SegmentType.ClosePathRelative:
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
                else if (element.GetType() == typeof(Rect))
                {
                    Debug.WriteLine("Add Rectangle");
                    Rect rectangle = (Rect)element;
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

            shpDocument.Save("", "test");

            // Test Loading

            shpDocument = new SHPDocument();
            shpDocument.Load("", "test");
            foreach (IShape shape in shpDocument)
            {
                Console.WriteLine("loaded=" + shape.ToString());
            }

        }
    }
}
