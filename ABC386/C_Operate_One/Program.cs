using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 文字列 S に対して以下の操作を 0 回以上 K 回以下行って、文字列 T と一致させられるか判定してください。

 次の 3 種類の操作のうちひとつを選択し、実行する。
 ・S 中の (先頭や末尾を含む) 任意の位置に、任意の文字を 1 つ挿入する。
 ・S 中の文字を 1 つ選び、削除する。
 ・S 中の文字を 1 つ選び、別の 1 つの文字に変更する

 <制約>
 ・S,T は英小文字からなる長さ 1 以上 500000 以下の文字列
 ・K = 1

 <入力>
 K
 S
 T

 <出力>
 K 回以下の操作で S を T に一致させられる時 Yes 、そうでない時 No と出力せよ。
 */

namespace C_Operate_One {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wOperationsCount = int.Parse(Console.ReadLine());
            string wTextA = Console.ReadLine();
            string wTextB = Console.ReadLine();

            // 出力
            Console.WriteLine(CalcLevenshteinDistance(wTextA, wTextB, wOperationsCount) <= wOperationsCount ? "Yes" : "No");
        }

        /// <summary>
        /// レーベンシュタイン距離を求める(ただし、指定した操作回数を超える場合は最大文字数 500000 を返す)
        /// </summary>
        /// <param name="vStrA">文字列</param>
        /// <param name="vStrB">文字列</param>
        /// <returns>レーベンシュタイン距離</returns>
        static int CalcLevenshteinDistance(string vStrA, string vStrB, int vOperationsCount) {

            // 文字列が短い方を目標の文字列かつDPテーブルの縦軸とする(計算量を抑えるため)
            (string wDestText, string wSrcText) = vStrA.Length <= vStrB.Length ? (vStrA, vStrB) : (vStrB, vStrA);

            // 明らかに条件を満たさない場合は早期リターン
            if (wSrcText.Length - wDestText.Length > vOperationsCount) return 500000;

            // DPテーブルを初期化
            var wDP = new int[wDestText.Length + 1][];
            for (int i = 0; i <= wDestText.Length; i++) {
                wDP[i] = Enumerable.Repeat(500000, vOperationsCount * 2 + 1).ToArray();
            }

            // 空文字同士の比較を編集距離 0 で初期化
            wDP[0][vOperationsCount] = 0;

            // DPテーブルを埋めていく処理
            for (int i = 0; i <= wDestText.Length; i++) {
                for (int j = 0; j <= vOperationsCount * 2; j++) {

                    // 不要な計算をしない
                    if (i + j - vOperationsCount < 0) continue;
                    if (i + j - vOperationsCount > wSrcText.Length) break;

                    if (i > 0 && j < vOperationsCount * 2) {
                        wDP[i][j] = Math.Min(wDP[i][j], wDP[i - 1][j + 1] + 1);
                    }
                    if (j > 0) {
                        wDP[i][j] = Math.Min(wDP[i][j], wDP[i][j - 1] + 1);
                    }
                    if (i > 0 && i + j - vOperationsCount > 0) {
                        wDP[i][j] = Math.Min(wDP[i][j], wDP[i - 1][j] + (wDestText[i - 1] == wSrcText[i + j - vOperationsCount - 1] ? 0 : 1));
                    }
                }
            }
            return wDP[wDestText.Length][wSrcText.Length - wDestText.Length + vOperationsCount];
        }

        /// <summary>
        /// 3 つの整数値のうち最小値を返す
        /// </summary>
        static int CalcMin(int vX, int vY, int vZ) => Math.Min(vX, Math.Min(vY, vZ));

        /// <summary>
        /// 1 度の操作で文字列を一致させされるか判定する
        /// </summary>
        /// <param name="vSrcText">元の文字列</param>
        /// <param name="vDestText">目標とする文字列</param>
        /// <returns> 1 度の操作で文字列を一致されられるか否か</returns>
        /// <remarks>本問題ではこちらを使用すればよい。より汎用的にしたい場合にはレーベンシュタイン距離を求める。</remarks>
        static bool CanMatchStr(string vSrcText, string vDestText) {
            var wSrcChars = new List<char>(vSrcText);
            var wDestChars = new List<char>(vDestText);

            for (int i = 0; i < 2; i++) {
                // 文字列が空でなく末尾の文字が等しい場合に末尾の文字列を削除する
                while (wSrcChars.Count > 0 && wDestChars.Count > 0 && wSrcChars[wSrcChars.Count - 1] == wDestChars[wDestChars.Count - 1]) {
                    wSrcChars.RemoveAt(wSrcChars.Count - 1);
                    wDestChars.RemoveAt(wDestChars.Count - 1);
                }
                // リストを反転して元の文字列の先頭から処理する
                wSrcChars.Reverse();
                wDestChars.Reverse();
            }
            // 残った文字列の長さが 1 以下であれば条件を満たす
            return wSrcChars.Count <= 1 && wDestChars.Count <= 1;
        }
    }
}
