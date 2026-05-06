using System;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc454/tasks/abc454_b
 */

namespace B_Mapping {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            (int personCount, int clothesCount) = (inputs[0], inputs[1]);

            var seenClothes = Console.ReadLine().Split(' ').Select(int.Parse).ToHashSet();
            bool areAllDistinct = seenClothes.Count == personCount;
            bool existsAllClothes = seenClothes.Count == clothesCount;

            // 出力処理
            Console.WriteLine(areAllDistinct ? "Yes" : "No");
            Console.WriteLine(existsAllClothes ? "Yes" : "No");
        }
    }
}
