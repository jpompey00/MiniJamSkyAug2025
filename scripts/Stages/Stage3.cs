using Godot;
using System;

public partial class Stage3 : Node2D
{
	Node2D enemyList;
	Node2D continueArrow;
	TileMap tileMap;
	Boolean flag = true;

	Boolean topTileActive = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		enemyList = GetNode<Node2D>("EnemyList");
		GD.Print(enemyList.GetChildCount());

		continueArrow = GetNode<Node2D>("Continue Arrow");

		tileMap = GetNode<TileMap>("TileMap");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if (enemyList != null && enemyList.GetChildCount() > 0)
		// {
		// 	GD.Print(enemyList.GetChildCount());
		// }

		if (enemyList != null && enemyList.GetChildCount() <= 0)
		{

			if (flag)
			{
				winScenario();
				flag = false;
			}
		}

	}

	public void signalTest()
	{
		GD.Print("Signal Called");
		loseScenario();
	}


	public void loseScenario()
	{
		GD.Print("Player loses");
	}


	public void winScenario()
	{
		GD.Print("Player wins");
		continueArrow.Visible = true;
		tileMap.SetCell(0, new Vector2I(1, 1), 1, new Vector2I(0, 0), 1);
		//add some animation
	}

	public void buttonPressed(Vector2I coordinates)
	{
		GD.Print("Button Pressed");
		//checks coordinates
		//redraw 3,0 3,1
		// tileMap.SetCell(0, new Vector2I(3, 0), 1, new Vector2I(0, 0), -1);
		// tileMap.SetCell(0, new Vector2I(3, 1), 1, new Vector2I(0, 0), -1);
	}

	public void pressurePlatePressed(Vector2I coordinates)
	{
		GD.Print("Pressure Plated: " + coordinates);
		// if (topTileActive)
		// {
			tileMap.SetCell(0, new Vector2I(4, -2), 3, new Vector2I(0, 0), -1);
			tileMap.SetCell(0, new Vector2I(4, 1), 3, new Vector2I(0, 0), 0);
			// topTileActive = false;
		// }
		// else
		// {
		// 	tileMap.SetCell(0, new Vector2I(4, 1), 3, new Vector2I(0, 0), -1);
		// 	tileMap.SetCell(0, new Vector2I(4, -2), 3, new Vector2I(0, 0), 0);
		// 	topTileActive = true;
		// }


	}

	public void pressurePlateReleased(Vector2I coordinates)
	{
		GD.Print("Pressure Plate Released");
		tileMap.SetCell(0, new Vector2I(4, 1), 3, new Vector2I(0, 0), -1);
			tileMap.SetCell(0, new Vector2I(4, -2), 3, new Vector2I(0, 0), 0);
	}
}
