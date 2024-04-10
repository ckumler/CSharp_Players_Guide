public class Program {
    public static void Main() {
        Point[] points = [new Point(2, 3), new Point(-4, 0)];

        foreach (Point p in points) {
            Console.WriteLine($"({p.X},{p.Y})");
        }
    }
}

public class Point {
    public int X { get; init; }
    public int Y { get; init; }

    public Point(int x , int y) {
        X = x;
        Y = y;
    }

    public Point() {
        X = 0;
        Y = 0;
    }
}


