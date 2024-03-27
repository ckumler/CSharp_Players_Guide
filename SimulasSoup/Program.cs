using System.Runtime.CompilerServices;

(SoupType type, SoupMainIngredient ingredient, SoupSeasoning seasoning) mySoup;

Console.WriteLine("Welcome to Simula's Soup Shop!\n");
Console.Write("What type of soup would you like? (Soup, Stew, Gumbo)");
while (!Enum.TryParse<SoupType>(Console.ReadLine(), out mySoup.type))
{
    WriteErrorText("Invalid input. Please try again.");
    Console.Write("What type of soup would you like? (Soup, Stew, Gumbo)");
}
Console.WriteLine();

Console.Write("What is the main ingredient? (Mushrooms, Chicken, Carrots, Potatoes)");
while (!Enum.TryParse<SoupMainIngredient>(Console.ReadLine(), out mySoup.ingredient))
{
    WriteErrorText("Invalid input. Please try again.");
    Console.Write("What is the main ingredient? (Mushrooms, Chicken, Carrots, Potatoes)");
}
Console.WriteLine();

Console.Write("What seasoning would you like? (Spicy, Salty, Sweet)");
while (!Enum.TryParse<SoupSeasoning>(Console.ReadLine(), out mySoup.seasoning))
{
    WriteErrorText("Invalid input. Please try again.");
    Console.Write("What seasoning would you like? (Spicy, Salty, Sweet)");
}
Console.WriteLine();

Console.WriteLine($"You have ordered a {mySoup.seasoning} {mySoup.ingredient} {mySoup.type}.");

Console.WriteLine("\nThank you for visiting Simula's Soup Shop!");
Console.WriteLine("Press any key to exit.");
Console.ReadKey();



void WriteErrorText(string text) {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(text);
    Console.ResetColor();
}

enum SoupType { Soup, Stew, Gumbo};
enum SoupMainIngredient { Mushrooms, Chicken, Carrots, Potatoes };
enum SoupSeasoning { Spicy, Salty, Sweet };

