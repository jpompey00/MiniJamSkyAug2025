using Godot;
using System;
using System.Threading;


public partial class player : CharacterBody2D
{
	// public const float Speed = 300.0f;
	// public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	// public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	// public override void _PhysicsProcess(double delta)
	// {
	// 	Vector2 velocity = Velocity;

	// 	// Add the gravity.
	// 	if (!IsOnFloor())
	// 		velocity.Y += gravity * (float)delta;

	// 	// Handle Jump.
	// 	if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
	// 		velocity.Y = JumpVelocity;

	// 	// Get the input direction and handle the movement/deceleration.
	// 	// As good practice, you should replace UI actions with custom gameplay actions.
	// 	Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
	// 	if (direction != Vector2.Zero)
	// 	{
	// 		velocity.X = direction.X * Speed;
	// 	}
	// 	else
	// 	{
	// 		velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
	// 	}

	// 	Velocity = velocity;
	// 	MoveAndSlide();
	// }


	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed(Controls.Left.ToString()))
		{
			GD.Print("Left Pressed");
			Position = new Vector2(Position.X-100, Position.Y);
		}
		if (Input.IsActionJustPressed(Controls.Right.ToString()))
		{
			Position = new Vector2(Position.X+100, Position.Y);
		}
		if (Input.IsActionJustPressed(Controls.Throw1.ToString()))
		{
			GD.Print("Throw-1 Pressed");
		}
		if (Input.IsActionJustPressed(Controls.Throw2.ToString()))
		{
			GD.Print("Throw-2 Pressed");
		}
	}
}
