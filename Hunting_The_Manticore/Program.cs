// See https://aka.ms/new-console-template for more information

const int PLAYER_ONE_MAX_HEALTH = 10;
const int PLAYER_TWO_MAX_HEALTH = 15;
int playerOneHealth;
int playerTwoHealth;
int round;
int distance;
int distanceGuess;
int damageThisRound;

while (true) {
    InitializeGame();

    // Player 1 : Manticore Distance
    Console.ForegroundColor = ConsoleColor.White;
    PlayerOneTurn();

    // Player 2 : Guess Distance
    Console.WriteLine("Player 2, it is your turn.");
    Console.WriteLine("-----------------------------------------------------------");
    while (playerOneHealth > 0 && playerTwoHealth > 0) {

        PlayerTwoTurn();

        if (playerOneHealth <= 0) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nThe Manticore has been destroyed!\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-----------------------------------------------------------");
            break;
        }
        playerTwoHealth--;
        if (playerTwoHealth <= 0) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nThe city has been destroyed! The city of Consolas has been saved!\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-----------------------------------------------------------");
            break;
        }
        round++;
        Console.WriteLine("-----------------------------------------------------------");
    }

    Console.WriteLine("Press any key to continue. . .");
    Console.ReadKey();
    Console.Clear();

    //would you like to play again?
    Console.WriteLine("Would you like to play again?");
    Console.WriteLine("1. Yes");
    Console.WriteLine("2. No");
    Console.Write("Enter 1 or 2: ");
    string input = Console.ReadLine();
    while (input != "1" && input != "2") {
        Console.WriteLine("Invalid input. Please enter 1 or 2.");
        input = Console.ReadLine();
    }
    if (input == "2") {
        break;
    }
    Console.Clear();
}


void InitializeGame() {
    playerOneHealth = PLAYER_ONE_MAX_HEALTH;
    playerTwoHealth = PLAYER_TWO_MAX_HEALTH;
    round = 1;
    distance = -1;
    distanceGuess = -1;
    damageThisRound = 0;
}

void PlayerOneTurn() {
    Console.WriteLine("Would you like the manticore to be positioned by a CPU or Human?");
    Console.WriteLine("1. CPU");
    Console.WriteLine("2. Human");
    Console.Write("Enter 1 or 2: ");
    string input = Console.ReadLine();
    while (input != "1" && input != "2") {
        Console.WriteLine("Invalid input. Please enter 1 or 2.");
        input = Console.ReadLine();
    }
    if (input == "1") {
        //get random number between 0-100
        Random random = new Random();
        distance = random.Next(0, 101);
    }
    else if (input == "2") {
        Console.Write("Player 1, how far away from the city do you want to station the Manticore? ");
        distance = GetDistance();
    }
    Console.Clear();
}

void PlayerTwoTurn() {
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"STATUS: Round: {round} City: {playerTwoHealth}/{PLAYER_TWO_MAX_HEALTH} Manticore: {playerOneHealth}/{PLAYER_ONE_MAX_HEALTH}");
    Console.WriteLine($"The cannon is expected to deal {GetCannonDamageThisRound()} damage this round.");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("Enter desired cannon range: ");
    distanceGuess = GetDistance();

    Console.Write("That round ");
    Console.Write(GetCannonRange());
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(" the target.");
}

int GetDistance() {
    //decide distance or guess for compare
    int _distance = -1;

    // Verify player 1 input is a number between 0-100 and not null, then return int
    while (_distance < 0 || _distance > 100) {
        Console.Write("Enter the distance (0-100): ");
        string input = Console.ReadLine();
        try {
            _distance = Convert.ToInt32(input);
            if (_distance < 1 || _distance > 100) {
                Console.WriteLine("Please enter a number between 0 and 100.");
            }
        }
        catch (Exception) {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
    return _distance;
}

int GetCannonDamageThisRound() {
    // return the damage the cannon will do this round
    if (round % 3 == 0 && round % 5 == 0) {
        damageThisRound = 10;
        return 10;
    };
    if (round % 3 == 0 || round % 5 == 0) {
        damageThisRound = 3;
        return 3;
    }
    damageThisRound = 1;
    return 1;
}

string GetCannonRange() {
    if (distance > distanceGuess) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        return "FELL SHORT";
    }
    else if (distance < distanceGuess) {
        Console.ForegroundColor = ConsoleColor.Red;
        return "OVERSHOT";
    }
    else {
        ApplyDamageToManticore();
        Console.ForegroundColor = ConsoleColor.Green;
        return "DIRECT HIT!";
    }
}

void ApplyDamageToManticore() {
    playerOneHealth -= damageThisRound;
}