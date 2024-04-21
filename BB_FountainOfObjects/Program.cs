using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

Game game = new Game();
game.Run();


public class Game
{
    public Map map = Map.CreateNewMap(MapType.VeryBig, new Coordinate(0, 0), new Coordinate(0, 2));
    public Player player = new Player();
    public InputController inputController = new InputController();

    public void Run()
    {
        TextRenderer.ActionText("Welcome to The Fountain of Objects!");
        TextRenderer.EndOfRound();
        while (true)
        {
            DrawMap();
            DisplayDetails();
            ICommand command = inputController.GetCommand();
            Console.Clear();
            command.Execute(this);
            TextRenderer.EndOfRound();
        }

    }

    private void DisplayDetails()
    {
        TextRenderer.WriteLine($"You are at coordinate {player.currentPosition}.");
    }

    private void DrawMap()
    {
        for (int i = map.mapRows-1; i >= 0; i--)
        {

            for (int ia = 0; ia < map.mapCols+1; ia++)
            {
                TextRenderer.DrawMap("+---");
            }
            Console.WriteLine();


            TextRenderer.DrawMap("|");
            for (int j = 0; j < map.mapCols; j++)
            {


                if (i == player.currentPosition.Y && j == player.currentPosition.X) { TextRenderer.DrawMapAccent($"{i},{j}"); } else { TextRenderer.DrawMapAccent($"   "); }
                


                TextRenderer.DrawMap($"|");
                //if end of row, draw row identifier
                if (j == map.mapCols - 1){TextRenderer.DrawMapAccent($" {i} ");}

                //if at the end of last row, draw footer
                if (i == 0 && j == map.mapCols - 1) { DrawFooter(); }                
            }
            
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private void DrawFooter()
    {
        Console.WriteLine();
        for (int ia = 0; ia < map.mapCols+1; ia++)
        {
            TextRenderer.DrawMap("+---");
        }
        Console.WriteLine();
        TextRenderer.DrawMap("|");
        for (int i = 0; i < map.mapCols; i++)
        {
            TextRenderer.DrawMapAccent($@"{TextRenderer.CenterAlign($"{i}",3)}");
            TextRenderer.DrawMap("|");
        }
    }
}

public class Player
{
    public Coordinate currentPosition;

    public Player()
    {
        currentPosition = new Coordinate(0, 0);
    }

    public Player(Coordinate coord)
    {
        currentPosition = coord;
    }
}

public class InputController
{
    public ICommand GetCommand()
    {
        ICommand? command = null;

        while (command == null)
        {
            TextRenderer.AskText("What would you like to do?");
            string[]? input = Console.ReadLine()?.ToLower().Split(" ") ?? new string[1] { "empty" };
            if (input.Length <= 1 || input.Length >= 3)
            {
                continue;
            }
            else
            {
                Direction commandDirection = GetCommandDirection(input[1]);
                if (commandDirection != Direction.None)
                {
                    switch (GetCommandAction(input[0]))
                    {
                        case CommandAction.Move:
                            command = new MoveCommand(commandDirection);
                            break;
                        default:
                            continue;
                    }
                }
            }
        }

        return command ?? throw new InvalidOperationException("Failed to create a command.");
    }

    public CommandAction GetCommandAction(string str)
    {
        switch (str.ToLower())
        {
            case "move":
                return CommandAction.Move;
            default:
                TextRenderer.ErrorText($"'{str}' is not a valid action!!");
                return CommandAction.None;
        }
    }

    public Direction GetCommandDirection(string str)
    {
        switch (str.ToLower())
        {
            case "north":
                return Direction.North;
            case "east":
                return Direction.East;
            case "south":
                return Direction.South;
            case "west":
                return Direction.West;
            default:
                return Direction.None;
        }
    }
}

public class MoveCommand(Direction direction) : ICommand
{
    public Direction direction = direction;

    public void Execute(Game game)
    {
        Coordinate newCoord = game.map.GetNewCoordByDirection(game.player.currentPosition, direction);
        if (game.map.CheckIfIsOnMap(newCoord))
        {
            game.player.currentPosition = newCoord;
            TextRenderer.ActionText($"You have moved {direction}.");
        }
        else
        {
            TextRenderer.ActionText($"Unable to move {direction}, there is a wall.");
        }
    }


}

public interface ICommand
{
    void Execute(Game game);
}

public enum CommandAction
{
    None,
    Move,
}

public enum Direction
{
    None,
    North,
    East,
    South,
    West,
}

public class Map
{
    private Room[,] rooms;
    public int mapRows;
    public int mapCols;


    public Map(int rows, int cols)
    {
        mapRows = rows;
        mapCols = cols;
        rooms = new Room[rows, cols];
    }

    public RoomType GetRoomTypeAt(Coordinate coord)
    {
        return rooms[coord.X, coord.Y].GetRoomType();
    }

    public static Map CreateNewMap(MapType mapType, Coordinate entranceCoord, Coordinate fountainCoord)
    {
        Map map;

        switch (mapType)
        {
            case MapType.Default:
                map = new Map(4, 4);
                break;
            case MapType.VeryBig:
                map = new Map(10,10);
                break;
            default:
                map = new Map(4, 4);
                break;
        }

        //initialize and populate with default
        for (int i = 0; i < map.mapRows; i++)
        {
            for (int j = 0; j < map.mapCols; j++)
            {
                // 0,0 is entrance :: 0,2 is fountain :: all others are empty
                if (i == entranceCoord.X && j == entranceCoord.Y)
                {
                    //Console.WriteLine($"{i},{j}: Entrance");
                    map.rooms[i, j] = new Room(new Coordinate(i, j), RoomType.Entrance);
                }
                if (i == fountainCoord.X && j == fountainCoord.Y)
                {
                    //Console.WriteLine($"{i},{j}: FountainRoom");
                    map.rooms[i, j] = new Room(new Coordinate(i, j), RoomType.FountainRoom);
                }
                else
                {
                    //Console.WriteLine($"{i},{j}: Empty");
                    map.rooms[i, j] = new Room(new Coordinate(i, j), RoomType.Empty);
                }
            }
        }

        return map;
    }


    public bool CheckIfIsOnMap(Coordinate coord)
    {
        if (coord.X >= 0 && coord.Y >= 0 && coord.X <= rooms.GetLength(0)-1 && coord.Y <= rooms.GetLength(1)-1)
        {
            return true;
        }
        return false;
    }

    public Coordinate GetNewCoordByDirection(Coordinate coord, Direction direction)
    {
        var moveCoord = direction switch
        {
            Direction.North => new Coordinate(coord.X, coord.Y + 1),
            Direction.East => new Coordinate(coord.X + 1, coord.Y),
            Direction.South => new Coordinate(coord.X, coord.Y - 1),
            Direction.West => new Coordinate(coord.X - 1, coord.Y),
            _ => throw new ArgumentException("Could not find move coord, supply a proper Direction"),
        };
        return moveCoord;
    }
}
public enum MapType
{
    Default,
    VeryBig
}
public class Room
{
    private RoomType roomType { get; init; }
    private Coordinate coordinate { get; init; }

    public Room(RoomType type = RoomType.Empty)
    {
        coordinate = new Coordinate(0, 0);
        roomType = type;
    }

    public Room(Coordinate coord, RoomType type = RoomType.Empty)
    {
        coordinate = coord;
        roomType = type;
    }

    public RoomType GetRoomType()
    {
        return roomType;
    }

    public bool CheckIfNeighbor(Coordinate coord)
    {
        return coordinate.CheckIfNeighbor(coord);
    }
}

public enum RoomType
{
    Empty,
    Entrance,
    FountainRoom,
}

public struct Coordinate
{
    public int X { get; init; }
    public int Y { get; init; }

    public Coordinate()
    {
        X = 0;
        Y = 0;
    }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"{X}, {Y}";
    }

    public bool CheckIfNeighbor(Coordinate coord)
    {
        if (coord.X == X - 1 || coord.Y == Y - 1 || coord.X == X + 1 || coord.Y == Y + 1)
        {
            return true;
        }
        return false;
    }
}





public static class TextRenderer
{
    public static void WriteLine(string text, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
    }
    public static void Write(string text, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
    }

    public static void DebugText(string text, ConsoleColor color = ConsoleColor.DarkCyan)
    {
        Console.ForegroundColor = color;
        Console.WriteLine("DEBUG: " + text);
    }

    public static void ErrorText(string text, ConsoleColor color = ConsoleColor.Red)
    {
        Console.ForegroundColor = color;
        Console.WriteLine("ERROR: " + text);
    }
    public static void ActionText(string text, ConsoleColor color = ConsoleColor.DarkYellow)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
    }
    public static void AskText(string text, ConsoleColor questionColor = ConsoleColor.Green, ConsoleColor answerColor = ConsoleColor.Yellow)
    {
        Console.ForegroundColor = questionColor;
        Console.Write(text + " ");
        Console.ForegroundColor = answerColor;
    }
    public static void DrawMap(string text, ConsoleColor color = ConsoleColor.Magenta)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
    }
    public static void DrawMapNL(string text, ConsoleColor color = ConsoleColor.Magenta)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
    }
    public static void DrawMapAccent(string text, ConsoleColor color = ConsoleColor.Cyan)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
    }
    public static void EndOfRound()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"\n================================================================================\n");
    }

    public static string CenterAlign(string text, int width)
    {
        int spaces = width - text.Length;
        int padLeft = spaces / 2 + text.Length;
        return text.PadLeft(padLeft).PadRight(width);
    }
}

