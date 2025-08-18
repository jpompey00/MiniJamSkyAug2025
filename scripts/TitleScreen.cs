using Godot;
using System;

public partial class TitleScreen : Node2D
{


	// Called when the node enters the scene tree for the first time.
	AnimationPlayer protagTower;
	AnimationPlayer titleScreen;
	SceneTransitionAnimation sceneTransitionAnimation;
	Boolean canInteract = false;
	AudioStreamPlayer2D audioStreamPlayer2D;
	public override async void _Ready()
	{
		audioStreamPlayer2D = GetTree().Root.GetNode<AudioStreamPlayer2D>("MusicPlayer");
		audioStreamPlayer2D.Stream = GD.Load<AudioStream>(Constants.TITLE_SCREEN_MUSIC);
		audioStreamPlayer2D.Play();

		sceneTransitionAnimation = GetNode<SceneTransitionAnimation>("SceneTransitionAnimation");
		sceneTransitionAnimation.Visible = false;
		//play animations
		protagTower = GetNode<AnimationPlayer>("ProtagTowerSlideAnimation");
		titleScreen = GetNode<AnimationPlayer>("Title Screen Animation");


		protagTower.Play("Protag and Tower Slide in");
		// await ToSignal(protagTower.Play("Protag and Tower Slide in"), AnimationPlayer.SignalName.AnimationFinished);
		await protagTower.ToSignal(protagTower, AnimationPlayer.SignalName.AnimationFinished);
		titleScreen.Play("Title Screen Idle");
		canInteract = true;
		
	}

  public override async void _Input(InputEvent @event)
{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed && canInteract)
		{
			// GD.Print("Key pressed: " + (Key)keyEvent.Keycode);
			sceneTransitionAnimation.Visible = true;
			sceneTransitionAnimation.PlayFadeOut();
			await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
			GetTree().ChangeSceneToFile(Constants.FALL_ANIMATION_PATH);
		
    }
}

}
