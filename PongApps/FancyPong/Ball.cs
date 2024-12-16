using Pong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyPong
{
	class Ball
	{
		public Point PointRef => _point;

		private Point _point;
		private Point _velocity;
		private Board _board;

		public Ball(Board board)
		{
			_point = new Point(Board.HalfFieldWidth, Board.HalfFieldHeight);
			_velocity = new Point(-1, 0);
			_board = board;
			SetBallPosition();
		}

		public void SetBallPosition() => _board.GameField[_point.Y, _point.X] = CharacterUtilities.BallIcon;

		public void IncrementBallMovement() => _point += _velocity;

		public void SpawnBallRandomPosition()
		{
			_point.Y = GetRandomPosition() + Board.HalfFieldHeight;
			_point.X = GetRandomDirection() + Board.HalfFieldWidth;
			_velocity.Y = GetRandomDirection();

			if (GameManager.GameMode == GameMode.PVP) 
				_velocity.X *= -1;
			else 
				_velocity.X = -1;

			SetBallPosition();
		}

		private int GetRandomPosition()
		{
			Random rnd = new Random();

			return rnd.Next(0, 5);
		}

		private int GetRandomDirection()
		{
			Random rnd = new Random();

			return rnd.Next(-1, 2);
		}

		public void CheckCollision(char currentPixel, ref bool isFirstPlayerScored, ref bool isGoal, Point firstPlayer, Point secondPlayer)
		{
			if (currentPixel == CharacterUtilities.PlayerIcon)
				HandlePaddleCollion(firstPlayer, secondPlayer);

			else if (currentPixel == CharacterUtilities.TopBottomBorderIcon)
				_velocity.Y *= -1;

			else if (currentPixel == CharacterUtilities.LeftRightBorderIcon)
				HandleGoalScored(out isFirstPlayerScored, out isGoal);
		}

		private void HandlePaddleCollion(Point firstPlayer, Point secondPlayer)
		{
			PaddleEdge collideWithBall = PaddleEdge.None;
			WhichPaddleEdgeCollidedWithBall(ref collideWithBall, firstPlayer, secondPlayer);

			switch (collideWithBall)
			{
				case PaddleEdge.UpperEdge:
					_velocity.Y *= -1;
					break;
				case PaddleEdge.MiddleEdge:
					_velocity.Y *= 0;
					break;
				case PaddleEdge.BottomEdge:
					_velocity.Y *= 1;
					break;
			}

			_velocity.X *= -1;
		}

		private void WhichPaddleEdgeCollidedWithBall(ref PaddleEdge collideWithBall, Point firstPlayer, Point secondPlayer)
		{
			for (int i = 0; i < 5; i++)
			{
				Point first = new Point(firstPlayer.X, firstPlayer.Y + i);
				Point second = new Point(secondPlayer.X + i, secondPlayer.Y);

				if (FoundHitPart(first, second))
				{
					GetCollidedPaddleEdge(ref collideWithBall, i);
					break;
				}
			}
		}

		private bool FoundHitPart(Point a, Point b) => a == _point || b == _point;

		private void GetCollidedPaddleEdge(ref PaddleEdge collideWithBall, int i)
		{
			if (i == 0 || i == 1)
				collideWithBall = PaddleEdge.UpperEdge;
			else if (i == 2)
				collideWithBall = PaddleEdge.MiddleEdge;
			else
				collideWithBall = PaddleEdge.BottomEdge;
		}

		private void HandleGoalScored(out bool isFirstPlayerScored, out bool isGoal)
		{
			if (_point.X >= 89)
				isFirstPlayerScored = true;
			else
				isFirstPlayerScored = false;

			isGoal = true;
		}
	}
}
