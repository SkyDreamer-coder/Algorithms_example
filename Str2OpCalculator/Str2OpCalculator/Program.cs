using System.Collections.Generic;
using System.Text;

class Solution
{
    public static int Calculate(string input)
    {
        List<int> numbers = new List<int>();
        List<char> operators = new List<char>();

        var sb = new StringBuilder();
        sb.Append(input);
        int currentIndex = 0;

        while (currentIndex < input.Length)
        {
            if(currentIndex==0 && !(char.IsDigit(input[currentIndex])))
            {
                if (input[currentIndex] == '+')
                {
                    sb.Remove(currentIndex, 1);
                    input = sb.ToString();
                }
                else if (input[currentIndex] == '-')
                {
                    string indexCurrentNumber = "";
                    int index = 1;

                    while (char.IsDigit(input[index]))
                    {
                        indexCurrentNumber += input[index];
                        index++;
                    }

                    sb.Remove(currentIndex, 1);
                    sb.Insert(currentIndex, "1-" + indexCurrentNumber + "*1");
                    sb.Remove(4 + indexCurrentNumber.Length, indexCurrentNumber.Length);
                    sb.Insert(sb.Length, "-1");
                    input = sb.ToString();
                }
            }

            // Find Digists
            string currentNumber = "";
            while (currentIndex < input.Length && (Char.IsDigit(input[currentIndex])))
            {
                currentNumber += input[currentIndex];
                currentIndex++;
            }                        

            if (!int.TryParse(currentNumber, out int number))
            {
                throw new Exception("Geçersiz sayı formatı.");
            }

            
            numbers.Add(number);

            // Find operators
            if (currentIndex < input.Length)
            {
                char op = input[currentIndex];
                if (op == '+' || op == '-' || op == '*' || op == '/')
                {
                    operators.Add(op);
                }
                else
                {
                    throw new Exception("Geçersiz operatör.");
                }
                currentIndex++;
            }
        }

        // multiply and divide priority
        for (int i = 0; i < operators.Count; i++)
        {
            if (operators[i] == '*' || operators[i] == '/')
            {
                int left = numbers[i];
                int right = numbers[i + 1];
                int result = 0;

                if (operators[i] == '*')
                {
                    result = left * right;
                }
                else if (operators[i] == '/')
                {
                    if (right == 0)
                        throw new DivideByZeroException("Sıfıra bölme hatası.");
                    result = left / right;
                }

                
                numbers[i] = result;
                numbers.RemoveAt(i + 1);
                operators.RemoveAt(i);
                i--;
            }
        }

        // +, -
        int finalResult = numbers[0];
        for (int i = 0; i < operators.Count; i++)
        {
            if (operators[i] == '+')
            {
                finalResult += numbers[i + 1];
            }
            else if (operators[i] == '-')
            {
                finalResult -= numbers[i + 1];
            }
        }

        return finalResult;
    }
}


class Program
{

    static void Main()
    {
        Console.WriteLine("Bir işlem giriniz (örnek: 3+2 veya 5-3*8): ");
        string ?input = Console.ReadLine();

        try
        {
            var result = Solution.Calculate(input);
            Console.WriteLine($"Sonuç: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hatalı giriş! Lütfen doğru formatta bir işlem giriniz.");
            Console.WriteLine(ex.Message);
        }

    }

}
