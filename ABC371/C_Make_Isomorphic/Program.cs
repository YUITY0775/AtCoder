using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 頂点 1, 頂点 2,…, 頂点 N の N 個の頂点からなる単純無向グラフ G, H が与えられます。 
 G には M(G) 本の辺があり、i 本目 (1 ≤ i ≤ M(G)) の辺は頂点 u(i) と頂点 v(i) を結んでいます。 
 H には M(H) 本の辺があり、i 本目 (1 ≤ i ≤ M(H)) の辺は頂点 a(i) と頂点 b(i) を結んでいます。

 あなたは、グラフ H に対して次の操作を 0 回以上の好きな回数繰り返すことができます。

 ・1 ≤ i < j ≤ N を満たす整数の組 (i, j) をひとつ選ぶ。A (i, j) 円を支払って、頂点 i と頂点 j を結ぶ辺がなければ追加し、あれば取り除く。

 G と H を同型にするために少なくとも何円支払う必要があるか求めてください。

 <制約>
 ・1 ≤ N ≤ 8
 ・0 ≤ M(G) ≤ N(N − 1)/2
 ・0 ≤ M(H) ≤ N(N − 1)/2​
 ・1 ≤ u(i) < v(i) ≤ N (1 ≤ i ≤ M(G)) ​
 ・(u(i), v(i)) != (u(j), v(j)) (1 ≤ i < j ≤ M(G)) 
 ・1 ≤ a(i) <b(i) ≤ N (1 ≤ i ≤ M(H)) 
 ・(a(i), b(i)) != (a(j), b(j)) (1 ≤ i < j ≤ M(H)) 
 ・1 ≤ A(i, j) ≤ 10^6 (1 ≤ i < j ≤ N)
 ・入力はすべて整数

 <入力>
 N
 M(G)
 u(1) v(1)
 u(2) v(2)
 …
 u(M(G)) v(M(G))
 M(H)
 a(1) b(1)
 a(2) b(2)
 …
 a(M(H)) b(M(H))
 A(1, 2) A(1, 3) … A(1, N)
 A(2, 3) … A(2, N)
 …
 A(N - 1, N)
 */

namespace C_Make_Isomorphic {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wNodesCount = int.Parse(Console.ReadLine());

            var wGraph_G = LoadGraph(wNodesCount);
            var wGraph_H = LoadGraph(wNodesCount);
            var wFees = LoadFees(wNodesCount);

            var wNodesPerm = new int[wNodesCount];
            for (int i = 0;i < wNodesCount;i++) {
                wNodesPerm[i] = i + 1;
            }

            var wMinFee = int.MaxValue;

            // 頂点の順列をすべて探索する
            foreach (var wPerm in GetPermutation(wNodesPerm)) {
                int wFee = 0;
                for (int i = 0; i < wNodesCount; i++) {
                    for (int j = i + 1; j < wNodesCount; j++) {
                        if (wGraph_H[i, j] != wGraph_G[wPerm[i] - 1, wPerm[j] - 1]) {
                            wFee += wFees[i, j];
                        }
                    }
                }
                wMinFee = Math.Min(wMinFee, wFee);
            }

            // 出力
            Console.WriteLine(wMinFee);
        }

        /// <summary>
        /// 無向グラフの入力を受け取る。その際、辺の有無をbool値で表現する。
        /// </summary>
        /// <param name="vNodesCount">頂点数</param>
        /// <returns>辺の有無をbool値で表現した無向グラフ</returns>
        static bool[,] LoadGraph (int vNodesCount) {
            int wBranchesCount = int.Parse(Console.ReadLine());
            var wGraph = new bool[vNodesCount, vNodesCount];
            for (int i = 0; i < wBranchesCount; i++) {
                var wLine = Console.ReadLine().Split(' ');
                wGraph[int.Parse(wLine[0]) - 1, int.Parse(wLine[1]) - 1] = true;
                wGraph[int.Parse(wLine[1]) - 1, int.Parse(wLine[0]) - 1] = true;    // 無向グラフなので逆向きの辺も考える
            }
            return wGraph;
        }

        /// <summary>
        /// 辺を追加もしくは削除する場合のコストの入力を受け取る。
        /// </summary>
        /// <param name="vNodesCount">頂点数</param>
        /// <returns>辺を追加もしくは削除するコストの二次元配列</returns>
        static int[,] LoadFees(int vNodesCount) {
            var wFees = new int[vNodesCount, vNodesCount];
            for (int i = 0; i < vNodesCount - 1; i++) {
                var wLine = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                var wIndex = 0;
                for (int j = i + 1; j < vNodesCount; j++) {
                    wFees[i, j] = wLine[wIndex];
                    wFees[j, i] = wLine[wIndex];
                    wIndex++;
                }
            }
            return wFees;
        }

        /// <summary>
        /// 辞書順で昇順ソートした配列を受け取り、辞書順でのすべての順列を返す。
        /// </summary>
        /// <param name="vArray">辞書順で昇順ソートした配列</param>
        /// <returns>受け取った配列の全順列</returns>
        static IEnumerable<int[]> GetPermutation(int[] vNumbers) {

            if (vNumbers.Count() == 1) {
                yield return vNumbers;
                yield break;
            }

            foreach (int wNumber in vNumbers) {
                var wUsedNumber = new int[] { wNumber };
                var wUnusedNumbers = vNumbers.Except(wUsedNumber).ToArray();
                foreach (int[] wPerm in GetPermutation(wUnusedNumbers) ) {
                    yield return wUsedNumber.Concat(wPerm).ToArray();
                }
            }
        }
    }
}
