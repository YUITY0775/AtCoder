using System;

/*
 <問題>
 00, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 のボタンがある電卓があります。
 この電卓で文字列 x が表示されている時に b のボタンを押すと、表示される文字列は x の末尾に b を連結したものとなります。
 最初、電卓には空文字列 (0 文字の文字列) が表示されています。
 この電卓に文字列 S を表示させるためにボタンを押す回数の最小値を求めてください。

 <制約>
 ・S は 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 からなる長さ 1 以上 1000 以下の列
 ・S の先頭は 0 でない

 <入力>
 S

 <出力>
 答えを整数として出力せよ。
 */

namespace B_Calculator {
    internal class Program {
        static void Main(string[] args) {

            var wDestNumber = Console.ReadLine();

            int wCount = 0;
            for (int i = 0; i < wDestNumber.Length; i++) {
                if (wDestNumber[i] == '0' && i < wDestNumber.Length - 1 && wDestNumber[i + 1] == '0') {
                    i++;
                }
                wCount++;
            }
            Console.WriteLine(wCount);
        }
    }
}
