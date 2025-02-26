using System;
using System.Collections.Generic;
using System.Text;

/*
 <問題>
 英大文字のみからなる文字列 S が与えられます。
 S に対して、次の手順を行った後に得られる文字列を出力してください。

 文字列が WA を（連続する）部分文字列として含む限り、次の操作を繰り返す。
    ・文字列中に登場する WA のうち最も先頭のものを AC に置換する。

 なお、本問題の制約下で、この操作は高々有限回しか繰り返されないことが証明できます。

 <制約>
 S は長さ 1 以上 3×10^5 以下の英大文字のみからなる文字列

 <入力>
 S

 <出力>
 S に対して、問題文中に記された手順を行った後に得られる文字列を出力せよ。
 */

namespace C_Debug {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            string wReadLine = Console.ReadLine();

            // W…WA の塊をリストに保持する
            var wWAChunks = new List<(int StartIndex, int Length)>();
            int wCurrentChunkStartIndex = -1;
            for (int i = 0; i < wReadLine.Length; i++) {
                char wCurrentChar = wReadLine[i];
                if (wCurrentChunkStartIndex == -1 && wCurrentChar == 'W') {
                    wCurrentChunkStartIndex = i;
                    continue;
                }
                if (wCurrentChunkStartIndex != -1 && wCurrentChar == 'A') {
                    wWAChunks.Add((wCurrentChunkStartIndex, i - wCurrentChunkStartIndex + 1));
                }
                if (wCurrentChunkStartIndex == -1 || wCurrentChar == 'W') continue;
                wCurrentChunkStartIndex = -1;
            }

            // 出力
            var wAnswerBuilder = new StringBuilder();
            foreach (var wWAChunk in wWAChunks) {
                wAnswerBuilder.Append(wReadLine.Substring(wAnswerBuilder.Length, wWAChunk.StartIndex - wAnswerBuilder.Length));
                wAnswerBuilder.Append('A' + new string('C', wWAChunk.Length - 1));
            }
            if (wAnswerBuilder.Length < wReadLine.Length) {
                wAnswerBuilder.Append(wReadLine.Substring(wAnswerBuilder.Length));
            }
            Console.Write(wAnswerBuilder);
        }
    }
}
