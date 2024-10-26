using System;
using System.Collections.Generic;
using System.Text;


/*
 <問題>
 N 頂点 M 辺の有向グラフが与えられます。j 番目の有向辺は頂点 u(j)​ から頂点 v(j) ​に向かっており、重み w(j) を持っています。
 各頂点に −10^18 以上 10^18 以下の整数を書き込む方法であって、次の条件を満たすものを 1 つ見つけてください。

 ・頂点 i に書き込まれている値を x(i) とする。すべての辺 j = 1, 2, …, M について、x(v(j)) - x(u(j)) = w(j) が成り立つ。
​
 与えられる入力について、条件を満たす書き込み方が少なくとも 1 つ存在することが保証されます。

 <制約>
 ・2 ≤ N ≤ 2×10^5
 ・1 ≤ M ≤ min{2×10^5, N(N - 1)/2}  
 ・1 ≤ u(j), v(j) ≤ N
 ・u(j) != v(j)
 ・i != j なら​ (u(i), v(i)) != (u(j), v(j)) かつ (u(i), v(i)) != (v(j), u(j))
 ・∣w(j)∣ ≤ 10^9
 ・入力はすべて整数
 ・条件を満たす書き込み方が少なくとも 1 つ存在する

 <入力>
 N M
 u(1) v(1) w(1)
 u(2) v(2) w(2)
 …
 u(M) v(M) w(M)

 <出力>
 頂点 i に書き込む整数を x(i) として、x(1), x(2), …, x(N) をこの順に空白区切りで 1 行で出力せよ。
 答えが複数の場合、どれを出力しても良い。
 */
namespace D_Hidden_Weights {
    internal class Program {
        static void Main(string[] args) {

            var wInput = Console.ReadLine().Split(' ');
            int wPointCount = int.Parse(wInput[0]);
            int wEdgeCount = int.Parse(wInput[1]);

            var wGraph = new List<Point>();
            for (int i = 0; i < wPointCount; i++) {
                wGraph.Add(new Point(new List<Edge>(), 0, false));
            }

            for (int i = 0; i < wEdgeCount; i++) {
                wInput = Console.ReadLine().Split(' ');
                int wStart = int.Parse(wInput[0]) - 1; //頂点を 0 スタートにする
                int wEnd = int.Parse(wInput[1]) - 1;
                int wWeight = int.Parse(wInput[2]);

                wGraph[wStart].Edges.Add(new Edge(wEnd, wWeight));
                wGraph[wEnd].Edges.Add(new Edge(wStart, -wWeight));
            }

            for (int i = 0; i < wGraph.Count; i++) {

                if (wGraph[i].Visited) continue;

                SetPointValue(wGraph, wGraph[i]);
            }

            var wResult = new StringBuilder();
            for (int i = 0; i < wGraph.Count; i++) {
                wResult.Append(wGraph[i].Value.ToString() + " ");
            }
            Console.WriteLine(wResult.Remove(wResult.Length - 1, 1));
        }

        private static void SetPointValue(List<Point> vPoints, Point vPoint) {

            foreach (Edge wEdge in vPoint.Edges) {
                if (vPoints[wEdge.To].Visited) continue;
                vPoints[wEdge.To].Value = vPoint.Value + wEdge.Weight;
                vPoints[wEdge.To].Visited = true;

                // 再帰的に呼び出す
                SetPointValue(vPoints, vPoints[wEdge.To]);
            }

        }
    }

    internal class Point {
        public bool Visited { get; set; }
        public long Value { get; set; }
        public List<Edge> Edges { get; set; }
        public Point(List<Edge> vEdge, int vNumber, bool vVisited) {
            this.Edges = vEdge;
            this.Value = vNumber;
            this.Visited = vVisited;
        }
    }

    internal class Edge {
        public int To { get; }
        public int Weight { get; }
        public Edge(int vTo, int vWeight) {
            this.To = vTo;
            this.Weight = vWeight;
        }
    }
}
