using System;
using System.Linq;

namespace ABC086A___Product {
    internal class Program {
        static void Main(string[] args) {

            int[] input = (Console.ReadLine()).Split(' ','　').Select(x => int.Parse(x)).ToArray();

            int a = input[0];
            int b = input[1];

            if ((a * b) % 2 == 0) {
                Console.WriteLine("Even");
            }
            else {
                Console.WriteLine("Odd");
            }
        }
    }
}
