using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
	static class UIUtilities
	{
		private const int LeftSetCursor = 28;

		public static void PrintTitles()
		{
			PrintPongTitle();
			PrintPlayerOne();
			PrintPlayerTwo();
			PrintHighScore();
		}

		public static void PrintPongTitle()
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.SetCursorPosition(33, 0);
			Console.Write("  ___  ___  _  _  ___ ");
			Console.SetCursorPosition(33, 1);
			Console.Write(" | _ \\/ _ \\| \\| |/ __|");
			Console.SetCursorPosition(33, 2);
			Console.Write(" |  _/ (_) | .` | (_ |");
			Console.SetCursorPosition(33, 3);
			Console.WriteLine(" |_|  \\___/|_|\\_|\\___|");
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void PrintHighScoreTitle()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.SetCursorPosition(29, 0);
			Console.Write("  _  _ _      _                       ");
			Console.SetCursorPosition(29, 1);
			Console.Write(" | || (_)__ _| |_  ___ __ ___ _ _ ___ ");
			Console.SetCursorPosition(29, 2);
			Console.Write(" | __ | / _` | ' \\(_-</ _/ _ \\ '_/ -_)");
			Console.SetCursorPosition(29, 3);
			Console.Write(" |_||_|_\\__, |_||_/__/\\__\\___/_| \\___|");
			Console.SetCursorPosition(29, 4);
			Console.WriteLine("        |___/                         ");
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void PrintPressToStart()
		{
			Console.SetCursorPosition(0, 20);
			Console.WriteLine("Press anything to -start- and good luck!");
			Console.ReadLine();
		}

		public static void PrintWinner(string winner)
		{
			Console.SetCursorPosition(36, 15);
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine($"{winner} wins!");
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void ClearTitles()
		{
			for (int i = 0; i < 33; i++)
			{
				for (int j = 4; j < 29; j++)
				{
					Console.SetCursorPosition(i, j);
					Console.Write("\t\t\t\t\t\t   ");
				}
			}
		}

		public static void PrintPlayerInstructions(string player)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			PrintPlayerOneControls(player);
			Console.ForegroundColor = ConsoleColor.White;
			PrintRules();
			PrintPressToStart();
		}

		public static void PrintPlayerInstructions(string playerOne, string playerTwo)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			PrintPlayerOneControls(playerOne);
			PrintPlayerTwoControls(playerTwo);
			Console.ForegroundColor = ConsoleColor.White;
			PrintRules();
			PrintPressToStart();
		}

		private static string PrintPlayerOneControls(string playerOne)
		{
			Console.SetCursorPosition(3, 7);
			Console.WriteLine($"{playerOne}: ");
			Console.SetCursorPosition(6, 8);
            Console.WriteLine("Controls the paddle on the left side of the screen using your keyboard.");
			Console.SetCursorPosition(6, 9);
            Console.WriteLine("Use -UpArrow- to move the paddle up and -DownArrow- to move the paddle down.");

			return playerOne;
        }

		private static void PrintPlayerTwoControls(string playerTwo)
		{
			Console.SetCursorPosition(3, 11);
			Console.WriteLine($"{playerTwo}: ");
			Console.SetCursorPosition(6, 12);
			Console.WriteLine("Controls the paddle on the right side of the screen using your keyboard.");
			Console.SetCursorPosition(6, 13);
			Console.WriteLine("Use -W- to move the paddle up and -S- to move the paddle down.");
		}

		private static void PrintRules()
		{
			Console.SetCursorPosition(16, 15);
            Console.WriteLine("Points are scored when your opponent misses the ball.");
            Console.SetCursorPosition(17, 16);
            Console.WriteLine($"First player to reach {GameManager.GoalsToReach} points wins the game.");
        }

		private static void PrintPlayerOne()
		{
			Console.SetCursorPosition(LeftSetCursor, 7);
            Console.Write("  _   ___ _                   ");
            Console.SetCursorPosition(LeftSetCursor, 8);
            Console.Write(" / | | _ \\ |__ _ _  _ ___ _ _ ");
			Console.SetCursorPosition(LeftSetCursor, 9);
            Console.Write(" | | |  _/ / _` | || / -_) '_|");
			Console.SetCursorPosition(LeftSetCursor, 10);
            Console.Write(" |_| |_| |_\\__,_|\\_, \\___|_|  ");
			Console.SetCursorPosition(LeftSetCursor, 11);
            Console.WriteLine("                 |__/         ");
		}

		private static void PrintPlayerTwo()
		{
			Console.SetCursorPosition(LeftSetCursor, 13);
            Console.Write("  ___   ___ _                   ");
            Console.SetCursorPosition(LeftSetCursor, 14);
            Console.Write(" |_  ) | _ \\ |__ _ _  _ ___ _ _ ___");
			Console.SetCursorPosition(LeftSetCursor, 15);
            Console.Write("  / /  |  _/ / _` | || / -_) '_(_-<");
			Console.SetCursorPosition(LeftSetCursor, 16);
            Console.Write(" /___| |_| |_\\__,_|\\_, \\___|_| /__/");
			Console.SetCursorPosition(LeftSetCursor, 17);
            Console.WriteLine("                   |__/         ");
		}

		private static void PrintHighScore()
		{
			Console.SetCursorPosition(LeftSetCursor, 19);
			Console.Write("  _  _ _      _                       ");
			Console.SetCursorPosition(LeftSetCursor, 20);
			Console.Write(" | || (_)__ _| |_  ___ __ ___ _ _ ___ ");
			Console.SetCursorPosition(LeftSetCursor, 21);
			Console.Write(" | __ | / _` | ' \\(_-</ _/ _ \\ '_/ -_)");
			Console.SetCursorPosition(LeftSetCursor, 22);
			Console.Write(" |_||_|_\\__, |_||_/__/\\__\\___/_| \\___|");
			Console.SetCursorPosition(LeftSetCursor, 23);
			Console.WriteLine("        |___/                         ");
		}
	}
}
