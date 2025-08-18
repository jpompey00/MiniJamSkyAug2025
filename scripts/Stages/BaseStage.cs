using Godot;
using System;
using System.Threading.Tasks;

public partial class BaseStage : Node2D
{


	int _ActionCount = 6;
	int currentActionCount;
	int _AmmoCount = 3;
	int currentAmmo = -1;
	int _EnemyCount = -1;

	int levelCount;

	Player player;
	UI UI;
	SceneTransitionAnimation sceneTransitionAnimation;
	SceneTransition sceneTransition;


	public override void _Ready()
	{
		// GD.Print("running");
		player = GetNode<Player>("Player");
		UI = GetNode("Camera2D").GetNode<UI>("UI");
		sceneTransitionAnimation = GetNode<SceneTransitionAnimation>("SceneTransitionAnimation");
		sceneTransition = GetNode<SceneTransition>("Scene Transition");
		sceneTransitionAnimation.PlayFadeIn();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// GD.Print("Running");
	}

	public void setStageCount(int actionCount, int ammoCount, int enemyCount)
	{
		_ActionCount = actionCount;
		// currentActionCount = _ActionCount;
		_AmmoCount = ammoCount;
		currentAmmo = _AmmoCount;
		_EnemyCount = enemyCount;
		player.setActionAndAmmo(actionCount, ammoCount);
		UI.setMoveAmount(actionCount);
		UI.setAmmoCount(ammoCount);
	}


	public async void loseScenario()
	{
		// GD.Print("Player loses");
		// GD.Print(currentAmmo);
		if (currentAmmo < 1)
		{
			await ToSignal(GetTree().CreateTimer(.5f), SceneTreeTimer.SignalName.Timeout);
			sceneTransitionAnimation.PlayFadeOut();
			await ToSignal(GetTree().CreateTimer(2.0f), SceneTreeTimer.SignalName.Timeout);

			GetTree().ChangeSceneToFile("res://Scenes/LoseScreen.tscn");
			//will need to switch to another scene
		}

	}


	public void winFunction()
	{
		GD.Print("Player wins");
		player.addMovement();
		
		//will need to switch to another scene
		//continueArrow.Visible = true;
		//tileMap.SetCell(0, new Vector2I(4, 1), 1, new Vector2I(0, 0), 1);
		//add some animation
	}

	public void setNextLevel(String path)
	{
		sceneTransition.setNextLevel(path);
	}
	public void projectileExpended()
	{
		currentAmmo -= 1;
	}

	public int getCurrentAmmo()
	{
		return currentAmmo;
	}

	public int getCurrentActions()
	{
		return currentActionCount;
	}


public override async void _Input(InputEvent @event)
{
    if (@event is InputEventKey keyEvent && keyEvent.Pressed)
    {
			// Check if the key pressed was 'R'
			if (keyEvent.Keycode == Key.R)
			{
				GetTree().ReloadCurrentScene();
        }
    }
}

}
