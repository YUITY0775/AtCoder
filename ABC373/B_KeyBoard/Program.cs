using System;
using System.Collections.Generic;

/*
 <問題>
 26 個のキーが数直線上に並んだキーボードがあります。
 このキーボードの配列は ABCDEFGHIJKLMNOPQRSTUVWXYZ を並べ替えた文字列 S で表されます。
 文字 x に対応するキーが座標 x (1 ≤ x ≤ 26) にあります。
 ここで S(x) は S の x 文字目を表します。

 あなたはこのキーボードを使って ABCDEFGHIJKLMNOPQRSTUVWXYZ をこの順で右手人差し指で一度だけ入力します。
 ある文字を入力するためには、その文字に対応するキーの座標に指を移動させてキーを押す必要があります。
 はじめ、指は A に対応するキーの座標にあります。A に対応するキーを押してから、Z に対応するキーを押すまでの
 指の移動距離の合計として考えられる最小値を求めてください。ただし、 キーを押す動作は移動距離に含まれません。

 <制約>
 ・S は ABCDEFGHIJKLMNOPQRSTUVWXYZ を並べ替えた文字列である。

 <入力>
 S

 <出力>
 答えを出力せよ。
 */

namespace B_KeyBoard {
    internal class Program {
        static void Main(string[] args) {

            var wNonOrderedNumbers = new List<int>();

            foreach (char wAlphabet in Console.ReadLine()) {
                wNonOrderedNumbers.Add(wAlphabet - 'A');
            }

            int wResult = 0;
            for (int i = 0; i < wNonOrderedNumbers.Count - 1; i++) {
                wResult += Math.Abs(wNonOrderedNumbers.IndexOf(i) - wNonOrderedNumbers.IndexOf(i + 1));
            }
            Console.WriteLine(wResult);
        }
    }
}
