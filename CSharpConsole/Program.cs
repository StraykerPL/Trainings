using System;

public class Program
{
    private static int Fact(int n)
    {
        if (n < 1)
            return 1;
        else
            return n * Fact(n - 1);
    }

    private static int FactorialLastTwo(int n, int factor = 2, int remainder = 1)
    {
        if (factor > n || remainder == 0)
            return remainder;

        return FactorialLastTwo(n, factor + 1, (remainder * factor) % 100);
    }

	public static void Main()
	{
		var amount = int.Parse(Console.ReadLine());
		
		List<int> inputs = new List<int>();
		for (int i = 0; i < amount; i++)
		{
			inputs.Add(int.Parse(Console.ReadLine()));
		}

        foreach(var val in inputs)
        {
            Console.WriteLine();
        }
	}
}