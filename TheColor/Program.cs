public class Program 
{
    static void Main()
    {
        Color custom = new Color(100,100,100);
        Color purple = Color.Purple;

        Console.WriteLine($"custom = {custom}");
        Console.WriteLine($"purple = {purple}");
    }
}

public class Color 
{
    public (byte Red, byte Green, byte Blue) RGB { get; set; }

    public static readonly Color White = new Color(255, 255, 255);
    public static readonly Color Black = new Color(0, 0, 0);
    public static readonly Color LightGrey = new Color(64, 64, 64);
    public static readonly Color Grey = new Color(128, 128, 128);
    public static readonly Color DarkGrey = new Color(192, 192, 192);
    public static readonly Color Red = new Color(255, 0, 0);
    public static readonly Color Green = new Color(0, 255, 0);
    public static readonly Color Blue = new Color(0,0,255);
    public static readonly Color Orange = new Color(255, 128,0);
    public static readonly Color Yellow = new Color(255, 255, 0);
    public static readonly Color Purple = new Color(128, 0, 255);


    public Color(byte red, byte green, byte blue) 
    {
        RGB = (red, green, blue);
    }

    public Color () 
    {
        RGB = (0,0,0);
    }

    public override string ToString()
    {
        return $"Red: {RGB.Red}, Green: {RGB.Green}, Blue: {RGB.Blue}";
    }
}