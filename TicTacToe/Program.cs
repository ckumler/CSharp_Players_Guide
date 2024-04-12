TicTacToeGame game = new TicTacToeGame();
game.Run();

public class TicTacToeGame
{
    int turnCount = 0;
    int maxTurnCount = 8;
    bool gameIsOver = false;
    Board board = new Board();
    Player playerOne = new Player(Player.PlayerNumberEnum.One);
    Player playerTwo = new Player(Player.PlayerNumberEnum.Two);

    public void PlayGame()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Welcome to Tic-Tac-Toe!");
            Console.WriteLine($"Current Turn : {turnCount+1} - Current Players Turn : Player {turnCount % 2 + 1}\n");
            BoardRenderer.DrawBoard(board.spaces);
            Console.Write("\nSelect a Space (Numpad) : ");
            TakeTurn();
            gameIsOver = IsGameOver();
            if(gameIsOver) break;
            turnCount++;
        } while (!gameIsOver);
        Console.Clear();
        Console.WriteLine("Congratulations!");
        Console.WriteLine($"Player {turnCount % 2 + 1} has won!\n");
        BoardRenderer.DrawBoard(board.spaces);
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();

    }

    public void TakeTurn()
    {

        (int space, char token) move;
        do
        {
            if (turnCount % 2 == 0)
            {
                move = playerOne.GetMove();
            }
            else
            {
                move = playerTwo.GetMove();
            }
        } while (!board.MarkSpace(move.space, move.token));
    }

    public bool IsGameOver()
    {
        if (BoardStateAnalyzer.hasAPlayerWon(board.spaces) || turnCount >= maxTurnCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Test()
    {
        BoardRenderer.DrawTestBoard();
    }

    public void Run()
    {
        PlayGame();
    }

}

public class Board
{
    //spaces go from top left to bottom right - in numpad order 7,8,9,4,5,6,1,2,3
    public char[] spaces = [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '];
    private int minIndex = 0;
    private int maxIndex = 8;

    public bool MarkSpace(int space, char token)
    {
        //returns true if valid move, false if invalid move
        if (space >= minIndex && space <= maxIndex && spaces[space] == ' ')
        {
            spaces[space] = token;
            return true;
        }
        else
        {
            return false;
        }
    }
}

public static class BoardRenderer
{
    public static void DrawBoard(char[] spaces)
    {
        Console.WriteLine($" {spaces[0]} | {spaces[1]} | {spaces[2]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {spaces[3]} | {spaces[4]} | {spaces[5]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {spaces[6]} | {spaces[7]} | {spaces[8]} ");
    }


    private static char[] testSpaces = ['X', 'X', 'X', 'X', 'O', 'X', 'O', 'O', 'X'];
    public static void DrawTestBoard()
    {
        Console.WriteLine($" {testSpaces[0]} | {testSpaces[1]} | {testSpaces[2]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {testSpaces[3]} | {testSpaces[4]} | {testSpaces[5]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {testSpaces[6]} | {testSpaces[7]} | {testSpaces[8]} ");

        Console.WriteLine("\nhas a player won? : " + BoardStateAnalyzer.hasAPlayerWon(testSpaces));
    }
}

public static class BoardStateAnalyzer
{
    public static bool hasAPlayerWon(char[] spaces)
    {
        if (spaces[0] != ' ' && spaces[0] == spaces[1] && spaces[1] == spaces[2])
        {
            return true;
        }
        else if (spaces[3] != ' ' && spaces[3] == spaces[4] && spaces[4] == spaces[5])
        {
            return true;
        }
        else if (spaces[6] != ' ' && spaces[6] == spaces[7] && spaces[7] == spaces[8])
        {
            return true;
        }
        else if (spaces[0] != ' ' && spaces[0] == spaces[3] && spaces[3] == spaces[6])
        {
            return true;
        }
        else if (spaces[1] != ' ' && spaces[1] == spaces[4] && spaces[4] == spaces[7])
        {
            return true;
        }
        else if (spaces[2] != ' ' && spaces[2] == spaces[5] && spaces[5] == spaces[8])
        {
            return true;
        }
        else if (spaces[0] != ' ' && spaces[0] == spaces[4] && spaces[4] == spaces[8])
        {
            return true;
        }
        else if (spaces[6] != ' ' && spaces[6] == spaces[4] && spaces[4] == spaces[3])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class Player
{

    public PlayerNumberEnum PlayerNumber { get; private set; }
    public char Token { get; private set; }

    public Player(PlayerNumberEnum playerNumber)
    {
        this.PlayerNumber = playerNumber;
        if (playerNumber == PlayerNumberEnum.One)
        {
            Token = 'O';
        }
        else
        {
            Token = 'X';
        }
    }

    public (int space, char token) GetMove()
    {
        int spaceSelected = -1;

        do
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.NumPad7: spaceSelected = 0; break;
                case ConsoleKey.NumPad8: spaceSelected = 1; break;
                case ConsoleKey.NumPad9: spaceSelected = 2; break;
                case ConsoleKey.NumPad4: spaceSelected = 3; break;
                case ConsoleKey.NumPad5: spaceSelected = 4; break;
                case ConsoleKey.NumPad6: spaceSelected = 5; break;
                case ConsoleKey.NumPad1: spaceSelected = 6; break;
                case ConsoleKey.NumPad2: spaceSelected = 7; break;
                case ConsoleKey.NumPad3: spaceSelected = 8; break;
                default: break;
            }
        } while (spaceSelected == -1);

        return (spaceSelected, Token);
    }


    public enum PlayerNumberEnum { One, Two }
}