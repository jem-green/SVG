using System;
using System.Collections.Generic;
using System.Diagnostics;
using SVGLibrary;
using static SVGLibrary.Path;

namespace SVGTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // test.svg

            Console.WriteLine("---");
            Console.WriteLine("-> 1");
            List<Segment> segments = new List<Segment>();
            segments = Path.Decode("M 10,10 H 40 V 40 H 10 Z");
            foreach (Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }

            // test1.svg

            Console.WriteLine("---");
            Console.WriteLine("-> 1");
            segments = Path.Decode("M 10,10 H 40 V 40 H 10 Z");
            foreach (Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }
            Console.WriteLine("-> 2");
            segments = Path.Decode("m 20,25 5,-5 0,15");
            foreach (Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }
            Console.WriteLine("-> 3");
            segments = Path.Decode("M 30,25 25,20");
            foreach (Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }
            Console.WriteLine("-> 4");
            segments = Path.Decode("M 26.999999,18 V 13 H 32 v 2 l -5,0");
            foreach (Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }
            Console.WriteLine("-> 5");
            segments = Path.Decode("m 23,13 v 5 h -5 l 0,-5");
            foreach (Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }

        }
    }
}
