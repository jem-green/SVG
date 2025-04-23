using System;
using System.Collections.Generic;
using System.Diagnostics;
using SVGLibrary;
using static SVGLibrary.PathSegment;

namespace SVGTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // test.svg

            Console.WriteLine("---");
            Console.WriteLine("-> 1");
            List<SVGLibrary.PathSegment.Segment> segments = new List<SVGLibrary.PathSegment.Segment>();
            segments = SVGLibrary.PathSegment.Decode("M 10,10 H 40 V 40 H 10 Z");
            foreach (SVGLibrary.PathSegment.Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }

            // test1.svg

            Console.WriteLine("---");
            Console.WriteLine("-> 1");
            segments = SVGLibrary.PathSegment.Decode("M 10,10 H 40 V 40 H 10 Z");
            foreach (SVGLibrary.PathSegment.Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }
            Console.WriteLine("-> 2");
            segments = SVGLibrary.PathSegment.Decode("m 20,25 5,-5 0,15");
            foreach (SVGLibrary.PathSegment.Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }
            Console.WriteLine("-> 3");
            segments = SVGLibrary.PathSegment.Decode("M 30,25 25,20");
            foreach (SVGLibrary.PathSegment.Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }
            Console.WriteLine("-> 4");
            segments = SVGLibrary.PathSegment.Decode("M 26.999999,18 V 13 H 32 v 2 l -5,0");
            foreach (SVGLibrary.PathSegment.Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }
            Console.WriteLine("-> 5");
            segments = SVGLibrary.PathSegment.Decode("m 23,13 v 5 h -5 l 0,-5");
            foreach (SVGLibrary.PathSegment.Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }

        }
    }
}
