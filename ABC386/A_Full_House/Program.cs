using System;
using System.Collections.Generic;
using System.Linq;

/*
 <問題>
 4 枚のカードがあり、それぞれのカードには整数 A,B,C,D が書かれています。
 ここに 1 枚カードを加え、フルハウスとできるか判定してください。
 ただし、5 枚組のカードは以下の条件を満たすとき、またそのときに限って、フルハウスであると呼ばれます。

 ・異なる整数 x, y について、x が書かれたカード 3 枚と y が書かれたカード 2 枚からなる。

 <制約>
 ・入力は全て整数
 ・1 ≤A, B, C, D ≤ 13

 <入力>
 A B C D

 <出力>
 1 枚カードを加えてフルハウスとできる場合は Yes 、そうでないときは No と出力せよ。
 */

namespace A_Full_House {
    internal class Program {
        static void Main(string[] args) {

            var wCardNumbers = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            var wUniqueNumbers = new HashSet<int>();
            for (int i = 0; i < wCardNumbers.Length; i++) {
                wUniqueNumbers.Add(wCardNumbers[i]);
            }
            // カードを追加する前のカード番号が2種しかない場合にフルハウスとなり得る
            Console.WriteLine(wUniqueNumbers.Count == 2 ? "Yes" : "No");
        }
    }
}
