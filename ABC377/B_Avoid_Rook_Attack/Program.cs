using System;

namespace B_Avoid_Rook_Attack {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wBoard = new bool[8, 8];
            for (int i = 0; i < 8; i++) {
                var wLine = Console.ReadLine();
                for (int j = 0; j < 8; j++) {
                    if (wLine[j] == '#') wBoard[i, j] = true;
                }
            }

            // 1行すべてfalseになっている行数
            int wRowCount = 0;
            for (int i = 0; i < 8; i++) {
                bool wFlag = false;
                for (int j = 0; j < 8; j++) {
                    if (wBoard[i, j] == true) {
                        wFlag = true;
                        break;
                    }
                }
                if (!wFlag) wRowCount++;
            }

            // 1列すべてfalseになっている列数
            int wColumnCount = 0;
            for (int i = 0; i < 8; i++) {
                bool wFlag = false;
                for (int j = 0; j < 8; j++) {
                    if (wBoard[j, i] == true) {
                        wFlag = true;
                        break;
                    }
                }
                if (!wFlag) wColumnCount++;
            }

            Console.WriteLine(wRowCount * wColumnCount);
        }
    }
}
