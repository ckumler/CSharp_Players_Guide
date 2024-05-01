Game game = new Game();
game.Run();


public class Game
{
    public bool gameOver = false;

    public Map map = Map.CreateNewMap(MapType.Medium);
    public Player player = new Player();
    public InputController inputController = new InputController();

    public void Run()
    {
        player.currentPosition = map.entrancePos;
        TextRenderer.ActionText("Welcome to The Fountain of Objects!");
        TextRenderer.EndOfRound();
        DrawMap();
        DisplayDetails();
        while (!gameOver)
        {

            ICommand command = inputController.GetCommand();
            Console.Clear();
            command.Execute(this);
            TextRenderer.EndOfRound();
            DrawMap();
            DisplayDetails();
        }
        Console.ReadKey();
    }

    private void DisplayDetails()
    {
        TextRenderer.WriteLine($"You are at coordinate {player.currentPosition}.\n");

        //TODO: Create Sensing class and supporting types
        if(player.currentPosition.X == map.entrancePos.X && player.currentPosition.Y == map.entrancePos.Y)
        {
            if (!map.fountainIsActivated)
            {
                TextRenderer.ActionText($"You see light in this room coming from outside the cavern. This is the entrance.");
            }
            else
            {
                //TODO: Create a better way to detect if game is over
                TextRenderer.ActionText($"The Fountain of Objects has been reactivated, and you have escaped with your life!\nYou win!");
                gameOver = true;
            }
        }
       
        if (player.currentPosition.X == map.fountainPos.X && player.currentPosition.Y == map.fountainPos.Y)
        {
            if (!map.fountainIsActivated) 
            {
                TextRenderer.ActionText($"You hear water dripping in this room. The Fountain of Objects is here!");
            }
            else
            {
                TextRenderer.ActionText($"You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
            }
        }

        foreach (Coordinate pit in map.pitPosArray)
        {
            if (pit.X == player.currentPosition.X && pit.Y == player.currentPosition.Y)
            {
                //do pit logic
                TextRenderer.ActionText($"You fell in to a pit!\nYou lose!");
                gameOver = true;
                break;
            } else if (player.currentPosition.CheckIfNeighbor(pit))
            {
                TextRenderer.ActionText($"You feel a draft. There is a pit in a nearby room.");
                //warn about pit
                break;
            }
        }

        Console.WriteLine();
    }

    private void DrawMap()
    {
        
        for (int i = map.mapRows - 1; i >= 0; i--)
        {
            //Draw top border
            for (int ia = 0; ia < map.mapCols + 1; ia++)
            {
                TextRenderer.DrawMap("+---");
            }
            Console.WriteLine();

            //Draw cells
            TextRenderer.DrawMap("|");
            for (int j = 0; j < map.mapCols; j++)
            {
                
                bool isPit = false;
                foreach(Coordinate pit in map.pitPosArray)
                {
                    if (pit.X == j && pit.Y == i) {
                        isPit = true; 
                    }
                }


                if (i == player.currentPosition.Y && j == player.currentPosition.X)
                {
                    TextRenderer.DrawMapAccent(" @ ");
                }
                else if (i == map.entrancePos.Y && j == map.entrancePos.X)
                {
                    TextRenderer.DrawMapAccent(" E ");
                }
                else if (i == map.fountainPos.Y && j == map.fountainPos.X)
                {
                    if (map.fountainIsActivated) 
                    {
                        TextRenderer.DrawMapAccent(" F ");
                    }
                    else
                    {
                        TextRenderer.DrawMapAccent(" f ");
                    }

                    
                }
                else if(isPit)
                {
                    TextRenderer.DrawMapAccent($" P ");
                }
                else
                {
                    TextRenderer.DrawMapAccent($"   ");
                }
                TextRenderer.DrawMap($"|");

                //if end of row, draw row identifier
                if (j == map.mapCols - 1) { TextRenderer.DrawMapAccent($" {i} "); }

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
        for (int ia = 0; ia < map.mapCols + 1; ia++)
        {
            TextRenderer.DrawMap("+---");
        }
        Console.WriteLine();
        TextRenderer.DrawMap("|");
        for (int i = 0; i < map.mapCols; i++)
        {
            TextRenderer.DrawMapAccent($@"{TextRenderer.CenterAlign($"{i}", 3)}");
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
                // short inputs - single letter
                if (input.Length == 1)
                {
                    switch (input[0]?.ToUpper() ?? " ")
                    {
                        case "N":
                            command = new MoveCommand(Direction.North);
                            break;
                        case "E":
                            command = new MoveCommand(Direction.East);
                            break;
                        case "S":
                            command = new MoveCommand(Direction.South);
                            break;
                        case "W":
                            command = new MoveCommand(Direction.West);
                            break;
                        case "F":
                            command = new ActivateFountainCommand();
                            break;

                        default:
                            TextRenderer.ErrorText("Please input a proper action");
                            break;
                    }
                }
                else
                {
                    TextRenderer.ErrorText("Please input a proper action");
                    continue;
                }
            }
            else if (input[0].ToLower() == "enable" && input[1].ToLower() == "fountain") {
                command = new ActivateFountainCommand();
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
                            TextRenderer.ErrorText("Please input a proper action");
                            continue;
                    }
                } else {
                    TextRenderer.ErrorText("Please input a proper action");
                    continue;
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

public class ActivateFountainCommand() : ICommand
{
    public void Execute(Game game)
    {
        if(game.player.currentPosition.X == game.map.fountainPos.X && game.player.currentPosition.Y == game.map.fountainPos.Y)
        {
            if (game.map.fountainIsActivated) 
            {
                TextRenderer.ErrorText($"There fountain is already activated."); 
            } else {
                game.map.fountainIsActivated = true;
                TextRenderer.ActionText($"You have activated the fountain.");
            }
        } else {
            TextRenderer.ErrorText($"The fountain is not in this room.");
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
    public Coordinate entrancePos;
    public Coordinate fountainPos;
    public Coordinate[] pitPosArray=[];
    public bool fountainIsActivated;


    public Map(int rows, int cols)
    {
        mapRows = rows;
        mapCols = cols;
        rooms = new Room[rows, cols];
        fountainIsActivated = false;
    }

    public RoomType GetRoomTypeAt(Coordinate coord)
    {
        return rooms[coord.X, coord.Y].GetRoomType();
    }

    public static Map CreateNewMap(MapType mapType)
    {
        const int SMALL_SIZE = 4;
        const int MEDIUM_SIZE = 6;
        const int LARGE_SIZE = 8;
        const int VERY_LARGE_SIZE = 10;
        
        Map map;
        Coordinate entranceCoord;
        Coordinate fountainCoord;
        Coordinate[] pitsCoords;


        switch (mapType)
        {
            case MapType.Small:
                map = new Map(SMALL_SIZE, SMALL_SIZE);
                entranceCoord = new Coordinate(0, 0);
                fountainCoord = new Coordinate(3, 2);
                pitsCoords = [new Coordinate(1, 2)];
                break;
            case MapType.Medium:
                map = new Map(MEDIUM_SIZE, MEDIUM_SIZE);
                entranceCoord = new Coordinate(3, 0);
                fountainCoord = new Coordinate(0, 5);
                pitsCoords = [new Coordinate(2, 2), new Coordinate(4,4)];
                break;
            case MapType.Large:
                map = new Map(LARGE_SIZE, LARGE_SIZE);
                entranceCoord = new Coordinate(0, 4);
                fountainCoord = new Coordinate(2, 8);
                pitsCoords = [new Coordinate(1, 1), new Coordinate(3, 6), new Coordinate(4, 3), new Coordinate(7, 7)];
                break;
            case MapType.VeryLarge:
                map = new Map(VERY_LARGE_SIZE, VERY_LARGE_SIZE);
                entranceCoord = new Coordinate(5, 5);
                fountainCoord = new Coordinate(1, 9);
                pitsCoords = [new Coordinate(1, 1), new Coordinate(3, 1), new Coordinate(4,3), new Coordinate(4, 7), new Coordinate(6, 2), new Coordinate(7, 5), new Coordinate(8, 9), new Coordinate(9, 1)];
                break;
            default:
                map = new Map(MEDIUM_SIZE, MEDIUM_SIZE);
                entranceCoord = new Coordinate(3, 0);
                fountainCoord = new Coordinate(0, 5);
                pitsCoords = [new Coordinate(2, 2), new Coordinate(4, 4)];
                break;
        }

        //initialize and populate with default
        for (int i = 0; i < map.mapRows; i++)
        {
            for (int j = 0; j < map.mapCols; j++)
            {
                // place entrance, fountain and fill rest with empty
                if (i == entranceCoord.X && j == entranceCoord.Y)
                {
                    //Console.WriteLine($"{i},{j}: Entrance");
                    map.rooms[i, j] = new Room(new Coordinate(i, j), RoomType.Entrance);
                    map.entrancePos = new Coordinate(i, j);
                }
                if (i == fountainCoord.X && j == fountainCoord.Y)
                {
                    //Console.WriteLine($"{i},{j}: FountainRoom");
                    map.rooms[i, j] = new Room(new Coordinate(i, j), RoomType.FountainRoom);
                    map.fountainPos = new Coordinate(i, j);
                }
                else
                {
                    //Console.WriteLine($"{i},{j}: Empty");
                    map.rooms[i, j] = new Room(new Coordinate(i, j), RoomType.Empty);
                }
            }
        }

        //add pits to map
        map.pitPosArray = pitsCoords;
        foreach (Coordinate pitCoord in pitsCoords)
        {
            if (pitCoord.X < map.mapRows && pitCoord.Y < map.mapCols)
            {
                //Console.WriteLine($"{i},{j}: Pit");
                map.rooms[pitCoord.X, pitCoord.Y] = new Room(new Coordinate(pitCoord.X, pitCoord.Y), RoomType.Pit);
            }
        }

        return map;
    }


    public bool CheckIfIsOnMap(Coordinate coord)
    {
        if (coord.X >= 0 && coord.Y >= 0 && coord.X <= rooms.GetLength(0) - 1 && coord.Y <= rooms.GetLength(1) - 1)
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
    Small,
    Medium,
    Large,
    VeryLarge,
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
    Pit,
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

