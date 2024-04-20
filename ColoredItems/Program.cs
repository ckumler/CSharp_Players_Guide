

ColoredItem<Sword> ci1 = new ColoredItem<Sword> (ConsoleColor.Blue);
ColoredItem<Bow> ci2 = new ColoredItem<Bow> (ConsoleColor.Green);
ColoredItem<Axe> ci3 = new ColoredItem<Axe> (ConsoleColor.Red);

ci1.Display();
ci2.Display();
ci3.Display();  


public class Sword { }
public class Bow { }
public class Axe { }

public class ColoredItem<T>
{
    public Type type = typeof(T);
    public ConsoleColor Color { get; set; }

    public ColoredItem(ConsoleColor color)
    {
        Color = color;
    }

    public void Display()
    {
        Console.WriteLine($" Type={type}  :  Color={Color} ");
    }
}
