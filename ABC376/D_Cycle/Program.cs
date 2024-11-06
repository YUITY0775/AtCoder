using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 頂点に 1 から N の番号がついた N 頂点 M 辺の単純有向グラフがあります。
 i 番目の辺 (1 ≤ i ≤ M) は頂点 a(i) から頂点 b(i) へ伸びる辺です。
 頂点 1 を含む閉路が存在するか判定して、存在する場合はそのような閉路のうち辺数が最小の閉路の辺数を求めてください。

 <制約>
 ・2 ≤ N ≤ 2×10^5
 ・1 ≤ M ≤ min(N(N−1)/2, 2×10^5)
 ・1 ≤ a(i) ≤ N
 ・1 ≤ b(i) ≤ N
 ・a(i) != b(i) 
 ・i != j ならば (a(i), b(i)) != ((a(j), b(j)) かつ (a(i), b(i)) != ((b(j), a(j))
 ・入力される値は全て整数

 <入力>
 N M
 a(1) b(1)
 a(2) b(2)
 …
 a(M) b(m)

 <出力>
 頂点 1 を含む閉路が存在する場合は、そのような閉路のうち辺数が最小の閉路の辺数を出力せよ。そうでない場合は -1 を出力せよ。
 */

namespace D_Cycle {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wUserInput = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
            (long wNodesCount, long wEdgesCount) = (wUserInput[0], wUserInput[1]);

            var wNodes = new Node[wNodesCount];
            for (long i = 0; i < wNodesCount; i++) {
                wNodes[i] = new Node(new List<Edge>());
            }

            for (long i = 0; i < wEdgesCount; i++) {
                var wLine = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                wNodes[wLine[0] - 1].Edges.Add(new Edge(wLine[1] - 1));
            }

            // 出力
            Console.WriteLine(wNodes[0].FindShortestCycle(wNodes));
        }
    }

    /// <summary>
    /// 頂点
    /// </summary>
    internal class Node {
        public List<Edge> Edges { get; }
        public long Distance { get; private set; }
        public bool IsVisited { get; private set; }
        public Node(List<Edge> vEdges, bool IsVisited = false, long vDistance = 0) {
            this.Edges = vEdges;
            this.IsVisited = IsVisited;
            this.Distance = vDistance;
        }
        public long FindShortestCycle(IList<Node> vNodes) {

            var wNodesQueue = new Queue<Node>();
            wNodesQueue.Enqueue(vNodes[0]);

            while (wNodesQueue.Count > 0) {
                var wNode = wNodesQueue.Dequeue();
                foreach (var wEdge in wNode.Edges) {
                    if (wEdge.To == 0) return wNode.Distance + 1;
                    if (vNodes[wEdge.To].IsVisited) continue;
                    vNodes[wEdge.To].Distance = wNode.Distance + 1;
                    wNodesQueue.Enqueue(vNodes[wEdge.To]);
                    vNodes[wEdge.To].IsVisited = true;
                }
            }
            return -1;
        }
    }

    /// <summary>
    /// 辺
    /// </summary>
    internal class Edge {
        public int To { get; }
        public Edge(int vTo) {
            this.To = vTo;
        }
    }
}
