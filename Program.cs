using System;
using System.Threading;

// Main class for the TicTacToe game
class TicTacToe
{
    // Array representing the game board (positions 1–9)
    static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    // Keeps track of whose turn it is (1 = Player1, 2 = Player2)
    static int playerTurn = 1;

    // Game status flag (0 = playing, 1 = win, -1 = draw)
    static int flag = 0;

    // Variables to store player names
    static string player1Name = "";
    static string player2Name = "";

    // Main method where program execution starts
    static void Main(string[] args)
    {
        // Enable emoji and special characters in console
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Set console window title
        Console.Title = "✨ Kawaii Tic-Tac-Toe ✨";

        // Show the welcome animation screen
        ShowWelcomeScreen();

        // Boolean to keep menu running
        bool running = true;

        // Main menu loop
        while (running)
        {
            // Display menu and get user choice
            string choice = ShowMenu();

            if (choice == "1")
            {
                // Ask for player names
                GetPlayerNames();

                // Start the game
                PlayGame();
            }
            else if (choice == "2")
            {
                // Exit the program
                running = false;
            }
        }

        // Goodbye message
        Console.Clear();
        Console.WriteLine("\n\n\t ✨ See you next time! ✨");

        // Pause before closing
        Thread.Sleep(1000);
    }

    // Displays welcome screen with decorative text
    static void ShowWelcomeScreen()
    {
        Console.Clear();

        // Set text color
        Console.ForegroundColor = ConsoleColor.Magenta;

        Console.WriteLine("\n\n\n");
        Console.WriteLine("\t\t ✨ ✨ ✨ ✨ ✨ ✨ ✨ ✨");
        Console.WriteLine("\t\t ✨ ULTIMATE X & O ✨");
        Console.WriteLine("\t\t ✨ ✨ ✨ ✨ ✨ ✨ ✨ ✨");

        // Reset console color
        Console.ResetColor();

        // Small delay for animation effect
        Thread.Sleep(1200);
    }

    // Displays main menu
    static string ShowMenu()
    {
        Console.Clear();

        // Draw the game header
        DrawHeader();

        // Menu options
        Console.WriteLine("\t\t [ 1 ] START GAME ");
        Console.WriteLine("\t\t [ 2 ] EXIT ");
        Console.WriteLine("\t\t ─────────────────────");

        // Ask user to select option
        Console.Write("\n\t\t Pick an option: ");

        // Return user input
        return Console.ReadLine();
    }

    // Method to get player names
    static void GetPlayerNames()
    {
        Console.Clear();

        // Draw game header
        DrawHeader();

        // Player 1 name input
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("\n\t 🌸 Player 1 (X), name: ");
        player1Name = Console.ReadLine();

        // If empty name, assign default
        if (string.IsNullOrWhiteSpace(player1Name)) player1Name = "Player 1";

        // Player 2 name input
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("\n\t 🌸 Player 2 (O), name: ");
        player2Name = Console.ReadLine();

        // If empty name, assign default
        if (string.IsNullOrWhiteSpace(player2Name)) player2Name = "Player 2";

        Console.ResetColor();

        // Small loading message
        Console.WriteLine("\n\t Setting up the fun...");
        Thread.Sleep(800);
    }

    // Handles rematch functionality
    static void PlayGame()
    {
        bool playAgain = true;

        // Loop for multiple matches
        while (playAgain)
        {
            // Reset board and variables
            ResetGame();

            // Run the main game loop
            RunGameLoop();

            // Ask players if they want to play again
            Console.SetCursorPosition(10, 20);
            Console.Write(" REMATCH? (Y/N): ");

            string response = Console.ReadLine().ToUpper();

            // If user enters Y, game restarts
            playAgain = (response == "Y");
        }
    }

    // Core game loop
    static void RunGameLoop()
    {
        do
        {
            // Redraw header and board
            DrawHeader();
            PrintBoard();

            // Determine current player name
            string currentPlayer = (playerTurn == 1) ? player1Name : player2Name;

            // Determine current symbol
            char currentSymbol = (playerTurn == 1) ? 'X' : 'O';

            // Ask player to choose slot
            Console.SetCursorPosition(10, 16);
            Console.ForegroundColor = (playerTurn == 1) ? ConsoleColor.Cyan : ConsoleColor.Magenta;

            Console.Write($" 👉 {currentPlayer.ToUpper()} ({currentSymbol}), PICK A SLOT: ");
            Console.ResetColor();

            // Read input
            string input = Console.ReadLine();

            // Validate move
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 9 && board[choice - 1] != 'X' && board[choice - 1] != 'O')
            {
                // Place symbol on board
                board[choice - 1] = currentSymbol;

                // Check if someone won
                flag = CheckWin();

                // Switch turn if game still running
                if (flag == 0)
                {
                    playerTurn = (playerTurn == 1) ? 2 : 1;
                }
            }
            else
            {
                // Invalid input message
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(10, 17);
                Console.WriteLine(" ❌ Oops! Try a different number.");
                Console.ResetColor();

                Thread.Sleep(800);
            }

        } while (flag == 0); // Continue until win or draw

        // Display result
        if (flag == 1) ShowWinScreen();
        else ShowDrawScreen();
    }

    // Draws decorative header for the game
    static void DrawHeader()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine("\n\t ✨───────────────────────────────────✨");
        Console.WriteLine("\t ✨ ULTIMATE TIC-TAC-TOE ");
        Console.WriteLine("\t ✨───────────────────────────────────✨\n");

        Console.ResetColor();
    }

    // Displays the Tic-Tac-Toe board
    static void PrintBoard()
    {
        // Board logic with "Big" spacing
        string[] display = new string[9];
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 'X') display[i] = "\x1b[96mX\x1b[0m"; // Cyan X
            else if (board[i] == 'O') display[i] = "\x1b[95mO\x1b[0m"; // Magenta O
            else display[i] = "\x1b[90m" + board[i] + "\x1b[0m"; // Gray number
        }

        Console.WriteLine("\t\t      ║       ║      ");
        Console.WriteLine($"\t\t  {display[0]}   ║   {display[1]}   ║   {display[2]}  ");
        Console.WriteLine("\t\t══════╬═══════╬══════");
        Console.WriteLine("\t\t      ║       ║      ");
        Console.WriteLine($"\t\t  {display[3]}   ║   {display[4]}   ║   {display[5]}  ");
        Console.WriteLine("\t\t══════╬═══════╬══════");
        Console.WriteLine("\t\t      ║       ║      ");
        Console.WriteLine($"\t\t  {display[6]}   ║   {display[7]}   ║   {display[8]}  ");
        Console.WriteLine("\t\t      ║       ║      ");
    }

    // Checks all possible winning combinations
    static int CheckWin()
    {
        // All winning positions
        int[,] winners = {
{ 0, 1, 2 },
{ 3, 4, 5 },
{ 6, 7, 8 },
{ 0, 3, 6 },
{ 1, 4, 7 },
{ 2, 5, 8 },
{ 0, 4, 8 },
{ 2, 4, 6 }
};

        // Check each combination
        for (int i = 0; i < 8; i++)
        {
            if (board[winners[i, 0]] == board[winners[i, 1]] && board[winners[i, 1]] == board[winners[i, 2]])
                return 1; // Win
        }

        // Check if board still has empty spaces
        foreach (char c in board)
            if (c != 'X' && c != 'O') return 0; // Game still running

        // If board full and no winner
        return -1; // Draw
    }

    // Displays winning screen
    static void ShowWinScreen()
    {
        DrawHeader();
        PrintBoard();

        // Determine winner name
        string winnerName = (playerTurn == 1) ? player1Name : player2Name;

        Console.WriteLine("\n");

        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("\t 💖 ✨ 💖 ✨ 💖 ✨ 💖 ✨ 💖 ✨ 💖");
        Console.WriteLine($"\t YAAY! {winnerName.ToUpper()} WINS! ");
        Console.WriteLine("\t 💖 ✨ 💖 ✨ 💖 ✨ 💖 ✨ 💖 ✨ 💖");

        Console.ResetColor();
    }

    // Displays draw screen
    static void ShowDrawScreen()
    {
        DrawHeader();
        PrintBoard();

        Console.WriteLine("\n");

        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine("\t ☁️ ☁️ ☁️ ☁️ ☁️ ☁️ ☁️ ☁️ ☁️");
        Console.WriteLine("\t IT'S A DRAW! ");
        Console.WriteLine("\t ☁️ ☁️ ☁️ ☁️ ☁️ ☁️ ☁️ ☁️ ☁️");

        Console.ResetColor();
    }

    // Resets game variables and board for new match
    static void ResetGame()
    {
        board = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        playerTurn = 1;

        flag = 0;
    }
}
