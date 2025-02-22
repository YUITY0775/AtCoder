using System;
using System.Linq;

/*
 <問題>
 4 種類の牡蠣 1,2,3,4 があります。 このうちちょうど 1 種類の牡蠣については、食べるとお腹を壊してしまいます。
 それ以外の牡蠣については、食べてもお腹を壊しません。
 高橋君が牡蠣 1,2 を食べ、青木君が牡蠣 1,3 を食べました。
 二人がこれによってお腹を壊したかどうかの情報が二つの文字列 S(1), S(2) によって与えられます。
 具体的には、S(1) = sick であるとき高橋君がお腹を壊したことを、S(1) = fine であるとき高橋君がお腹を壊さなかったことを表します。
 同様に、S(2) = sick であるとき青木君がお腹を壊したことを、S(2) = fine であるとき青木君がお腹を壊さなかったことを表します。
 与えられた情報をもとに、どの種類の牡蠣を食べるとお腹を壊すか判定してください。

 <制約>
 S(1), S(2) はそれぞれ sick または fine

 <入力>
 S(1) S(2)

 <出力>
 食べるとお腹を壊す牡蠣の種類の番号を出力せよ。
 */

namespace A_Poisonous_Oyster {
    internal class Program {
        static void Main(string[] args) {
            bool[] IsSick = Console.ReadLine().Split(' ').Select(x => x == "sick").ToArray();

            int wBadOysterNumber = 0;
            if (IsSick[0] && IsSick[1]) {
                wBadOysterNumber = 1;
            } else if (IsSick[0] && !IsSick[1]) {
                wBadOysterNumber = 2;
            } else if (!IsSick[0] && IsSick[1]) {
                wBadOysterNumber = 3;
            } else {
                wBadOysterNumber = 4;
            }
            Console.WriteLine(wBadOysterNumber);
        }
    }
}
