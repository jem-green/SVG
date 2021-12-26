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
            Path path = new Path();
            List<Segment> segments = new List<Segment>();
            //segments = path.Decode("M 1,2 3,4 5,6");
            //segments = path.Decode("M 1.1,2.2 3.3,4.4 5.5,6.6");
            //segments = path.Decode("M 1.1,2.2 3.3,4.5 5.5,6.6");
            segments = path.Decode("M 9.2604166,50.270833 50.270833,9.2604166 91.281249,50.270833");
            foreach (Segment segment in segments)
            {
                Console.WriteLine(segment.ToString());
            }
        }
    }
}
