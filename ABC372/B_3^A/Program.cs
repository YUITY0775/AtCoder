using System;
using System.Collections.Generic;

/*
 <問題>
 正整数 M が与えられます。 以下の条件を全て満たす正整数 N と非負整数列 A = (A(1), A(2), …, A(N)) を一つ求めてください。
 1 ≤ N ≤ 20
 0 ≤ A(i) ≤ 10 (1 ≤ i ≤ N)
 (i = 1, N)Σ3^A(i) = M
​ ただし、制約下では条件を満たす N と A の組が必ず存在することが証明できます。

 <制約>
 ・1 ≤ M ≤ 10^5

 <入力>
 M

 <出力>
 以下の形式で条件を満たす N と A を出力せよ。

 N
 A(1) A(2) … A(N)

 なお、条件を満たす N と A の組が複数存在する場合は、どれを出力しても正答となる。
 */

namespace B_3_A {
    internal class Program {

        static void Main(string[] args) {

            var wInputNumber = int.Parse(Console.ReadLine());

            var wIndices = new List<int>();

            while (wInputNumber > 0) {
                for (int i = 0; i <= 10; i++) {
                    if (wInputNumber < Math.Pow(3, i + 1)) {
                        wInputNumber = wInputNumber - (int)Math.Pow(3, i);
                        wIndices.Add(i);
                        break;
                    }
                }
            }
            Console.WriteLine(wIndices.Count);
            Console.WriteLine(string.Join(" ", wIndices));
        }
    }
}
