//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace C_Avoid_Knight_Attack {
//    internal class Program {
//        static void Main(string[] args) {

//            // 入力受付
//            var wInput = Console.ReadLine().Split(' ');
//            int wMatrixOrder = int.Parse(wInput[0]);
//            int wPieceCount = int.Parse(wInput[1]);

//            var wPiecies = new List<int[]>();
//            for (int i = 0; i < wPieceCount; i++) {
//                var wLine = Console.ReadLine().Split(' ');
//                wPiecies.Add(new int[] { int.Parse(wLine[0]) - 1, int.Parse(wLine[1]) - 1 });
//            }

//            var wBoard = new bool[wMatrixOrder, wMatrixOrder];

//            foreach (var wPiece in wPiecies) {
//                // 駒があるところを true に変える
//                wBoard[wPiece[0], wPiece[1]] = true;

//                TurnOverBoard(wBoard, wPiece[0], wPiece[1]);
//            }

//            long wCount = 0;
//            foreach (var wPosition in wBoard) {
//                if (!wPosition) wCount++;
//            }

//            Console.WriteLine(wCount);
//        }

//        static void TurnOverBoard(bool[,] vBoard, int vRow, int vColumn) {
//            int wRowCount = vBoard.GetLength(0);
//            int wColumnCount = vBoard.GetLength(1);

//            if (vRow - 1 >= 0 && vColumn - 2 >= 0) {
//                vBoard[vRow - 1, vColumn - 2] = true;
//            }
//            if (vRow - 2 >= 0 && vColumn - 1 >= 0) {
//                vBoard[vRow - 2, vColumn - 1] = true;
//            }
//            if (vRow - 2 >= 0 && vColumn + 1 < wColumnCount) {
//                vBoard[vRow - 2, vColumn + 1] = true;
//            }
//            if (vRow - 1 >= 0 && vColumn + 2 < wColumnCount) {
//                vBoard[vRow - 1, vColumn + 2] = true;
//            }
//            if (vRow + 1 < wRowCount && vColumn + 2 < wColumnCount) {
//                vBoard[vRow + 1, vColumn + 2] = true;
//            }
//            if (vRow + 2 < wRowCount && vColumn + 1 < wColumnCount) {
//                vBoard[vRow + 2, vColumn + 1] = true;
//            }
//            if (vRow + 2 < wRowCount && vColumn - 1 >= 0) {
//                vBoard[vRow + 2, vColumn - 1] = true;
//            }
//            if (vRow + 1 < wRowCount && vColumn - 2 >= 0) {
//                vBoard[vRow + 1, vColumn - 2] = true;
//            }
//        }
//    }
//}
using System;
using System.Collections.Generic;

namespace C_Avoid_Knight_Attack {
    internal class Program {
        static void Main(string[] args) {
            // 入力受付
            var wInput = Console.ReadLine().Split(' ');
            int wMatrixOrder = int.Parse(wInput[0]);
            int wPieceCount = int.Parse(wInput[1]);

            // 駒の位置をリストに格納
            var wPieces = new List<(int, int)>();
            for (int i = 0; i < wPieceCount; i++) {
                var wLine = Console.ReadLine().Split(' ');
                wPieces.Add((int.Parse(wLine[0]) - 1, int.Parse(wLine[1]) - 1));
            }

            // 影響を受けたマスを記録するハッシュセット
            var attackedPositions = new HashSet<(int, int)>();

            // 駒ごとに影響範囲を記録
            foreach (var (row, col) in wPieces) {
                attackedPositions.Add((row, col));  // 駒の位置を記録
                MarkAttackedPositions(attackedPositions, row, col, wMatrixOrder);
            }

            // 全体の空いているマス数を計算
            long wCount = (long)wMatrixOrder * wMatrixOrder - attackedPositions.Count;
            Console.WriteLine(wCount);
        }

        static void MarkAttackedPositions(HashSet<(int, int)> attackedPositions, int row, int col, int size) {
            // ナイトの移動パターン
            int[] rowMoves = { -1, -2, -2, -1, 1, 2, 2, 1 };
            int[] colMoves = { -2, -1, 1, 2, 2, 1, -1, -2 };

            for (int i = 0; i < rowMoves.Length; i++) {
                int newRow = row + rowMoves[i];
                int newCol = col + colMoves[i];

                // ボードの範囲内なら影響範囲に追加
                if (newRow >= 0 && newRow < size && newCol >= 0 && newCol < size) {
                    attackedPositions.Add((newRow, newCol));
                }
            }
        }
    }
}