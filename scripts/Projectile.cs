using Godot;
using System;

public partial class Projectile : CharacterBody2D, GodotLogging
{
	// public const float Speed = 300.0f;
	// public const float JumpVelocity = -400.0f;
	//on colission layer 2
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	Vector2 velocity;
	public Vector2 direction; //set on instantiation
	float speed = Constants.SPEED;


	public override void _Ready()
	{
		// velocity = direction * speed;
		velocity = direction * speed;
		// GD.Print(this, direction);
	}

	public override void _PhysicsProcess(double delta)
	{
		velocity.Y += gravity * (float)delta;

		Velocity = velocity;
		KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

		GD.Print(Velocity);
		
		// if (!(collision == null))
		// {

		// velocity = velocity.Bounce(collision.GetNormal()) * .6f; 
		// }
	}
}
