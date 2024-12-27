using System;
using System.Linq;

/*
 <問題>
 N 個の箱が横一列に並んでおり、そのうちのいくつかの箱にはクッキーが入っています。
 各箱の状態は長さ N の文字列 S によって表されます。 具体的には、左から i (1 ≤ i ≤ N) 番目の箱は、
 S の i 文字目が @ のときクッキーが 1 枚入っており、. のとき空き箱です。

 高橋君は今からの D 日間、一日一回ずつ、これらの箱のいずれかに入ったクッキーを 1 枚選んで食べます。

 N 個の箱のうち、
 D 日間が経過した後に空き箱であるものはいくつあるか求めてください。
 （この値は高橋君が各日でどのクッキーを選ぶかによらないことが証明できます。）

 なお、S には @ が D 個以上含まれることが保証されます。

 <制約>
 ・1 ≤ D ≤ N ≤ 100
 ・N, D は整数
 ・S は @ と . からなる長さ N の文字列
 ・S には @ が D 個以上含まれる

 <入力>
 N D
 S

 <出力>
 N 個の箱のうち、D 日間が経過した後に空き箱であるものの個数を出力せよ。
 */

namespace A_Daily_Cookie {
    internal class Program {
        static void Main(string[] args) {

            int wDaysCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            string wBoxesCondition = Console.ReadLine();

            int wEmptyBoxCount = wBoxesCondition.Count(x => x == '.');

            Console.WriteLine(wEmptyBoxCount + wDaysCount);
        }
    }
}
