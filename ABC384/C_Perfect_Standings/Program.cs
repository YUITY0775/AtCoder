using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 高橋くんは、プログラミングコンテストを主催することにしました。

 コンテストは A 問題、B 問題、C 問題、D 問題、E 問題の 5 問からなり、それぞれの配点は a 点、b 点、c 点、d 点、e 点です。
 コンテストには 31 人が参加し、全員が 1 問以上解きました。
 より具体的には、文字列 ABCDE の空でない（連続するとは限らない）部分列すべてについて、その部分列を名前とする参加者が
 存在し、その参加者は名前に含まれる文字に対応する問題をすべて解き、それ以外の問題は解きませんでした。
 例えば、A さんは A 問題のみを、BCE さんは B 問題、C 問題、E 問題を解きました。

 参加者の名前を、取った点数が大きいほうから順に出力してください。
 ただし、参加者が取った点数は、その参加者が解いた問題の配点の合計です。
 ただし、同じ点数を獲得した参加者については、名前が辞書順で小さいほうを先に出力してください。

 <制約>
 ・100 ≤ a ≤ b ≤ c ≤ d ≤ e ≤ 2718
 ・入力はすべて整数

 <入力>
 a b c d e

 <出力>
 31 行出力せよ。 
 i 行目 (1 ≤ i ≤ 31) には、i 番目に高い得点を獲得した参加者の名前を出力せよ。
 同じ得点を獲得した参加者については、それらのうち辞書順で小さい名前をもつ参加者を先に出力せよ。
 */

namespace C_Perfect_Standings {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wScoreInfo = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            var wAlphabetToScores = new Dictionary<char, int>() {
                { 'A', wScoreInfo[0]},
                { 'B', wScoreInfo[1]},
                { 'C', wScoreInfo[2]},
                { 'D', wScoreInfo[3]},
                { 'E', wScoreInfo[4]},
            };

            // 組み合わせと点数のペアをリストに保持
            var wCombinationWithScores = new List<(string Combination, int Score)>();
            foreach (var wCombination in EnumerateCombination('A', 'B', 'C', 'D', 'E')) {
                int wScore = 0;
                foreach (var wAlphabetToScore in wAlphabetToScores) {
                    wScore += wCombination.Contains(wAlphabetToScore.Key) ? wAlphabetToScore.Value : 0;
                }
                wCombinationWithScores.Add((new string(wCombination), wScore));
            }

            // 出力
            var wResult = wCombinationWithScores.OrderByDescending(x => x.Score).ThenBy(x => x.Combination).Select(x => x.Combination).ToArray();
            Console.WriteLine(string.Join(Environment.NewLine, wResult));
        }

        /// <summary>
        /// 文字の組み合わせをすべて列挙する
        /// </summary>
        /// <param name="vAlphabets>文字の配列</param>
        /// <returns>すべての組み合わせ</returns>
        static IEnumerable<char[]> EnumerateCombination(params char[] vAlphabets) {
            for (int i = 1; i <= vAlphabets.Length; i++) {
                foreach (char[] wConbination in MakeCombination(i, vAlphabets)) {
                    yield return wConbination;
                }
            }
        }

        /// <summary>
        /// 指定した個数の文字を選ぶ組み合わせを列挙する
        /// </summary>
        /// <param name="vCombinationLength">文字を選ぶ個数</param>
        /// <param name="vAlphabets">文字の配列</param>
        /// <returns>指定した個数の文字を選ぶ組み合わせ</returns>
        static IEnumerable<char[]> MakeCombination(int vCombinationLength, params char[] vAlphabets) {
            if (vCombinationLength > vAlphabets.Length) throw new ArgumentOutOfRangeException(nameof(vCombinationLength));

            var wCurrentCombination = new List<char>();

            IEnumerable<char[]> Combine(int vStart, int vDepth) {
                if (vDepth == 0) {
                    // 必要な長さに達したら配列として結果を返す
                    yield return wCurrentCombination.ToArray();
                    yield break;
                }

                for (int i = vStart; i < vAlphabets.Length; i++) {
                    // 要素を追加
                    wCurrentCombination.Add(vAlphabets[i]);

                    // 再帰呼び出し
                    foreach (var wResult in Combine(i + 1, vDepth - 1)) {
                        yield return wResult;
                    }
                    // 要素を削除して次の組み合わせを試す
                    wCurrentCombination.RemoveAt(wCurrentCombination.Count - 1);
                }
            }

            foreach (var wResult in Combine(0, vCombinationLength)) {
                yield return wResult;
            }
        }

    }
}
