using Godot;
using System;

public partial class Button : Area2D, GodotLogging
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node2D body)
	{
		// GodotLogging.log(this, "Entered Body: " + body);	
	}

}
