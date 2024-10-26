using System;

namespace A_Rearranging_ABC {
    internal class Program {
        static void Main(string[] args) {

            var wText = Console.ReadLine();

            if (wText == "ABC") {
                Console.WriteLine("Yes");
                return;
            }
            if (wText == "ACB") {
                Console.WriteLine("Yes");
                return;
            }
            if (wText == "BAC") {
                Console.WriteLine("Yes");
                return;
            }
            if (wText == "BCA") {
                Console.WriteLine("Yes");
                return;
            }
            if (wText == "CAB") {
                Console.WriteLine("Yes");
                return;
            }
            if (wText == "CBA") {
                Console.WriteLine("Yes");
                return;
            }
            Console.WriteLine("No");
        }
    }
}
