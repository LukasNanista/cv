using Pong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyPong
{
	class Board
	{
		public const int FieldHeight = 24;
		public const int FieldWidth = 90;
		public const int HalfFieldHeight = FieldHeight / 2;
		public const int HalfFieldWidth = FieldWidth / 2;
		public const int FirstPlayerXPosition = 2;
		public const int SecondPlayerXPosition = FieldWidth - 3;

		public char[,] GameField;

		public Board()
		{
			GameField = new char[FieldHeight, FieldWidth];
			GameBorder gameBorder = new GameBorder(GameField);
		}

		public void PrintGameField()
		{
			UIUtilities.PrintPongTitle();

			for (int i = 0; i < GameField.GetLength(0); i++)
			{
				for (int j = 0; j < GameField.GetLength(1); j++)
				{
					Console.Write(GameField[i, j]);
				}

				Console.WriteLine();
			}
		}

		public void SetEmptyPixelAtPoint(Point point) => GameField[point.Y, point.X] = CharacterUtilities.EmptyPixel;

		public void ClearTopPaddleAfterStep(Point point) => GameField[point.Y - 1, point.X] = CharacterUtilities.EmptyPixel;

		public void ClearBottomPaddleAfterStep(Point point) => GameField[point.Y + 5, point.X] = CharacterUtilities.EmptyPixel;

		public bool IsPaddleReachBottomBorder(Point point)
			=> GameField[point.Y + 5, point.X] == CharacterUtilities.TopBottomBorderIcon;

		public bool IsPaddleReachTopBorder(Point point)
			=> GameField[point.Y - 1, point.X] == CharacterUtilities.TopBottomBorderIcon;
	}
}
