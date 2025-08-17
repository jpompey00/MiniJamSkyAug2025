using Godot;
using System;

public partial class Enemy1 : StaticBody2D, GodotLogging
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void OnBodyEntered(Node2D node)
	{
		GodotLogging.log(this, node.ToString());
		//add code here for when projectile interacts with it
	}
}
