using System;
using System.Collections.Generic;

/*
 <問題>
 0, 1 のみからなる長さ N の文字列 S が与えられます。
 S の中で先頭から K 番目の 1 の塊を K − 1 番目の 1 の塊の直後まで移動した文字列を出力してください。
 なお、S には 1 の塊が K 個以上含まれることが保証されます。

 より正確な説明は以下の通りです。
 ・S の l 文字目から r 文字目までからなる部分文字列を S(l…r) と表す。
 ・S の部分文字列 S(l…r) が 1 の塊であるとは、以下の条件を全て満たすことと定める。
    ・S(l) = S(l + 1) = … = S(r) = 1 
    ・l = 1 または S(l - 1) = 0 
    ・r = N または S(r + 1) = 0 
 ・S に含まれる 1 の塊が S(l(1)…r(1)), …, S(l(m)…r(m)) で全てであり、l(1) < … < l(m) を満たしているとする。 
   このとき、以下で定義される長さ N の文字列 T を、「 S の中で先頭から K 番目の 1 の塊を K − 1 番目の 1 の塊の直後まで
   移動した文字列 」と定める
    ・1 ≤ i ≤ r(K − 1) のとき T(i) = S(i)
    ・r(K − 1) + 1 ≤ i ≤ r(K − 1) + (r(K) − l(K)) + 1 のとき T(i) = 1
    ・r(K − 1) + (r(K) - l(K)) + 2 ≤ i ≤ r(K) のとき T(i) = 0
    ・r(K) + 1 ≤ i ≤ N のとき T(i) = S(i)

 <制約>
 ・1 ≤ N ≤ 5×10^5
 ・S は 0, 1 のみからなる長さ N の文字列
 ・2 ≤ K
 ・S には 1 の塊が K 個以上含まれる

 <入力>
 N K
 S

 <出力>
 答えを出力せよ。
 */

namespace C_Move_Segment {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wTargetIndex = int.Parse(Console.ReadLine().Split(' ')[1]);
            var wInputText = Console.ReadLine() + "X";  // 番兵 X を設定

            var wSegments = new List<string>();
            int wSegmentLength = 0;
            int wStartIndex = 0;
            for (int i = 0; i < wInputText.Length - 1; i++) {
                wSegmentLength++;
                if (wInputText[i] == wInputText[i + 1]) continue;

                wSegments.Add(wInputText.Substring(wStartIndex, wSegmentLength));
                wSegmentLength = 0;
                wStartIndex = i + 1;
            }

            SwapSegments(wSegments, wTargetIndex);

            // 出力
            Console.WriteLine(string.Join("", wSegments));
        }

        /// <summary>
        /// 部分文字列の順番を入れ替える
        /// </summary>
        /// <param name="vSegments">部分文字列のコレクション</param>
        /// <param name="vTargetIndex">入れ替える 1 の部分文字列が何番目か</param>
        static void SwapSegments(IList<string> vSegments, int vTargetIndex) {
            if (vSegments[0][0] == '1') {
                vTargetIndex = vTargetIndex * 2 - 2;
            } else {
                vTargetIndex = vTargetIndex * 2 - 1;
            }
            vSegments.Insert(vTargetIndex - 1, vSegments[vTargetIndex]);
            vSegments.RemoveAt(vTargetIndex + 1);
        }
    }
}

