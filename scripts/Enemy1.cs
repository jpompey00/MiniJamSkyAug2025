using Godot;
using System;

public partial class Enemy1 : Area2D, GodotLogging
{
	AnimatedSprite2D animatedSprite2D;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void OnBodyEntered(Node2D node)
	{
		GodotLogging.log(this, node.Name);
		// GD.Print(node.Name);
		//add code here for when projectile interacts with it
	}

	public void OnAreaEntered(Area2D area)
	{
		GodotLogging.log(this, area.Name);
		if (area.Name.Equals("ProjectileArea2D"))
		{
			this.QueueFree();
		}
	}
}
