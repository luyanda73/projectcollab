using System;
using System.Threading;

class TicTacToe
{
    static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static int playerTurn = 1; // 1 for X, 2 for O
    static int flag = 0; // 0: playing, 1: win, -1: draw

    static void Main(string[] args)
    {
        // Set console properties for a "Big" feel
        Console.Title = "PRO TIC-TAC-TOE v2.0";
        bool playAgain = true;

        while (playAgain)
        {
            ResetGame();
            RunGameLoop();

            Console.SetCursorPosition(10, 22);
            Console.Write("WANT A REMATCH? (Y/N): ");
            string response = Console.ReadLine().ToUpper();
            playAgain = (response == "Y");
        }

        Console.Clear();
        Console.WriteLine("\n\n\tTHANKS FOR PLAYING!");
        Thread.Sleep(1000);
    }

    static void RunGameLoop()
    {
        do
        {
            DrawHeader();
            PrintBoard();

            Console.SetCursorPosition(10, 18);
            Console.ForegroundColor = (playerTurn == 1) ? ConsoleColor.Cyan : ConsoleColor.Magenta;
            Console.Write($"PLAYER {playerTurn} ({(playerTurn == 1 ? "X" : "O")}), SELECT A SLOT: ");
            Console.ResetColor();

            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 9 && board[choice - 1] != 'X' && board[choice - 1] != 'O')
            {
                board[choice - 1] = (playerTurn == 1) ? 'X' : 'O';
                flag = CheckWin();

                if (flag == 0)
                {
                    playerTurn = (playerTurn == 1) ? 2 : 1;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(10, 19);
                Console.WriteLine("❌ INVALID MOVE! TRY AGAIN.");
                Console.ResetColor();
                Thread.Sleep(800);
            }

        } while (flag == 0);

        if (flag == 1) ShowWinScreen(playerTurn);
        else ShowDrawScreen();
    }

    static void DrawHeader()
    {
        Console.Clear();
        Console.WriteLine("\n\t╔══════════════════════════════════════════╗");
        Console.WriteLine("\t║         ULTIMATE TIC-TAC-TOE             ║");
        Console.WriteLine("\t╚══════════════════════════════════════════╝\n");
    }

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

    static int CheckWin()
    {
        int[,] winners = {
            {0,1,2}, {3,4,5}, {6,7,8}, // Rows
            {0,3,6}, {1,4,7}, {2,5,8}, // Cols
            {0,4,8}, {2,4,6}           // Diagonals
        };

        for (int i = 0; i < 8; i++)
        {
            if (board[winners[i, 0]] == board[winners[i, 1]] && board[winners[i, 1]] == board[winners[i, 2]])
                return 1;
        }

        foreach (char c in board) if (c != 'X' && c != 'O') return 0;
        return -1;
    }

    static void ShowWinScreen(int winner)
    {
        DrawHeader();
        PrintBoard();
        Console.WriteLine("\n");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\t╔══════════════════════════════════════════╗");
        Console.WriteLine("\t║                                          ║");
        Console.WriteLine($"\t║           🏆  PLAYER {winner} IS THE VICTOR!     ║");
        Console.WriteLine("\t║                                          ║");
        Console.WriteLine("\t╚══════════════════════════════════════════╝");
        Console.ResetColor();
    }

    static void ShowDrawScreen()
    {
        DrawHeader();
        PrintBoard();
        Console.WriteLine("\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\t╔══════════════════════════════════════════╗");
        Console.WriteLine("\t║                                          ║");
        Console.WriteLine("\t║             🤝  STALEMATE!               ║");
        Console.WriteLine("\t║                                          ║");
        Console.WriteLine("\t╚══════════════════════════════════════════╝");
        Console.ResetColor();
    }

    static void ResetGame()
    {
        board = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        playerTurn = 1;
        flag = 0;
    }
}
