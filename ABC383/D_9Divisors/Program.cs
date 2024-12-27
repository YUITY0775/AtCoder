using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 N 以下の正整数のうち、正の約数をちょうど 9 個持つものの個数を求めてください。

 <制約>
 ・1 ≤ N ≤ 4×10^12
 ・入力される数値は全て整数

 <入力>
 N

 <出力>
 答えを出力せよ。
 */

namespace D_9Divisors {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            long wMaximum = long.Parse(Console.ReadLine());

            /*
             <ポイント>
             N の正の約数が 9 個ある
             ⇔ 素因数分解した時の結果が 〇^8 もしくは □^2 × △^2
             ⇔ √N を素因数分解したときの結果が ○^4 もしくは □ × △
             */

            int wSqrtMaximum = (int)Math.Sqrt(wMaximum);
            var wPrimeNumbers = ExecuteEratosthenesSieve(wSqrtMaximum).ToArray();



            int wPrimeNumberCount = 0;
            foreach (int wPrimeNumber in wPrimeNumbers) {

                // 〇^4 の個数をカウントする
                if (TryExecutePower(wPrimeNumber, 4, out int wResult) && wResult <= wSqrtMaximum) {
                    wPrimeNumberCount++;
                }

                // □ × △ の個数をカウントする
                foreach (int wOtherPrimeNumber in wPrimeNumbers) {
                    if (wPrimeNumber <= wOtherPrimeNumber) continue;
                    if (wOtherPrimeNumber * wPrimeNumber > wSqrtMaximum) break;
                    wPrimeNumberCount++;
                }
            }

            // 出力
            Console.WriteLine(wPrimeNumberCount);
        }

        /// <summary>
        /// 引数として入力した正整数値以下の素数を列挙して返す
        /// </summary>
        /// <param name="vMaximum">最大値(正整数値)</param>
        /// <returns>最大値以下の素数</returns>
        static IEnumerable<int> ExecuteEratosthenesSieve(int vMaximum) {

            // 2と3を先に返す
            yield return 2;
            yield return 3;

            // i が素数か否かを保持する配列
            var wPrimeNumberFlags = new bool[vMaximum + 1];
            for (int i = 2; i <= vMaximum; i++) wPrimeNumberFlags[i] = true;

            int wSqrtMaximum = (int)Math.Sqrt(vMaximum);
            for (int i = 2; i <= wSqrtMaximum; i++) {
                if (wPrimeNumberFlags[i]) {
                    for (int j = i * 2; j <= vMaximum; j += i) wPrimeNumberFlags[j] = false;
                }

                // (i + 1) * (i + 1) - 1 以下の素数は求まっているので順に返す
                for (int k = i * i + 1; k <= vMaximum && k < (i + 1) * (i + 1); k++) {
                    if (wPrimeNumberFlags[k]) yield return k;
                }
            }
        }

        /// <summary>
        /// Math.Pow()メソッドの誤差発生を回避するための累乗メソッド
        /// </summary>
        /// <param name="vNumber">基数</param>
        /// <param name="vExponent">指数</param>
        /// <param name="vResult">累乗した結果</param>
        /// <returns>オーバーフローせずに実行できたか否か</returns>
        static bool TryExecutePower(int vNumber, int vExponent, out int vResult) {
            vResult = 1;
            for (int i = 0; i < vExponent; i++) {
                try {
                    vResult = checked(vResult * vNumber);
                } catch (OverflowException) {
                    vResult = -1;
                    return false;
                }
            }
            return true;
        }
    }
}
