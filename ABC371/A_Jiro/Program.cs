using System;

/*
 <問題>
 A, B, C の三兄弟がいます。この 3 人の年齢関係は、3 つの文字 S(AB), S(AC), S(BC) によって与えられ、それぞれ以下を意味します。
​
 ・S(AB) が < の場合 A は B より年下であり、> の場合 A は B より年上である。
 ・S(AC) が < の場合 A は C より年下であり、> の場合 A は C より年上である。
 ・S(BC) ​が < の場合 B は C より年下であり、> の場合 B は C より年上である。

 三兄弟の次男、つまり二番目に年上の人は誰ですか？

 <制約>
 ・S(AB), S(AC), S(BC) ​ はそれぞれ < または >
 ・入力に矛盾は含まれない。つまり、与えられた大小関係を全て満たす年齢関係が必ず存在する入力のみが与えられる。

 <入力>
 S(AB) S(AC) S(BC)

 <出力>
 三兄弟の次男、つまり二番目に年上の人の名前を出力せよ。
 */

namespace A_Jiro {
    internal class Program {
        static void Main(string[] args) {
            var wMoreOrLessSymbols = Console.ReadLine().Split(' ');
            string wSecondChild;

            if (wMoreOrLessSymbols[0] == "<") {
                if (wMoreOrLessSymbols[1] == ">") {
                    wSecondChild = "A";
                } else if (wMoreOrLessSymbols[2] == "<") {
                    wSecondChild = "B";
                } else {
                    wSecondChild = "C";
                }

            } else {
                if (wMoreOrLessSymbols[1] == "<") {
                    wSecondChild = "A";
                } else if (wMoreOrLessSymbols[2] == ">") {
                    wSecondChild = "B";
                } else {
                    wSecondChild = "C";
                }
            }
            Console.WriteLine(wSecondChild);
        }
    }
}
