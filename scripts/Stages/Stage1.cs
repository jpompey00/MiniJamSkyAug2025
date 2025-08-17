using Godot;
using System;

public partial class Stage1 : BaseStage
{
	Node2D enemyList;
	Node2D continueArrow;
	TileMap tileMap;
	Boolean flag = true;

	int actionCount = 6;
	int ammoCount = 3;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		enemyList = GetNode<Node2D>("EnemyList");
		GD.Print(enemyList.GetChildCount());

		continueArrow = GetNode<Node2D>("Continue Arrow");

		tileMap = GetNode<TileMap>("TileMap");
		base.setStageCount(actionCount,ammoCount,enemyList.GetChildCount());
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


	


	public void winScenario()
	{
		base.winScenario();
		GD.Print("Player wins");
		continueArrow.Visible = true;
		tileMap.SetCell(0, new Vector2I(4, 1), 1, new Vector2I(0, 0), 1);
		//add some animation
	}
}
