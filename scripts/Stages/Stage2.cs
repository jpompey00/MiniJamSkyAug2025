using Godot;
using System;

public partial class Stage2 : BaseStage
{
	Node2D enemyList;
	Node2D continueArrow;
	TileMap tileMap;
	Boolean flag = true;
	int actionCount = 6;
	Boolean hasLostFlag = false;
	int ammoCount = 4;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		enemyList = GetNode<Node2D>("EnemyList");
		// GD.Print(enemyList.GetChildCount());

		continueArrow = GetNode<Node2D>("Continue Arrow");

		tileMap = GetNode<TileMap>("TileMap");
		base.setStageCount(actionCount, ammoCount, enemyList.GetChildCount());
		base.setNextLevel(Constants.LEVEL_3_PATH);

		
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

		if (getCurrentAmmo() == 0 && !hasLostFlag && enemyList.GetChildCount() > 0 && !flag)
		{
			GD.Print("Has lost");
			hasLostFlag = true;
			loseScenario();
		}
	}


	public void winScenario()
	{
		base.winFunction();
		GD.Print("Player wins");
		continueArrow.Visible = true;
		tileMap.SetCell(0, new Vector2I(-4, 1), 1, new Vector2I(0, 0), 1);
		//add some animation
	}

	public void buttonPressed(Vector2I coordinates)
	{
		GD.Print("Button Pressed");
		//checks coordinates
		//redraw 3,0 3,1
		tileMap.SetCell(0, new Vector2I(3, 0), 1, new Vector2I(0, 0), -1);
		tileMap.SetCell(0, new Vector2I(3, 1), 1, new Vector2I(0, 0), -1);
	}

	// public void pressurePlatePressed(Vector2I coordinates)
	// {
	// 	GD.Print("Pressure Plated");
	// }
}
