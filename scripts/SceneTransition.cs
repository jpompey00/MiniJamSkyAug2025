using Godot;
using System;

public partial class SceneTransition : Area2D
{
	String nextLevelPath;
	SceneTransitionAnimation sceneTransitionAnimation;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneTransitionAnimation = GetNode<SceneTransitionAnimation>("../SceneTransitionAnimation");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public async void OnAreaEntered(Area2D area)
	{
		if (area.GetParent().Name == "Player")
		{
					GD.Print("Transitions to next level");
		sceneTransitionAnimation.PlayFadeOut();
		await ToSignal(GetTree().CreateTimer(2.0f), SceneTreeTimer.SignalName.Timeout);
		GetTree().ChangeSceneToFile(nextLevelPath);
		}

	}

	public void setNextLevel(String path)
	{
		nextLevelPath = path;
	}

}
