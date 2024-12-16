namespace Pong
{
	internal class Program
	{
		//field
		const int FieldLegth = 50;
		const int FieldWidth = 15;
		const char FieldTile = '#';

		//rackets
		const int RacketLength = FieldWidth / 4;
		const char RacketTile = '|';

		//ball
		const char BallTile = 'O';

		static void Main(string[] args)
		{
			Console.CursorVisible = false;

			//field
			string line = string.Concat(Enumerable.Repeat(FieldTile, FieldLegth));

			//rackets
			int leftRacketHeight = 0;
			int RightRacketHeight = 0;

			//ball
			int ballX = FieldLegth / 2;
			int ballY = FieldWidth / 2;

			bool isBallGoingDown = true;
			bool isBallGoingRight = true;

			//scoreboard
			int leftPlayerPoints = 0;
			int rightPlayerPoints = 0;

			int scoreboardX = FieldLegth / 2 - 2;
			int scoreboardY = FieldWidth + 1;

			while (true)
			{
				//draw field
				Console.SetCursorPosition(0, 0);
				Console.WriteLine(line);

				Console.SetCursorPosition(0, FieldWidth);
				Console.WriteLine(line);

				//draw rackets
				for (int i = 0; i < RacketLength; i++)
				{
					Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
					Console.WriteLine(RacketTile);

					Console.SetCursorPosition(FieldLegth - 1, i + 1 + RightRacketHeight);
					Console.WriteLine(RacketTile);
				}

				while (!Console.KeyAvailable)
				{
					Console.SetCursorPosition(ballX, ballY);
					Console.WriteLine(BallTile);
					Thread.Sleep(100);  //adds a timer so the players have time to react

					Console.SetCursorPosition(ballX, ballY);
					Console.WriteLine(" "); //clears the previous position of the ball

					//update position of the ball
					if (isBallGoingDown)
					{
						ballY++;
					}
					else
					{
						ballY--;
					}

					if (isBallGoingRight)
					{
						ballX++;
					}
					else
					{
						ballX--;
					}

					if (ballY == 0 || ballY == FieldWidth - 1)
					{
						isBallGoingDown = !isBallGoingDown; //change direction
					}

					if (ballX == 1)
					{
						//left racket hits the ball and it bounces
						if (ballY >= leftRacketHeight + 1 && ballY <= leftRacketHeight + RacketLength)
						{
							isBallGoingRight = !isBallGoingRight;
						}
						else
						{
							rightPlayerPoints++;
							ballX = FieldLegth / 2;
							ballY = FieldWidth / 2;
							Console.SetCursorPosition(scoreboardX, scoreboardY);
							Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
							if (rightPlayerPoints == 10)
							{
								goto outer;
							}
						}
					}

					if (ballX == FieldLegth - 2)
					{
						//right racket hits the ball and it bounces
						if (ballY >= RightRacketHeight + 1 && ballY <= RightRacketHeight + RacketLength)
						{
							isBallGoingRight = !isBallGoingRight;
						}
						else
						{
							//ball goes out of the field; left player scores
							leftPlayerPoints++;
							ballX = FieldLegth / 2;
							ballY = FieldWidth / 2;
							Console.SetCursorPosition(scoreboardX, scoreboardY);
							Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
							if (leftPlayerPoints == 10)
							{
								goto outer;
							}
						}
					}
				}

				//check which key has been pressed
				switch (Console.ReadKey().Key)
				{
					case ConsoleKey.UpArrow:
						if (RightRacketHeight > 0)
						{
							RightRacketHeight--;
						}
						break;

					case ConsoleKey.DownArrow:
						if (RightRacketHeight < FieldWidth - RacketLength - 1)
						{
							RightRacketHeight++;
						}
						break;

					case ConsoleKey.W:
						if (leftRacketHeight > 0)
						{
							leftRacketHeight--;
						}
						break;

					case ConsoleKey.S:
						if (leftRacketHeight < FieldWidth - RacketLength - 1)
						{
							leftRacketHeight++;
						}
						break;
				}

				//clear the rackets' previous positions
				for (int i = 0; i < FieldWidth; i++)
				{
					Console.SetCursorPosition(0, i);
					Console.WriteLine(" ");

					Console.SetCursorPosition(FieldLegth - 1, i);
					Console.WriteLine(" ");
				}
			}

		outer:;
			Console.Clear();
			Console.SetCursorPosition(0, 0);

			if (rightPlayerPoints == 10)
			{
				Console.WriteLine("Right player won!");
			}
			else
			{
				Console.WriteLine("Left player won!");
			}

			Console.ReadLine();
		}
	}
}
