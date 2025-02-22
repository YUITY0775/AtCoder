using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 <問題>
 整数 N と M が与えられます。
 以下の条件をすべて満たす長さ N の整数列 (A(1), A(2), …, A(N)) を辞書順にすべて出力してください。

 ・1 ≤ A(i)
 ・2 以上 N 以下の各整数 i に対して A(i − 1) + 10 ≤ A(i)
 ・A(N) ≤ M

 ※数列の辞書順とは
 長さ N の数列 S = (S(1), S(2), …, S(N)) が長さ N の数列 T = (T(1), T(2), …, T(N)) より辞書順で小さいとは、
 ある整数 1 ≤ i ≤ N が存在して下記の 2 つがともに成り立つことをいいます。

 ・(S(1), S(2), …, S(i - 1)) = (T(1), T(2), …, T(i - 1))
 ・S(i) が T(i) より（数として）小さい

 <制約>
 ・2 ≤ N ≤ 12
 ・10N − 9 ≤ M ≤ 10N
 ・入力される値はすべて整数

 <入力>
 N M

 <出力>
 条件を満たす長さ N の整数列の個数を X として X + 1 行出力せよ。
 1 行目には X の値を出力せよ。
 i + 1 (1 ≤ i ≤ X) 行目には条件を満たす整数列のうち辞書順で i 番目に小さいものを空白区切りで出力せよ。
 */

namespace D_Keep_Distance {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wInputNumbers = Console.ReadLine().Split(' ');
            (int wSequenceLength, int wMaxNumber) = (int.Parse(wInputNumbers[0]), int.Parse(wInputNumbers[1]));

            // 出力
            AddSequences(wMaxNumber, wSequenceLength);
            var wResultBuilder = new StringBuilder(FResultSequences.Count + Environment.NewLine);
            foreach (var wSequence in FResultSequences) {
                wResultBuilder.AppendLine(string.Join(" ", wSequence));
            }
            Console.WriteLine(wResultBuilder.ToString());
        }

        /// <summary>
        /// 数列をリストで保持
        /// </summary>
        static List<List<int>> FResultSequences = new List<List<int>>();

        /// <summary>
        /// 現在処理中の数列
        /// </summary>
        static List<int> FCurrentSequence = new List<int>();

        /// <summary>
        /// 解答用の数列を作成する
        /// </summary>
        /// <param name="vMax">数列の項の最大値</param>
        /// <param name="vDepth">数列の長さ</param>
        /// <param name="vStart">数列の各項の最小値</param>
        static void AddSequences(int vMax, int vDepth, int vStart = 1) {

            // 指定の数列の長さに達したら解答用の数列リストに作成中の数列を追加
            if (vDepth <= 0) {
                FResultSequences.Add(new List<int>(FCurrentSequence));
                return;
            }

            int wEnd = vMax - (vDepth - 1) * 10;
            for (int i = vStart; i <= wEnd; i++) {
                FCurrentSequence.Add(i);
                AddSequences(vMax, vDepth - 1, i + 10);
                FCurrentSequence.RemoveAt(FCurrentSequence.Count - 1);
            }
        }

        /// <summary>
        /// 指定した長さの辞書順に並べた整数列を列挙する
        /// </summary>
        /// <param name="vStart">数列の最小値</param>
        /// <param name="vMax">数列の要素の最大値</param>
        /// <param name="vLength">配列の長さ</param>
        /// <returns>辞書順に並べた整数列の列挙</returns>
        /// <remarks>解答には使用していないが参考までに残しておく</remarks>
        static IEnumerable<IEnumerable<int>> EnumulateCombinations(int vStart, int vMax, int vLength) {
            if (vLength <= 0) {
                yield return new List<int>();
                yield break;
            }

            int wEnd = vMax - (vLength - 1) * 10;
            for (int i = vStart; i <= wEnd; i++) {
                foreach (var wCombination in EnumulateCombinations(i + 10, vMax, vLength - 1)) {
                    yield return new List<int>() { i }.Concat(wCombination);
                }
            }
        }
    }
}
