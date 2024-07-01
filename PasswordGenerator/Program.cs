using System.ComponentModel.DataAnnotations;
using System.Text;

public class PasswordGenerator
{
    static void Main()
    {
        string temp = "";
        Console.WriteLine("How long would you like the tempPassword to be? (15 is default)");
        if (!int.TryParse(Console.ReadLine(), out int length))
        {
            Console.WriteLine("Password length set to 15");
            length = 15;
        }
        else
        {
            length = Math.Abs(length);
            Console.WriteLine($"Password set to {length.ToString()}");
        }

        Console.WriteLine("Would you like to specify how many of which character you would like? (y/n)");
        temp = Console.ReadLine();
        while (!temp.StartsWith('n') && !temp.StartsWith('y'))
        {
            Console.WriteLine("Please choose one of the available options.");
            temp = Console.ReadLine();
        }

        if (temp.StartsWith('n'))
        {
            Generate(length, new());
        }
        else
        {
            Console.WriteLine("Insert the amount you want for each character type");
            List<int> specifications = new();
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.Write("Specials: ");
                        break;
                    case 1:
                        Console.Write("Lower case letters: ");
                        break;
                    case 2:
                        Console.Write("Upper case letters: ");
                        break;
                    case 3:
                        Console.Write("Numbers: ");
                        break;
                }
                temp = Console.ReadLine();
                while (!int.TryParse(temp, out int specification))
                {
                    Console.WriteLine("Please enter a valid amount.");
                    temp = Console.ReadLine();
                }
                specifications.Add(Math.Abs(int.Parse(temp)));
            }
            Generate(length, specifications);
        }
    }
    static void Generate(int length, List<int> specifications)
    {
        Random rand = new();
        string tempPassword = ""; // password without length constraints or random shuffle
        if (specifications.Sum() == 0)
        {
            for (int i = 0; i < length; i++)
            {
                tempPassword += (char)rand.Next(33, 127);
            }
        }
        else
        {
            for (int i = 0; i < specifications.Count(); i++)
            {
                for (int j = 0; j < specifications[i]; j++)
                {
                    if (i == 0) // adds randomly selected special chars
                    {
                        int num = rand.Next(33, 65);
                        if (num > 47)
                        {
                            num += 10;
                            if (num > 64)
                            {
                                num += 26;
                                if (num > 96) 
                                {
                                    num += 26;
                                }
                            }
                        }
                        tempPassword += (char)num;
                    } 
                    /* For future updates, if letters from the danish alphabet should be added, the rand.Next(x, y), y should be 3 larger than before
                       * This allows for another if statement to be made, which will provide the password generator with exclusive danish letters
                       * Smileys and chinese letters don't have corresponding ASCII codes, for them to be added, a string containing the possible characters shall serve as ASCII code */
                    else if (i == 1)
                    {
                        tempPassword += (char)rand.Next(97, 123); // lower case
                    }
                    else if (i == 2)
                    {
                        tempPassword += (char)rand.Next(65, 90); // upper case
                    }
                    else if (i == 3)
                    {
                        tempPassword += (char)rand.Next(48, 58); // numbers
                    }
                }
            }
            if (tempPassword.Length < length)
            {
                while (tempPassword.Length != length)
                {
                    tempPassword += (char)rand.Next(33, 127);
                }
            }
            else if (tempPassword.Length > length)
            {
                List<char> chars = tempPassword.ToList();
                for (int i = length; i < chars.Count();)
                {
                    chars.RemoveAt(rand.Next(0, chars.Count()));
                }
                tempPassword = "";
                foreach (char c in chars)
                {
                    tempPassword += c;
                }
            }
        }
        string password = "";
        foreach (char c in tempPassword)
        {
            password += ' '; // add spaces to avoid out of bounds error
        }
        for (int i = 0; i < tempPassword.Length; i++)
        {
            password = password.Insert(rand.Next(0, password.Length), tempPassword[i].ToString());
        }
        Console.WriteLine(RemoveSpaces(password));
    }
    static string RemoveSpaces(string line)
    {
        string result = "";
        foreach (char c in line)
        {
            if (c != ' ')
            {
                result += c;
            }
        }
        return result;
    }
}