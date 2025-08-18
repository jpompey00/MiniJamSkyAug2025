using Godot;
using System;

public partial class FallAnimation : Node2D
{
	AnimationPlayer animationPlayer;
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("FallingTransition");

		await ToSignal(GetTree().CreateTimer(2.5f), SceneTreeTimer.SignalName.Timeout);
		GetTree().ChangeSceneToFile(Constants.LEVEL_1_PATH);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	


}
