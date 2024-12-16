using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancyPong
{
	enum MainMenuOptions
	{
		None = 0,
		SinglePlayer = 9,
		PVP = 15,
		Highscore = 21
	}

	enum GameMode
	{
		None,
		SinglePlayer,
		PVP
	}

	enum PaddleEdge
	{
		None,
		UpperEdge,
		MiddleEdge,
		BottomEdge
	}

	enum GameStatus
	{
		None,
		Restart,
		End
	}
}
