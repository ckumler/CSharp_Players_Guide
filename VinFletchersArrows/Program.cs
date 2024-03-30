﻿ArrowHeadType myArrowHeadType;
ArrowFletchingType myArrowFletchingType;
int myArrowLength;

Console.WriteLine("Welcome to Vin Fletcher's Arrow Shop!\n");
Console.Write("What type of arrowhead would you like? (Steel, Obsidian, Wool)");
while (!Enum.TryParse<ArrowHeadType>(Console.ReadLine(), out myArrowHeadType)) {
    WriteErrorText("Invalid input. Please try again.");
    Console.Write("What type of arrowhead would you like? (Steel, Obsidian, Wool)");
}
Console.WriteLine();
Console.Write("What type of fletching would you like? (Plastic, TurkeyFeather, GooseFeather)");
while (!Enum.TryParse<ArrowFletchingType>(Console.ReadLine(), out myArrowFletchingType)) {
    WriteErrorText("Invalid input. Please try again.");
    Console.Write("What type of fletching would you like? (Plastic, TurkeyFeather, GooseFeather)");
}
Console.WriteLine();
Console.Write("What length would you like the arrow to be? (60-100 cm)");
while (!int.TryParse(Console.ReadLine(), out myArrowLength) || myArrowLength < 60 || myArrowLength > 100) {
    WriteErrorText("Invalid input. Please try again.");
    Console.Write("What length would you like the arrow to be? (60-100 cm)");
}
Console.WriteLine();

Arrow myArrow = new Arrow(myArrowHeadType, myArrowFletchingType, myArrowLength);
Console.WriteLine($"You have ordered an arrow with a {myArrowHeadType} arrowhead, {myArrowFletchingType} fletching, and a length of {myArrowLength} cm.");
Console.WriteLine($"The cost of this arrow is {myArrow.GetCost()} gold.");




void WriteErrorText(string text) {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(text);
    Console.ResetColor();
}

class Arrow {
    private ArrowHeadType ArrowHeadType { get; init; }
    private ArrowFletchingType ArrowFletchingType { get; init; }
    private int ArrowLength { get; init; }

    public Arrow(ArrowHeadType arrowHeadType, ArrowFletchingType arrowFletchingType, int arrowLength) {
        if (arrowLength < 60 || arrowLength > 100) {
            throw new ArgumentOutOfRangeException(nameof(arrowLength), "Arrow length must be between 60 and 100.");
        }

        ArrowHeadType = arrowHeadType;
        ArrowFletchingType = arrowFletchingType;
        ArrowLength = arrowLength;
    }

    public float GetCost() {
        float cost = 0.05f * ArrowLength;
        switch (ArrowHeadType) {
            case ArrowHeadType.Steel:
                cost += 10;
                break;
            case ArrowHeadType.Obsidian:
                cost += 5;
                break;
            case ArrowHeadType.Wool:
                cost += 3;
                break;
        }
        switch (ArrowFletchingType) {
            case ArrowFletchingType.Plastic:
                cost += 10;
                break;
            case ArrowFletchingType.TurkeyFeather:
                cost += 5;
                break;
            case ArrowFletchingType.GooseFeather:
                cost += 3;
                break;
        }
        return cost;
    }
}

enum ArrowHeadType { Steel, Obsidian, Wool };
enum ArrowFletchingType { Plastic, TurkeyFeather, GooseFeather };