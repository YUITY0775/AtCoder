using System;
using System.Text;

/* 引用元：
 https://atcoder.jp/contests/abc454/tasks/abc454_d
 */

namespace D_xx {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int caseCount = int.Parse(Console.ReadLine());
            var testCases = new TestCase[caseCount];
            for (int i = 0; i < caseCount; i++) {
                string src = Console.ReadLine();
                string dst = Console.ReadLine();
                (string, string) patternToReplacement = ("(xx)", "xx");
                testCases[i] = new TestCase(patternToReplacement, src, dst);
            }

            // テストケースの実行
            var answers = new string[caseCount];
            for (int i = 0; i < caseCount; i++) {
                answers[i] = testCases[i].Solve();
            }

            // 出力処理
            Console.WriteLine(string.Join(Environment.NewLine, answers));
        }
    }

    /// <summary>
    /// テストケース
    /// </summary>
    internal class TestCase {

        /// <summary>
        /// パターンと置換文字列の対応表
        /// </summary>
        private readonly (string Pattern, string Replacement) _patternToReplacement;

        /// <summary>
        /// 変換元の文字列
        /// </summary>
        private readonly string _src;

        /// <summary>
        /// 変換先の文字列
        /// </summary>
        private readonly string _dst;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCase((string, string) patternToReplacement, string src, string dst) {
            _patternToReplacement = patternToReplacement;
            _src = src;
            _dst = dst;
        }

        /// <summary>
        /// テストケースを実行し、結果を返す
        /// </summary>
        public string Solve() => ReplacePattern(_src) == ReplacePattern(_dst) ? "Yes" : "No";

        /// <summary>
        /// 変換表に従ってパターンにマッチする文字列を置換した文字列を返す
        /// </summary>
        private string ReplacePattern(string text) {
            var replacedtext = new StringBuilder();
            for (int i = 0; i < text.Length; i++) {
                replacedtext.Append(text[i]);
                while (this.IsMatchPattern(replacedtext)) {
                    replacedtext.Length -= _patternToReplacement.Pattern.Length;
                    replacedtext.Append(_patternToReplacement.Replacement);
                }
            }
            return replacedtext.ToString();
        }

        /// <summary>
        /// パターンにマッチするか否かを返す
        /// </summary>
        private bool IsMatchPattern(StringBuilder builder) {
            if (builder.Length < _patternToReplacement.Pattern.Length) return false;
            for (int i = 0; i < _patternToReplacement.Pattern.Length; i++) {
                if (builder[builder.Length - _patternToReplacement.Pattern.Length + i] != _patternToReplacement.Pattern[i]) return false;
            }
            return true;
        }
    }
}
