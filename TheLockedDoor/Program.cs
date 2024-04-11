Console.WriteLine("Welcome to the Door Simulator!");
int initialPasscode;
do {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"Input a initial passcode for the door (0000 -> 9999): ");
    Console.ForegroundColor = ConsoleColor.Red;
} while (!int.TryParse(Console.ReadLine(), out initialPasscode) || initialPasscode < 0000 || initialPasscode > 9999);
Console.ResetColor();

Door door = new Door(initialPasscode);

bool running = true;
while(running)
{   
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("\nCurrent Door State: ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(door.CurrentState);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Available commands: ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("open, close, lock, unlock, changepasscode, exit");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter a command: ");
    Console.ForegroundColor = ConsoleColor.Red;
    string command = Console.ReadLine()?.Trim().ToLower() ?? String.Empty;
    Console.ResetColor();

    switch (command) {
        case "open":
            door.Open();
            break;
        case "close":
            door.Close();
            break;
        case "lock":
            door.Lock();
            break;
        case "unlock":
            door.Unlock();
            break;
        case "changepasscode":
            door.ChangePasscode();
            break;
        case "exit":
            running = false;
            Console.WriteLine("Exiting the Door Simulator.");
            break;
        default:
            Console.WriteLine("Invalid command. Please try again.");
            break;
    }

}


public class Door 
{
    public State CurrentState { get; private set; } = State.Locked;
    private int _passcode;
    private const int MIN_PASSCODE = 0;
    private const int MAX_PASSCODE = 9999;

    public Door(int passcode)
    {
        if (passcode < MIN_PASSCODE || passcode > MAX_PASSCODE) {
            throw new ArgumentOutOfRangeException(nameof(passcode), $"Passcode must be between {MIN_PASSCODE} and {MAX_PASSCODE}.");
        }

        _passcode = passcode;
    }

    public void Open() 
    {
        switch (CurrentState)
        {
            case State.Open:
                Console.WriteLine("Failure: The door is already open.");
                break;
            case State.Locked:
                Console.WriteLine("Failure: The door is locked.");
                break;
            case State.Closed:
                CurrentState = State.Open;
                Console.WriteLine("Success: The door is now open.");
                break;
        }
    }

    public void Close()
    {
        switch (CurrentState)
        {
            case State.Open:
                CurrentState = State.Closed;
                Console.WriteLine("Success: The door is now closed.");
                break;
            case State.Locked:
                goto case State.Closed;
            case State.Closed:
                Console.WriteLine("Failure: The door is already closed.");
                break;
        }
    }

    public void Lock()
    {
        switch (CurrentState)
        {
            case State.Open:
                Console.WriteLine("Failure: The door is open. Close it to lock.");
                break;
            case State.Locked:
                Console.WriteLine("Failure: The door is already locked.");
                break;
            case State.Closed:
                CurrentState = State.Locked;
                Console.WriteLine("Success: The door is now locked.");
                break;
        }
    }

    public void Unlock()
    {
        switch (CurrentState)
        {
            case State.Open:
                Console.WriteLine("Failure: The door is open. Close it to lock.");
                break;
            case State.Locked:
                int passcodeInput = GetPasscodeFromUser();

                if(_passcode != passcodeInput)
                {
                    Console.WriteLine("Failure: That passcode is incorrect!");
                    break;
                }

                CurrentState = State.Closed;
                Console.WriteLine("Success: The door is now unlocked.");
                break;
            case State.Closed:
                Console.WriteLine("Failure: The door is already unlocked.");
                break;
        }
    }

    public void ChangePasscode()
    {
        int passcodeInput = GetPasscodeFromUser();
        if (_passcode == passcodeInput)
        {
            _passcode = GetPasscodeFromUser(true);
            Console.WriteLine("Success: Passcode changed.");
        } else {
            Console.WriteLine("Failure: That passcode is incorrect!");
        }
    }

    private int GetPasscodeFromUser(bool isNew = false) 
    {
        int returnVal;

        do {
            Console.ForegroundColor = ConsoleColor.Green;
            if (isNew)
            {
                Console.Write($"Input a new passcode for the door ({MIN_PASSCODE} -> {MAX_PASSCODE}): ");
            } else {
                Console.Write($"Input a passcode for the locked door ({MIN_PASSCODE} -> {MAX_PASSCODE}): ");
            }            
            Console.ForegroundColor = ConsoleColor.Red;
        } while (!int.TryParse(Console.ReadLine(), out returnVal) || returnVal < MIN_PASSCODE || returnVal > MAX_PASSCODE);

        Console.ResetColor();
        return returnVal;
    }

    public enum State { Open, Closed, Locked }
}


