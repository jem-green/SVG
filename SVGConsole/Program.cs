using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ShapeLibrary;
using SVGLibrary;

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
                    shpDocument.AddLine(l1);
                }
                else if (element.GetType() == typeof(SVGPath))
                {
                    Debug.WriteLine("Add Path");
                    SVGLibrary.SVGPath path = (SVGPath)element;
                    
                    string[] pathData = path.PathData.Split(' ');


                    int i = 0;
                    SHPPoint p1 = shpDocument.GetPoint(Convert.ToDouble(pathData[i]), Convert.ToDouble(pathData[i + 1]), 0);
                    for (i = 2; i < pathData.Length; i = i + 2)
                    {
                        SHPPoint p2 = shpDocument.GetPoint(Convert.ToDouble(pathData[i]), Convert.ToDouble(pathData[i + 1]), 0);
                        SHPLine l1 = new SHPLine(p1, p2);
                        shpDocument.AddLine(l1);
                        p1 = p2;
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
                    Debug.WriteLine(element.GetType());
                }
            }
        }
    }
}
