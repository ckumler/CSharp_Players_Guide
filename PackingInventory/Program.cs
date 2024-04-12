Game game = new Game();
game.Run();

public class Game
{
    public void Run()
    {
        bool running = true;
        int selection;
        Pack pack = new Pack(15, 9f, 12f);

        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("* Pack your inventory!");
            Console.WriteLine("* Select an item to add to your pack! (1 through 6, or type exit)");
            Console.WriteLine("");
            Console.ResetColor();
            DisplayShop();
            DisplayPack(pack);
            Console.Write("Select an item to add to your pack : ");

            string userInput = Console.ReadLine() ?? " ";
            if (userInput.ToLower() == "exit")
            {
                running = false;
            }
            else if (int.TryParse(userInput, out selection) && selection >= 1 && selection <= 6)
            {
                switch (selection)
                {
                    case 1:
                        pack.Add(new Arrow());
                        break;
                    case 2:
                        pack.Add(new Bow());
                        break;
                    case 3:
                        pack.Add(new Rope());
                        break;
                    case 4:
                        pack.Add(new Water());
                        break;
                    case 5:
                        pack.Add(new Ration());
                        break;
                    case 6:
                        pack.Add(new Sword());
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number from 1 to 6 or type 'exit' to leave.");
            }

        } while (running);


    }

    private void DisplayShop()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($@"|--------------------------------------|");
        Console.WriteLine($@"|            SHOP INVENTORY            |");
        Console.WriteLine($@"|--------------------------------------|");
        Console.WriteLine($@"| Num | Item     | Weight   | Volume   |");
        Console.WriteLine($@"|-----+----------+----------+----------|");
        Console.WriteLine($@"| 1   | Arrow    | 0.1      | 0.05     |");
        Console.WriteLine($@"| 2   | Bow      | 1        | 4        |");
        Console.WriteLine($@"| 3   | Rope     | 1        | 1.5      |");
        Console.WriteLine($@"| 4   | Water    | 2        | 3        |");
        Console.WriteLine($@"| 5   | Food     | 1        | 0.5      |");
        Console.WriteLine($@"| 6   | Sword    | 5        | 3        |");
        Console.WriteLine($@"|--------------------------------------|");
        Console.WriteLine("");
        Console.ResetColor();
    }

    private void DisplayPack(Pack pack)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($@"|--------------------------------------|");
        Console.WriteLine($@"|            PACK INVENTORY            |");
        Console.WriteLine($@"|--------------------------------------|");
        Console.WriteLine($@"| Num | Item     | Weight   | Volume   |");
        Console.WriteLine($@"|-----+----------+----------+----------|");

        int iter = 0;
        foreach (InventoryItem item in pack.Items)
        {
            iter++;
            Console.WriteLine($@"| {iter,-3} | {item.GetType(),-8} | {item.weight,-8} | {item.volume,-8} |");
        }

        Console.WriteLine($@"|--------------------------------------|");
        Console.WriteLine($@"| {"ITM:" + pack.CurrentItems + "/" + pack.MaxItems,-10} {"WGT:" + Math.Truncate(pack.CurrentWeight*100)/100 + "/" + pack.MaxWeight,-12} {"VOL:" + Math.Truncate(pack.CurrentVolume*100)/100 + "/" + pack.MaxVolume,-12} |");
        Console.WriteLine($@"|--------------------------------------|");
        Console.WriteLine("");
        Console.ResetColor();
    }
}



//===============================================================
//Inventory Items
//Class Definitions
//===============================================================
public class InventoryItem
{
    public float weight;
    public float volume;

    public InventoryItem(float weight, float volume)
    {
        this.weight = weight;
        this.volume = volume;
    }
}

public class Arrow : InventoryItem
{
    public Arrow() : base(0.1f, 0.05f)
    {

    }
}

public class Bow : InventoryItem
{
    public Bow() : base(1f, 4f)
    {

    }
}

public class Rope : InventoryItem
{
    public Rope() : base(1f, 1.5f)
    {

    }
}

public class Water : InventoryItem
{
    public Water() : base(2f, 3f)
    {

    }
}

public class Ration : InventoryItem
{
    public Ration() : base(1f, 0.5f)
    {

    }
}
public class Sword : InventoryItem
{
    public Sword() : base(5f, 3f)
    {

    }
}

//===============================================================
//Packs
//Class Definitions
//===============================================================

public class Pack
{
    List<InventoryItem> _items;
    int _maxItems;
    float _maxWeight;
    float _maxVolume;

    int _currentItems = 0;
    float _currentWeight = 0f;
    float _currentVolume = 0f;

    public int CurrentItems { get { return _currentItems; } }
    public float CurrentWeight { get { return _currentWeight; } }
    public float CurrentVolume { get { return _currentVolume; } }
    public float MaxItems { get { return _maxItems; } }
    public float MaxWeight { get { return _maxWeight; } }
    public float MaxVolume { get { return _maxVolume; } }
    public List<InventoryItem> Items { get { return _items; } }

    public Pack(int maxItems, float maxWeight, float maxVolume)
    {
        _items = new List<InventoryItem>();
        _maxItems = maxItems;
        _maxWeight = maxWeight;
        _maxVolume = maxVolume;
    }

    public bool Add(InventoryItem item)
    {
        if (_currentItems + 1 <= _maxItems && _currentVolume + item.volume <= _maxVolume && _currentWeight + item.weight < _maxWeight)
        {
            _currentItems++;
            _currentWeight += item.weight;
            _currentVolume += item.volume;
            _items.Add(item);
            return true;
        }

        return false;
    }
}