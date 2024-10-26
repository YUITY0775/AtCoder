using System;

//<問題文>
//あなたは、500 円玉を A 枚、100 円玉を B 枚、50 円玉を C 枚持っています。
//これらの硬貨の中から何枚かを選び、合計金額をちょうど X 円にする方法は何通りありますか。
//同じ種類の硬貨どうしは区別できません。2 通りの硬貨の選び方は、ある種類の硬貨について
//その硬貨を選ぶ枚数が異なるとき区別されます。

//<制約>
//0≤A, B, C≤50
//A+B+C≥1
//50≤X≤20,000
//A, B, C は整数である
//X は 
//50 の倍数である

//<入力>
//A
//B
//C
//X

namespace ABC087B___Coins {
    internal class Program {
        static void Main(string[] args) {

            int w500yen = int.Parse(Console.ReadLine());
            int w100yen = int.Parse(Console.ReadLine());
            int w50yen = int.Parse(Console.ReadLine());
            int wAmount = int.Parse(Console.ReadLine());

            if (!IsExists(w500yen, w100yen, w50yen, wAmount)) {
                Console.WriteLine(0);
                return;
            }
            Console.WriteLine(CountPattern(w500yen, w100yen, w50yen, wAmount));
        }

        private static bool IsExists(int v500yen, int v100yen, int v50yen, int vAmount) {
            if (vAmount < 50 || vAmount % 50 != 0) return false;
            if (vAmount > 500 * v500yen + 100 * v100yen + 50 * v50yen) return false;
            return true;
        }

        private static int CountPattern(int v500yen, int v100yen, int v50yen, int vAmount) {
            int wCount = 0;
            for (int i = 0; i <= v500yen; i++) {
                int w500yenChecker = vAmount - 500 * i;
                if (w500yenChecker < 0) continue;

                for (int j = 0; j <= v100yen; j++) {
                    int w100yenChecker = vAmount - 500 * i - 100 * j;
                    if (v50yen < w100yenChecker /50 || w100yenChecker < 0) continue;
                    wCount++;
                }
            }
            return wCount;
        }
    }
}
