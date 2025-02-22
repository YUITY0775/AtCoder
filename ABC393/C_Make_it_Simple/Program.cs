using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 頂点に 1 から N の、辺に 1 から M の番号がついた N 頂点 M 辺の無向グラフが与えられます。辺 i は頂点 u(i) と頂点 v(i) を結ぶ辺です。
 グラフから辺を取り除いてグラフを単純にするためには、少なくとも何本の辺を取り除く必要がありますか？
 ここでグラフが単純であるとは、グラフが自己ループや多重辺を含まないことをいいます。

 <制約>
 ・1 ≤ N ≤ 2×10^5
 ・0 ≤ M ≤ 5×10^5
 ・1 ≤ u(i) ≤ N
 ・1 ≤ v(i) ≤ N
 ・入力される値は全て整数

 <入力>
 N M
 u(1) v(1)
 u(2) v(2)
 …
 u(M) v(M)

 <出力>
 グラフを単純にするために取り除く必要がある辺の本数の最小値を出力せよ。
 */

namespace C_Make_it_Simple {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wEdgesCount = int.Parse(Console.ReadLine().Split(' ')[1]);

            // 自己ループ辺や多重辺を除いた辺のみをマップに追加する
            var wUniqueEdges = new Dictionary<int, HashSet<int>>();
            for (int i = 0; i < wEdgesCount; i++) {
                var wEdgeInfo = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                if (wEdgeInfo[0] == wEdgeInfo[1]) continue;

                // 管理しやすいように辺が接する頂点の番号を昇順にする
                if (wEdgeInfo[0] > wEdgeInfo[1]) {
                    (wEdgeInfo[1], wEdgeInfo[0]) = (wEdgeInfo[0], wEdgeInfo[1]);
                }
                if (!wUniqueEdges.TryGetValue(wEdgeInfo[0], out HashSet<int> wEndPoints)) {
                    wUniqueEdges.Add(wEdgeInfo[0], new HashSet<int>() { wEdgeInfo[1] });
                    continue;
                }
                wEndPoints.Add(wEdgeInfo[1]);
            }

            // 出力
            Console.WriteLine(wEdgesCount - wUniqueEdges.Sum(x => x.Value.Count));
        }
    }
}
