namespace Projects.Src.UI
{
    public static class InputValidator
    {
        public static string GetValidString(string prompt)
        {
            while (true)
            {
                Console.Write($"  {prompt}");
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                UIHelper.ShowError("Input cannot be empty. Please try again.\n");
            }
        }

        public static int GetValidInt(string prompt)
        {
            while (true)
            {
                Console.Write($"  {prompt}");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                    return result;

                UIHelper.ShowError("Invalid format. Please enter a whole number.\n");
            }
        }

        public static decimal GetValidDecimal(string prompt)
        {
            while (true)
            {
                Console.Write($"  {prompt}");
                string? input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal result))
                    return result;

                UIHelper.ShowError("Invalid format. Please enter a valid decimal number.\n");
            }
        }

        public static DateTime GetValidDate(string prompt)
        {
            while (true)
            {
                Console.Write($"  {prompt}");
                string? input = Console.ReadLine();

                if (DateTime.TryParse(input, out DateTime result))
                    return result;

                UIHelper.ShowError("Invalid date format. Please use MM/DD/YYYY.\n");
            }
        }
    }
}
