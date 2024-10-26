using System;
using System.Collections.Generic;
using System.Linq;

/*
 2 次元座標平面の原点に高橋くんがいます。
 高橋くんが座標平面上の点 (a, b) から点 (c, d) に移動するには √(a − c)^2 + (b − d)^2 のコストがかかります。
 高橋くんが原点からスタートし N 個の点 (X(1), Y(1)), …, (X(N), Y(N)) へこの順に移動したのち原点に戻るときの、
 コストの総和を求めてください。

 <制約>
 ・1 ≤ N ≤ 2×10^5
 ・−10^9 ≤ X(i), Y(i)​ ≤ 10^9 
 ・入力は全て整数である

 <入力>
 N
 X(1) Y(1)
 X(2) Y(2)
 …
 X(N) Y(N)

 <出力>
 答えを出力せよ。真の値との相対誤差または絶対誤差が 10^(−6) 以下であれば正解とみなされる。
 */

namespace B_Traveling_Takahashi_Problem {
    internal class Program {
        static void Main(string[] args) {

            var wPointsCount = int.Parse(Console.ReadLine());
            var wPoints = new List<Point>();
            wPoints.Add(new Point(0, 0));
            for (int i = 0; i < wPointsCount; i++) {
                var wInput = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                wPoints.Add(new Point(wInput[0], wInput[1]));
            }
            wPoints.Add(new Point(0, 0));

            double wAnswer = 0;
            for (int i = 0; i < wPointsCount + 1; i++) {
                wAnswer += Point.CalcDistance(wPoints[i], wPoints[i + 1]);
            }
            Console.WriteLine(wAnswer);
        }
    }

    internal struct Point {
        int X { get; set; }
        int Y { get; set; }
        public Point(int vX, int vY) {
            this.X = vX;
            this.Y = vY;
        }
        public static double CalcDistance(Point vA, Point vB) => Math.Sqrt(Math.Pow(vA.X - vB.X, 2) + Math.Pow(vA.Y - vB.Y, 2));
    }
}
