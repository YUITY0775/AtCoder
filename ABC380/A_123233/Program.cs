using System;

/*
 <問題>
 6 桁の正整数 N が与えられます。この整数が以下の条件を全て満たすか判定してください。

 ・N の各桁のうち、1 は丁度 1 つである。
 ・N の各桁のうち、2 は丁度 2 つである。
 ・N の各桁のうち、3 は丁度 3 つである。

 <制約>
 N は 
 ・100000 ≤ N ≤ 999999 を満たす整数

 <入力>
 N

 <出力>
 N が問題文中の条件を全て満たすなら Yes 、そうでないなら No と 1 行に出力せよ。
 */

namespace A_123233 {
    internal class Program {
        static void Main(string[] args) {

            int[] wNumCounter = new int[10];
            foreach (char wNumChar in Console.ReadLine()) {
                wNumCounter[wNumChar - '0']++;
            }

            for (int i = 1; i <= 3; i++) {
                if (wNumCounter[i] != i) {
                    Console.WriteLine("No");
                    return;
                }
            }
            Console.WriteLine("Yes");
        }
    }
}
