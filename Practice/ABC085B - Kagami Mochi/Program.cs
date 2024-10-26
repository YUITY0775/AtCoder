using System;
using System.Collections.Generic;

//<問題文>
//X 段重ねの鏡餅 (X≥1) とは、X 枚の円形の餅を縦に積み重ねたものであって、
//どの餅もその真下の餅より直径が小さい（一番下の餅を除く）もののことです。例えば、直径 10、8、6センチメートル
//の餅をこの順に下から積み重ねると 3 段重ねの鏡餅になり、餅を一枚だけ置くと 1 段重ねの鏡餅になります。

//ダックスフンドのルンルンは N 枚の円形の餅を持っていて、そのうち i 枚目の餅の直径は d(i)センチメートルです。
//これらの餅のうち一部または全部を使って鏡餅を作るとき、最大で何段重ねの鏡餅を作ることができるでしょうか。

//<制約>
//1≤N≤100
//1≤d(i)≤100
//入力値はすべて整数である。

//<入力>
//N
//D(1)
//D(2)
//D(3)
//…
//D(N-1)

namespace ABC085B___Kagami_Mochi {
    internal class Program {
        static void Main(string[] args) {

            int wNumCount = int.Parse(Console.ReadLine());
            var wNumbers = new HashSet<int>();
            for (int i = 0; i < wNumCount; i++) {
                wNumbers.Add(int.Parse(Console.ReadLine()));
            }

            Console.WriteLine(wNumbers.Count);
        }
    }
}