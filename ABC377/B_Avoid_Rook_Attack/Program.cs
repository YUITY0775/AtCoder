using System;

/*
 <問題>
 縦 8 マス、横 8 マスの 64 マスからなるマス目があります。 上から i 行目 (1 ≤ i ≤ 8) 、左から j 列目 (1 ≤ j ≤ 8) のマスを
 マス (i, j) と呼ぶことにします。
 それぞれのマスは、空マスであるかコマが置かれているかのどちらかです。
 マスの状態は長さ 8 の文字列からなる長さ 8 の列 (S(1), S(2), S(3), …, S(8)) で表されます。
​ マス (i, j) (1 ≤ i ≤ 8, 1 ≤ j ≤ 8) は、S(i) の j 文字目が . のとき空マスで、# のときコマが置かれています。
 あなたは、すでに置かれているどのコマにも取られないように、いずれかの空マスに自分のコマを置きたいです。
 マス (i, j) に置かれているコマは、次のどちらかの条件を満たすコマを取ることができます。

 ・i 行目のマスに置かれている
 ・j 列目のマスに置かれている

 あなたがコマを置くことができるマスがいくつあるか求めてください。

 <制約>
 ・S(i)は ., # からなる長さ 8 の文字列 (1 ≤ i ≤ 8)

 <入力>
 S(1)
 S(2)
 …
 S(8)

 <出力>
 すでに置かれているコマに取られずに自分のコマを置くことができる空マスの個数を出力せよ。
 */

namespace B_Avoid_Rook_Attack {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var wBoard = new bool[8, 8];
            for (int i = 0; i < 8; i++) {
                var wLine = Console.ReadLine();
                for (int j = 0; j < 8; j++) {
                    if (wLine[j] == '.') wBoard[i, j] = true;
                }
            }

            // 出力
            Console.WriteLine(CountAllTrueLine(wBoard, true) * CountAllTrueLine(wBoard, false));
        }

        /// <summary>
        /// 1 行もしくは 1 列すべてが true となっている行数もしくは列数を返す
        /// </summary>
        /// <param name="vField">フィールド</param>
        /// <param name="vIsRow">行をカウントする場合はtrue, 列をカウントする場合は false</param>
        /// <returns>コマが全く置かれていない行数もしくは列数</returns>
        static int CountAllTrueLine(bool[,] vField, bool vIsRow) {
            int wLineCount = 0;
            int wFirstLoopCount = vField.GetLength(vIsRow ? 0 : 1);
            int wSecondLoopCount = vField.GetLength(vIsRow ? 1 : 0);
            for (int i = 0; i < wFirstLoopCount; i++) {
                bool wIsAllTrueLine = true;
                for (int j = 0; j < wSecondLoopCount; j++) {
                    if (!(vIsRow ? vField[i, j] : vField[j, i])) {
                        wIsAllTrueLine = false;
                        break;
                    }
                }
                if (wIsAllTrueLine) wLineCount++;
            }
            return wLineCount;
        }
    }
}
