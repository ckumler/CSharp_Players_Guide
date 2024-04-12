Game game = new Game();
do
{
    game.Run();
} while (true);


public class Game
{
    private string[] validCommands = new string[] { "on", "off", "north", "south", "east", "west" };


    public void Run()
    {
        Robot oldRobot = new Robot();

        Console.Write("Welcome to ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("The Old Robot");
        Console.ResetColor();
        Console.WriteLine("!");
        Console.WriteLine("Input three commands for the old robot to execute.");
        Console.Write("Available commands are :");
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (string command in validCommands) { Console.Write($" {command}"); }        
        Console.ResetColor();
        Console.WriteLine(".");
        Console.WriteLine("\nInput your commands!");
        LoadCommands(oldRobot, GetCommands());
        Console.WriteLine();
        oldRobot.Run();
        Console.Write("\nPress any key to continue, or '");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("x");
        Console.ResetColor();
        Console.WriteLine("' to exit\n");
        if (Console.ReadKey().Key == ConsoleKey.X) { Environment.Exit(0); }
        Console.Clear();
    }

    public string[] GetCommands()
    {
        string[] commands = new string[3];
        int commandsCollected = 0;

        do
        {
            string commandInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Command {commandsCollected + 1} : ");
                Console.ForegroundColor = ConsoleColor.Red;
                commandInput = Console.ReadLine()?.ToLower() ?? "empty";
                if (!validCommands.Contains(commandInput)) { continue; }
                else
                {
                    commands[commandsCollected] = commandInput;
                    break;
                }
            } while (true);
            commandsCollected++;
        } while (commandsCollected < 3);
        Console.ResetColor();
        return commands;
    }

    public void LoadCommands(Robot robot, string[] commands)
    {
        for (int i = 0; i < commands.Length; i++)
        {
            switch (commands[i])
            {
                case "on":
                    robot.Commands[i] = new OnCommand();
                    break;
                case "off":
                    robot.Commands[i] = new OffCommand();
                    break;
                case "north":
                    robot.Commands[i] = new NorthCommand();
                    break;
                case "south":
                    robot.Commands[i] = new SouthCommand();
                    break;
                case "east":
                    robot.Commands[i] = new EastCommand();
                    break;
                case "west":
                    robot.Commands[i] = new WestCommand();
                    break;

                default:
                    break;
            }
        }
    }
}


public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }
    public IRobotCommand?[] Commands { get; } = new IRobotCommand?[3];
    public void Run()
    {
        foreach (IRobotCommand? command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}

public interface IRobotCommand
{
    void Run(Robot robot);
}

public class OnCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        robot.IsPowered = true;
    }
}

public class OffCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        robot.IsPowered = false;
    }
}

public class NorthCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered) { robot.Y++; }
    }
}

public class SouthCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered) { robot.Y--; }
    }
}
public class EastCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered) { robot.X++; }
    }
}

public class WestCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered) { robot.X--; }
    }
}