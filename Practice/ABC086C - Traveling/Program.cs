using System;
using System.Collections.Generic;
using System.Linq;


//<問題文>
//シカのAtCoDeerくんは二次元平面上で旅行をしようとしています。 AtCoDeerくんの旅行プランでは、時刻 0 に
//点 (0,0) を出発し、1 以上 N 以下の各 i に対し、時刻 t(i) に 点 (x(i), y(i)) を訪れる予定です。
//AtCoDeerくんが時刻 t に 点 (x, y) にいる時、 時刻 t+1 には 点 (x+1, y), (x−1, y), (x, y+1), (x, y−1)
//のうちいずれかに存在することができます。
//その場にとどまることは出来ないことに注意してください。
//AtCoDeerくんの旅行プランが実行可能かどうか判定してください。

//<制約>
//1 ≤ N ≤ 10^5
//0 ≤ x(i) ≤ 10^5
//0 ≤ y(i) ≤ 10^5
//1 ≤ t(i) ≤ 10^5
//t(i) < t(i+1) (1 ≤ i ≤ N−1)
//入力は全て整数

//<入力>
//N
//t(1) x(1) y(1)
//t(2) x(2) y(2)
//…
//t(N) x(N) y(N)

//<出力>
//旅行プランが実行可能ならYesを、不可能ならNoを出力してください。

namespace ABC086C___Traveling {
    internal class Program {
        static void Main(string[] args) {

            int wPointCount = int.Parse(Console.ReadLine());

            var wPointInfos = new List<Point>() { new Point(0, 0, 0) };
            for (int i = 0; i < wPointCount; i++) {
                var wInput = (Console.ReadLine()).Split(' ').Select(x => int.Parse(x)).ToArray();
                var wPoint = new Point(wInput[0], wInput[1], wInput[2]);
                wPointInfos.Add(wPoint);
            }

            bool wResult = true;
            for (int i = 0; i < wPointInfos.Count - 1; i++) {
                if (!IsExist(wPointInfos[i], wPointInfos[i + 1])) {
                    wResult = false;
                    break;
                }
            }
            Console.WriteLine(wResult? "Yes": "No");
        }

        private static bool IsExist(Point vStart, Point vEnd) {
            var wTime = vEnd.Time - vStart.Time;    //移動距離
            var wCount = (vEnd.PosX - vStart.PosX) + (vEnd.PosY - vStart.PosY);
            if (wCount < - wTime || wTime < wCount) return false;

            for (int i = 0; i <= wTime; i++) {
                if ((wTime - i) - i == wCount) return true;
            }
            return false;
        }
    }

    public class Point {

        private int T;
        private int X;
        private int Y;

        public Point(int vTime, int vX, int vY) {
            this.T = vTime;
            this.X = vX;
            this.Y = vY;
        }
        public int Time => this.T;
        public int PosX => this.X;
        public int PosY => this.Y;
    }
}