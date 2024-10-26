using System;
using System.Collections.Generic;

/*
 <問題>
 AtCoder 王国では、長男に必ず「太郎」という名前を付けます。長男以外には「太郎」という名前は付けません。 
 長男とは、各家で生まれた男の子のうち最も早く生まれた者を指します。AtCoder 王国には N 戸の家があり、M 人の赤子が生まれました。
 また、M 人の赤子が生まれる前には、N 戸のどの家も赤子が生まれたことはありませんでした。
 赤子の情報が生まれの時系列順に与えられます。
 i 番目に生まれた赤子は、A(i) 番目の家で生まれ、B(i) が M のとき男の子、F のとき女の子です。
 M 人の赤子それぞれについて、付けられた名前が「太郎」か判定してください。

 <制約>
 ・1 ≤ N, M ≤ 100
 ・1 ≤ A(i) ≤ N
 ・B(i) は M または F
 ・入力される数値は全て整数

 <入力>
 N M
 A(1) B(1)
 A(2) B(2)
 …
 A(M) B(M)

 <出力>
 M 行出力せよ。
 i (1 ≤ i ≤ M) 行目には、i 番目に生まれた赤子の名前が「太郎」ならば Yes を、そうでない場合 No を出力せよ。
 */

namespace B_Taro {
    internal class Program {
        static void Main(string[] args) {

            var wChildren = int.Parse(Console.ReadLine().Split(' ')[1]);

            var wFamilies = new HashSet<string>();
            var wAnswers = new List<string>();

            for (int i = 0; i < wChildren; i++) {
                var wInputChildInfo = Console.ReadLine().Split(' ');
                if (wInputChildInfo[1] == "F" || wFamilies.Contains(wInputChildInfo[0])) {
                    wAnswers.Add("No");
                    continue;
                }
                wFamilies.Add(wInputChildInfo[0]);
                wAnswers.Add("Yes");
            }
            foreach (var wAnswer in wAnswers) {
                Console.WriteLine(wAnswer);
            }
        }
    }
}
