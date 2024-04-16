Coordinate coordA = new Coordinate(0,1);
Coordinate coordB = new Coordinate(1,1);
Coordinate coordC = new Coordinate(1,2);
Coordinate coordD = new Coordinate(2,3);
Coordinate coordE = new Coordinate(3,3);

Console.WriteLine($"coordA (0,1) is next to coordB (1,1): True == {coordA.CheckIfNeighbor(coordB)}");
Console.WriteLine($"coordA (0,1) is next to coordC (1,2): True == {coordA.CheckIfNeighbor(coordC)}");
Console.WriteLine($"coordA (0,1) is next to coordD (2,3): False == {coordA.CheckIfNeighbor(coordD)}");
Console.WriteLine($"coordB (1,1) is next to coordE (3,3): False == {coordB.CheckIfNeighbor(coordE)}");


public struct Coordinate
{
    public int X { get; init; }
    public int Y { get; init; }

    public Coordinate ()
    {
        X = 0;
        Y = 0;
    }

    public Coordinate (int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool CheckIfNeighbor(Coordinate coord)
    {
        if(coord.X == X - 1 || coord.Y == Y - 1 || coord.X == X + 1 || coord.Y == Y + 1)
        {
            return true;
        }
        return false;
    }
}