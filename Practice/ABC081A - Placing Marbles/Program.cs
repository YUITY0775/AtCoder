using System;

namespace ABC081A___Placing_Marbles {
    internal class Program {
        static void Main(string[] args) {

            string input = Console.ReadLine();
            
            if (!int.TryParse(input, out _)) {
                Console.WriteLine("数値以外が入力されています。");
                return;
            }

            int count = 0;
            foreach(char num in input) {
                if (char.GetNumericValue(num) == 0) continue;
                ++count;
            }
            Console.WriteLine(count);
        }
    }
}
