using Pong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyPong
{
	class GameBorder
	{
		public GameBorder(char[,] gameField) => SetBorder(gameField);

		private void SetBorder(char[,] gameField)
		{
			for (int i = 0; i < gameField.GetLength(0); i++)
			{
				for (int j = 0; j < gameField.GetLength(1); j++)
				{
					if (i == 0)
						HandleUpperBorder(i, j, gameField);

					else if (i == gameField.GetLength(0) - 1)
						HandleLowerBorder(i, j, gameField);

					else
					{
						gameField[i, j] = CharacterUtilities.EmptyPixel;
						HandleLeftRightBorders(i, j, gameField);
					}
				}
			}
		}

		private void HandleLeftRightBorders(int i, int j, char[,] gameField)
		{
			if (j == 0 || j == gameField.GetLength(1) - 1)
				gameField[i, j] = CharacterUtilities.LeftRightBorderIcon;
		}

		private void HandleLowerBorder(int i, int j, char[,] gameField)
		{
			gameField[i, j] = CharacterUtilities.TopBottomBorderIcon;

			if (j == 0) gameField[i, j] = CharacterUtilities.LeftBottomBorderIcon;

			else if (j == gameField.GetLength(1) - 1)
				gameField[i, j] = CharacterUtilities.RightBottomBorderIcon;
		}

		private void HandleUpperBorder(int i, int j, char[,] gameField)
		{
			gameField[i, j] = CharacterUtilities.TopBottomBorderIcon;

			if (j == 0) gameField[i, j] = CharacterUtilities.LeftUpperBorderIcon;

			else if (j == gameField.GetLength(1) - 1) gameField[i, j] = CharacterUtilities.RightUpperBorderIcon;
		}
	}
}