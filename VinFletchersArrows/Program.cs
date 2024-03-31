ArrowHeadType myArrowHeadType;
ArrowFletchingType myArrowFletchingType;
int myArrowLength;
Arrow? myArrow = null;

Console.WriteLine("Welcome to Vin Fletcher's Arrow Shop!\n");

Console.WriteLine("Would you like to order a premade arrow or customize your own? (Premade, Custom)");
OrderType myOrderType;
while (!Enum.TryParse<OrderType>(Console.ReadLine(), out myOrderType)) {
    WriteErrorText("Invalid input. Please try again.");
    Console.WriteLine("Would you like to order a premade arrow or customize your own? (Premade, Custom)");
}
Console.WriteLine();

if (myOrderType == OrderType.Premade) {
    Console.Write("What type of premade arrow would you like? (Beginner, Marksman, Elite)");
    PremadeArrowType myPremadeArrowType;
    while (!Enum.TryParse<PremadeArrowType>(Console.ReadLine(), out myPremadeArrowType)) {
        WriteErrorText("Invalid input. Please try again.");
        Console.Write("What type of premade arrow would you like? (Beginner, Marksman, Elite)");
    }
    Console.WriteLine();

    switch (myPremadeArrowType) {
        case PremadeArrowType.Beginner:
            myArrow = Arrow.CreateBeginnerArrow();
            break;
        case PremadeArrowType.Marksman:
            myArrow = Arrow.CreateMarksmanArrow();
            break;
        case PremadeArrowType.Elite:
            myArrow = Arrow.CreateEliteArrow();
            break;
    }    
} else {
    Console.Write("What type of arrowhead would you like? (Steel, Obsidian, Wood)");
    while (!Enum.TryParse<ArrowHeadType>(Console.ReadLine(), out myArrowHeadType)) {
        WriteErrorText("Invalid input. Please try again.");
        Console.Write("What type of arrowhead would you like? (Steel, Obsidian, Wood)");
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

    myArrow = new Arrow(myArrowHeadType, myArrowFletchingType, myArrowLength);
}

Console.WriteLine($"You have ordered an arrow with a {myArrow?.ArrowHeadType} arrowhead, {myArrow?.ArrowFletchingType} fletching, and a length of {myArrow?.ArrowLength} cm.");
Console.WriteLine($"The cost of this arrow is {myArrow?.GetCost()} gold.");

void WriteErrorText(string text) {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(text);
    Console.ResetColor();
}

class Arrow {
    public ArrowHeadType ArrowHeadType { get; init; }
    public ArrowFletchingType ArrowFletchingType { get; init; }
    public int ArrowLength { get; init; }

    public Arrow(ArrowHeadType arrowHeadType, ArrowFletchingType arrowFletchingType, int arrowLength) {
        if (arrowLength < 60 || arrowLength > 100) {
            throw new ArgumentOutOfRangeException(nameof(arrowLength), "Arrow length must be between 60 and 100.");
        }

        ArrowHeadType = arrowHeadType;
        ArrowFletchingType = arrowFletchingType;
        ArrowLength = arrowLength;
    }

    public static Arrow CreateBeginnerArrow() {
        return new Arrow(ArrowHeadType.Wood, ArrowFletchingType.GooseFeather, 75);
    }

    public static Arrow CreateMarksmanArrow() {
        return new Arrow(ArrowHeadType.Steel, ArrowFletchingType.GooseFeather, 65);
    }

    public static Arrow CreateEliteArrow() {
        return new Arrow(ArrowHeadType.Steel, ArrowFletchingType.Plastic, 95);
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
            case ArrowHeadType.Wood:
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

enum ArrowHeadType { Steel, Obsidian, Wood };
enum ArrowFletchingType { Plastic, TurkeyFeather, GooseFeather };
enum OrderType { Premade, Custom };
enum PremadeArrowType { Beginner, Marksman, Elite };