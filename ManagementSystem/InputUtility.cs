using System;
using System.Globalization;
using System.Linq;

namespace ManagementSystem
{
    public static class InputUtility
    {
        public static int GetIntegerWithOptionalRange(string prompt, bool setMinMaxLimits = false, int min = 0, int max = 0)
        {
            if (setMinMaxLimits && min > max)
            {
                throw new ArgumentException("Minimum should not exceed maximum.");
            }

            int number = 0;
            bool inputIsValid = false;

            do
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("No input received. Please enter an integer value.\n");
                    continue;
                }

                inputIsValid = int.TryParse(input, out number);

                if (!inputIsValid)
                {
                    Console.WriteLine("Invalid input. Please enter an integer value.\n");
                }
                else if (setMinMaxLimits && (number < min || number > max))
                {
                    inputIsValid = false;
                    Console.WriteLine($"Invalid input. The number must be within the range {min} to {max}.\n");
                }
            } while (!inputIsValid);

            return number;
        }

        public static string GetNonEmptyString(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be blank. Please try again. ");
                input = Console.ReadLine();
            }

            return input!;
        }


        public static string GetValidTitleCaseName(string prompt)
        {
            string? input = null;

            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be blank. Please try again.\n");
                    continue;
                }

                if (input.Any(char.IsDigit))
                {
                    Console.WriteLine("The name cannot have any numbers. Please try again.\n");
                    continue;
                }

                if (input.Any(char.IsPunctuation))
                {
                    Console.WriteLine("The name cannot have any punctuation characters. Please try again.\n");
                    continue;
                }

                // Normalize spaces
                input = string.Join(" ", input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

                break;
            }

            // Apply title case transformation
            input = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());

            return input;
        }
    }
}

