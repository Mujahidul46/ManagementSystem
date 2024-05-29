using System;
using System.Globalization;
using System.Linq;

namespace ManagementSystem
{
    public static class InputUtility
    {
        /// <summary>
        /// Validates user input until a valid integer is entered. Optionally you can specify a range that the integer must be within.
        /// </summary>
        /// <param name="prompt">The message displayed to the user prompting for input.</param>
        /// <param name="setMinMaxLimits">A boolean indicating whether to enforce a range for the integer.</param>
        /// <param name="min">The minumum value allowed, inclusive. Only used if <paramref name="setMinMaxLimits"/> is true.</param>
        /// <param name="max">The maximum value allowed, inclusive. Only used if <paramref name="setMinMaxLimits"/> is true.</param>
        /// <returns>An integer entered by the user.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
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

        /// <summary>
        /// Returns a non-empty string.
        /// </summary>
        /// <param name="prompt">The message displayed to the user prompting for input.</param>
        /// <returns>A non-empty string entered by the user.</returns>
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

        /// <summary>
        /// Returns a name in the title case format. E.g. Alice Smith
        /// </summary>
        /// <param name="prompt">The message displayed to the user prompting for a name to be input.</param>
        /// <returns>A valid title case name.</returns>
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

