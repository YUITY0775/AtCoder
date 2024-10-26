using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 1 から N までの番号が付けられた N 個のおもちゃと、1 から N - 1 までの番号が付けられた N − 1 個の箱があります。
 おもちゃ i (1 ≤ i ≤ N) の大きさは A(i)、箱 i (1 ≤ i ≤ N − 1) の大きさは B(i) です。
 すべてのおもちゃを別々の箱にしまいたいと考えている高橋君は、今から以下の操作を順に行うことにしました。

 1. 任意の正整数 x を選んで、大きさ x の箱を 1 つ購入する。
 2. N 個のおもちゃそれぞれを、（元々あった箱と新しく購入した箱を合わせた）N 個の箱のいずれかに入れる。
    ただし、各おもちゃはそのおもちゃの大きさ以上の大きさを持つ箱にしか入れることはできず、また 1 つの箱に 2 つ以上の
    おもちゃを入れることもできない。

 高橋君は、操作 1 で十分な大きさの箱を購入することで操作 2 が実行できるようにしたいですが、大きな箱ほど値段が高いため、
 可能な限り小さな箱を購入したいです。
 高橋君が操作 2 を実行できるような x の値が存在するか判定し、存在するならばその最小値を求めてください。

 <制約>
 ・2 ≤ N ≤ 2×10^5
 ・1 ≤ A(i), B(i) ≤ 10^9
 ・入力はすべて整数

 <入力>
 N
 A(1) A(2) … A(N)
 B(1) B(2) … B(N - 1)

 <出力>
高橋君が操作 2 を実行できるような x の値が存在するならばその最小値を、存在しないならば -1 を出力せよ。
 */

namespace C_Prepare_Another_Box {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wToysCount = int.Parse(Console.ReadLine());
            var wToysSize = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            var wBoxesSize = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            TryPutAway(wToysSize, wBoxesSize, out int wMinBoxSize);

            Console.WriteLine(wMinBoxSize);
        }


        static void TryPutAway(List<int> vToys, List<int> vBoxes, out int vMinBoxSize) {
            vMinBoxSize = -1;
            vToys.Sort();
            vBoxes.Sort();

            int wNGBorder = 0;
            int wOKBorder = vToys.Count - 1;

            // 一番大きいおもちゃと同じ大きさの箱を追加して、条件を満たさない場合は -1 を出力する。
            var wRemakedBoxes = vBoxes.ToList();
            InsertBox(wRemakedBoxes, vToys[vToys.Count - 1]);
            if (!CanPutAway(vToys, wRemakedBoxes)) return;

            // 一番小さいおもちゃと同じ大きさの箱を追加して、条件を満たす場合はその値を出力する。
            wRemakedBoxes = vBoxes.ToList();
            InsertBox(wRemakedBoxes, vToys[0]);
            if (CanPutAway(vToys, wRemakedBoxes)) {
                vMinBoxSize = vToys[0];
                return;
            }

            while (wOKBorder - wNGBorder > 1) {
                int wMidBorder = (wNGBorder + wOKBorder) / 2;
                wRemakedBoxes = vBoxes.ToList();
                InsertBox(wRemakedBoxes, vToys[wMidBorder]);
                if (CanPutAway(vToys, wRemakedBoxes)) {
                    wOKBorder = wMidBorder;
                } else {
                    wNGBorder = wMidBorder;
                }
            }
            vMinBoxSize = vToys[wOKBorder];
        }

        /// <summary>
        /// サイズによって昇順ソートされた箱のリストと、挿入するアイテムを受け取り、アイテム挿入後のリストを返す。
        /// </summary>
        /// <param name="vSortedBoxes">昇順ソートされた箱リスト</param>
        /// <param name="vItem">挿入するアイテム</param>
        static void InsertBox(List<int> vSortedBoxes, int vItem) {
            int wLeftIndex = 0;
            int wRightIndex = vSortedBoxes.Count - 1;
            while (wRightIndex - wLeftIndex > 1) {
                var wMidIndex = (wRightIndex + wLeftIndex) / 2;
                if (vSortedBoxes[wMidIndex] <= vItem) {
                    wLeftIndex = wMidIndex;
                } else {
                    wRightIndex = wMidIndex;
                }
            }
            if (vItem < vSortedBoxes[wLeftIndex]) {
                vSortedBoxes.Insert(wLeftIndex, vItem);
            } else if (vSortedBoxes[wRightIndex] < vItem) {
                vSortedBoxes.Insert(wRightIndex + 1, vItem);
            } else {
                vSortedBoxes.Insert(wRightIndex, vItem);
            }
        }

        /// <summary>
        /// 要素数が同じ2つの昇順ソートされたリスト A, B を受け取り、任意のインデックス i に対して、常に A[i] <= B[i]
        /// が成立するか否かを返す。
        /// </summary>
        /// <param name="vSortedToys">リスト A </param>
        /// <param name="vSortedBoxes">リスト B </param>
        /// <returns>常に A[i] <= B[i] が成立する場合 true を返す。</returns>
        static bool CanPutAway(List<int> vSortedToys, List<int> vSortedBoxes) {
            for (int i = 0; i < vSortedToys.Count; i++) {
                if (vSortedBoxes[i] < vSortedToys[i]) return false;
            }
            return true;
        }
    }
}
