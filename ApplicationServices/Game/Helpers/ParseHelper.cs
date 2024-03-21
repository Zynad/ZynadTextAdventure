using System.Globalization;

namespace ApplicationServices.Game.Helpers;
public static class ParseHelper
{
    public static TEnum AskForEnum<TEnum>(string question) where TEnum : struct
    {
        Console.Write(question);
        TEnum result;
        while (!Enum.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Invalid choice, please try again.");
            Console.Write(question);
        }
        return result;
    }

    public static string AskForString(string question)
    {
        Console.Write(question);
        return Console.ReadLine().ToLower();
    }
    
    public static string AskForName(string question)
    {
        Console.Write(question);
        string input = Console.ReadLine().ToLower();
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        return textInfo.ToTitleCase(input);
    }

    public static bool AskForBool(string question)
    {
        Console.Write(question);
        bool result;
        while (!bool.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Invalid choice, please try again.");
            Console.Write(question);
        }
        return result;
    }
    
    public static DateTime AskForDateTime(string question)
    {
        Console.Write(question);
        DateTime result;
        while (!DateTime.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Invalid date format, please try again.");
            Console.Write(question);
        }
        return result;
    }
    
    public static double AskForDouble(string question)
    {
        Console.Write(question);
        double result;
        while (!double.TryParse(Console.ReadLine(), out result) || result <= 0)
        {
            Console.WriteLine("That was not a correct number, please try again.");
            Console.Write(question);
        }
        return result;
    }
    
    public static int AskForInt(string question)
    {
        Console.Write(question);
        int result;
        while (!int.TryParse(Console.ReadLine(), out result) || result <= 0)
        {
            Console.WriteLine("That was not a correct number, please try again.");
            Console.Write(question);
        }
        return result;
    }
}
