using Godot;
using System;

public partial class VictoryScreen : Node2D
{
	Boolean canInteract = false;
	AnimationPlayer animationPlayer;
	// Called when the node enters the scene tree for the first time.
	AudioStreamPlayer2D audioStreamPlayer2D;
	public override async void _Ready()
	{
		audioStreamPlayer2D = GetTree().Root.GetNode<AudioStreamPlayer2D>("MusicPlayer");
		GD.Print(audioStreamPlayer2D);
		audioStreamPlayer2D.Stream = GD.Load<AudioStream>(Constants.VICTORY);
		audioStreamPlayer2D.Play();


		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("Victory");
		await animationPlayer.ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
		canInteract = true;
	}

	public override async void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed && canInteract)
		{

			GetTree().Quit();
		}
	}
}
