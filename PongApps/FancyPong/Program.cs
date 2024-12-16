using Pong;

namespace FancyPong
{
	//sauce: https://github.com/RoeiRubach/Classic-Pong-game-.NET

	class Program
	{
		static void Main(string[] args)
		{
			GameManager game;

			do
			{
				game = new GameManager();
				game.Start();
			} while (!game.IsGameRestart());
		}
	}
}
