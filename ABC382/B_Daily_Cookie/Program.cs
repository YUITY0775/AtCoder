using System;
using System.Text;

/*
 <問題>
 N 個の箱が横一列に並んでおり、そのうちのいくつかの箱にはクッキーが入っています。
 各箱の状態は長さ N の文字列 S によって表されます。 具体的には、左から i (1 ≤ i ≤ N) 番目の箱は、
 S の i 文字目が @ のときクッキーが 1 枚入っており、. のとき空き箱です。

 高橋君は今からの D 日間、一日一回ずつ、その時点でクッキーが入っている箱のうち最も右にある箱のクッキーを選んで食べます。

 N 個の箱それぞれについて、D 日間が経過した後にその箱にクッキーが入っているかを求めてください。

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
 長さ N の文字列を出力せよ。 出力する文字列の i (1 ≤ i ≤ N) 文字目は、
 D 日間が経過した後に左から i 番目の箱にクッキーが入っているならば @、入っていないならば . とせよ。
 */

namespace B_Daily_Cookie {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int wDaysCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            var wBoxesCondition = new StringBuilder(Console.ReadLine());

            int wLastCookieIndex = wBoxesCondition.Length;
            for (int i = 0; i < wDaysCount; i++) {
                wLastCookieIndex = RemoveRightEndCookie(wBoxesCondition, wLastCookieIndex - 1);
            }

            // 出力
            Console.WriteLine(wBoxesCondition.ToString());
        }

        /// <summary>
        /// 文字列内の最も右にあるクッキーが入っている箱を空箱にする
        /// </summary>
        /// <param name="vBoxesCondition">箱の状態を表す文字列</param>
        static int RemoveRightEndCookie(StringBuilder vBoxesCondition, int vLastCookieIndex) {
            for (int i = vLastCookieIndex; i >= 0; i--) {
                if (vBoxesCondition[i] == '@') {
                    vBoxesCondition[i] = '.';
                    return i;
                }
            }
            return -1;
        }
    }
}
