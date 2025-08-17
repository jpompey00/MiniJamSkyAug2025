using Godot;
using System;

public partial class BaseStage : Node2D
{


	int _ActionCount = 6;
	int _AmmoCount = 3;
	int _EnemyCount = -1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print("Running");
	}

	public void setStageCount(int actionCount, int ammoCount, int enemyCount)
	{
		_ActionCount = actionCount;
		_AmmoCount = ammoCount;
		_EnemyCount = enemyCount;

	}
	

	public void loseScenario()
	{
		GD.Print("Player loses");
	}


	public void winScenario()
	{
		GD.Print("Player wins");
		//continueArrow.Visible = true;
		//tileMap.SetCell(0, new Vector2I(4, 1), 1, new Vector2I(0, 0), 1);
		//add some animation
	}







}
