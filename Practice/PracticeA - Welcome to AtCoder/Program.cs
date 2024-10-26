using System;
using System.Linq;

namespace PracticeA___Welcome_to_AtCoder {
    internal class Program {
        static void Main(string[] args) {

            int a = int.Parse(Console.ReadLine());

            int[] input = (Console.ReadLine().Split(' ', '　')).Select(x => int.Parse(x)).ToArray();

            int b = input[0];
            int c = input[1];

            string str = Console.ReadLine();

            Console.WriteLine((a + b + c) + " " + str);
        }
    }
}