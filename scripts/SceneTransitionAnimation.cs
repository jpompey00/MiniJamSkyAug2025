using Godot;
using System;

public partial class SceneTransitionAnimation : Node2D
{
	AnimationPlayer animationPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Visible = true;
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayFadeIn()
	{
		animationPlayer.Play("FadeIn");
	}

	public void PlayFadeOut()
	{
		animationPlayer.Play("FadeOut");
	}
}
