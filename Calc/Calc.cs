namespace Calc;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter two values to multiply: ");
        int val1 = Convert.ToInt32(Console.ReadLine());
        int val2 = Convert.ToInt32(Console.ReadLine());
        double result = val1 * val2;
        Console.WriteLine(result);
        File.WriteAllText("Calc.txt" , Convert.ToString(result));
    }
}
