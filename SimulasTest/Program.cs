ChestState currentChestState = ChestState.Locked;
Console.WriteLine(@"There is a chest in the room. You have a key in your hand.");
Console.WriteLine(@"Use 'Open', 'Close', 'Lock' and 'Unlock' to interact with it. Use 'Exit' to exit the room.");

while (true) {
    Console.WriteLine();
    Console.Write($"The chest is {currentChestState}. What do you want to do? ");
    string action = Console.ReadLine()
        .Trim()
        .ToLower();

    if (action == "open") {
        if (currentChestState == ChestState.Locked) {
            Console.WriteLine("The chest is locked. You need to unlock it.");
        } else if (currentChestState == ChestState.Closed) {
            Console.WriteLine("You open the chest and find nothing inside.");
            currentChestState = ChestState.Open;
        } else {
            Console.WriteLine("The chest is already open.");
        }
    } else if (action == "close") {
        if (currentChestState == ChestState.Open) {
            Console.WriteLine("You close the chest.");
            currentChestState = ChestState.Closed;
        } else {
            Console.WriteLine("The chest is already closed.");
        }
    } else if (action == "lock") {
        if (currentChestState == ChestState.Closed) {
            Console.WriteLine("You lock the chest.");
            currentChestState = ChestState.Locked;
        } else {
            Console.WriteLine("The chest is already locked.");
        }
    } else if (action == "unlock") {
        if (currentChestState == ChestState.Locked) {
            Console.WriteLine("You unlock the chest.");
            currentChestState = ChestState.Closed;
        } else {
            Console.WriteLine("The chest is already unlocked.");
        }
    } else if (action == "exit") {
        break;
    } else {
        Console.WriteLine("Invalid action. Please enter 'open', 'close', 'lock', 'unlock', or 'exit'.");
    }
}

enum ChestState {
    Open,
    Closed,
    Locked
}