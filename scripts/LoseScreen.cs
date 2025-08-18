using Godot;
using System;

public partial class LoseScreen : Node2D
{
	AnimationPlayer towerAnimation;
	AnimationPlayer loseMessage;
	AnimationPlayer restartAnimation;
	SceneTransitionAnimation sceneTransitionAnimation;
	AudioStreamPlayer2D audioStreamPlayer2D;
	// Called when the node enters the scene tree for the first time.
	public async override void _Ready()
	{
		audioStreamPlayer2D = GetTree().Root.GetNode<AudioStreamPlayer2D>("MusicPlayer");
		audioStreamPlayer2D.Stream = GD.Load<AudioStream>(Constants.TITLE_SCREEN_MUSIC);
		audioStreamPlayer2D.Play();	

		towerAnimation = GetNode("Tower").GetChild<AnimationPlayer>(0);
		loseMessage = GetNode("LoseMessage").GetChild<AnimationPlayer>(0);
		restartAnimation = GetNode("RestartMessage").GetChild<AnimationPlayer>(0);
		sceneTransitionAnimation = GetNode<SceneTransitionAnimation>("SceneTransitionAnimation");

		towerAnimation.Play("Lose Screen Tower");
		sceneTransitionAnimation.PlayFadeIn();
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);


		loseMessage.Play("Lose Message Animation");
		restartAnimation.Play("Restart Message Animation");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override async void _Input(InputEvent @event)
{
    if (@event is InputEventKey keyEvent && keyEvent.Pressed)
    {
			// Check if the key pressed was 'R'
			if (keyEvent.Keycode == Key.R)
			{
				GetTree().ChangeSceneToFile(Constants.TITLE_SCREEN);
        }
    }
}
}
