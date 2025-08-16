using Godot;
using Microsoft.VisualBasic;
using System;
using System.Reflection.Metadata;
using System.Threading;


public partial class player : CharacterBody2D
{
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	TileMap tileMap;
	TileData tiledata;

	Boolean isOnRightEdge = false;
	Boolean isOnLeftEdge = false;
	int positionOnTile;
	AnimatedSprite2D animatedSprite2D;
	Shooter shooter;


	public override void _Ready()
	{
		tileMap = GetNode<TileMap>("../TileMap");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		shooter = GetNode<Shooter>("Shooter");
		// GD.Print(tileMap);
		// tiledata = tileMap.GetCellTileData(0, new Vector2I(1, 1));

		//gets the vector2I Position for this, pretty sick
		// GD.Print(Position);
		// GD.Print(tileMap.LocalToMap(Position));

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		// if (!IsOnFloor())
		velocity.Y += gravity * (float)delta;
		Velocity = velocity;
		MoveAndSlide();
	}


	// Called when the node enters the scene tree for the first time.

	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector(Controls.Left.ToString(), Controls.Right.ToString(), Controls.Throw1.ToString(), Controls.Throw2.ToString());
		GD.Print(inputDirection);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//todo kyok - clean this up using this way of doing it instead of my way :D
		// Vector2 inputDirection = Input.GetVector(Controls.Left.ToString(), Controls.Right.ToString(), Controls.Throw1.ToString(), Controls.Throw2.ToString());

		//this can likely be fixed to one if statement that just changes based on the vectors of the input actions
		//i thinks
		if (Input.IsActionJustPressed(Controls.Left.ToString()) || Input.IsActionJustPressed(Controls.Right.ToString()))
		{
			// GD.Print(tiledata.GetCustomData(CustomDataNames.tilePosition.ToString()));
			// GD.Print(tiledata.GetCustomData(CustomDataNames.tilePosition.ToString()));
			tiledata = tileMap.GetCellTileData(0, tileMap.LocalToMap(Position));
			GD.Print(tiledata.GetCustomData(CustomDataNames.tilePosition.ToString()));
			positionOnTile = (int)tiledata.GetCustomData(CustomDataNames.tilePosition.ToString());
		}

		if (Input.IsActionJustPressed(Controls.Left.ToString()) && !(positionOnTile == (int)PositionOnTile.ON_LEFT_EDGE))
		{
			GD.Print("Left Pressed, allowed to move left");
			// animatedSprite2D.Scale = new Vector2(-1, 1);
			Position = new Vector2(Position.X - 32, Position.Y);

		}
		if (Input.IsActionJustPressed(Controls.Right.ToString()) && !(positionOnTile == (int)PositionOnTile.ON_RIGHT_EDGE))
		{
			GD.Print("Right Pressed, allowed to move right");
			// animatedSprite2D.Scale = new Vector2(1, 1);
			Position = new Vector2(Position.X + 32, Position.Y);
		}
		if (Input.IsActionJustPressed(Controls.Throw1.ToString()))
		{
			GD.Print("Throw-1 Pressed");
			// GD.Print(Position);
			shooter.ShootProjectile(new Vector2(Position.X +20, Position.Y));

		}
		if (Input.IsActionJustPressed(Controls.Throw2.ToString()))
		{
			GD.Print("Throw-2 Pressed");
		}


		if ((shooter != null) && shooter.directionToCursor.X < 0)
		{
			// GD.Print(shooter.directionToCursor.X);
			animatedSprite2D.Scale = new Vector2(-1, 1);
			// GD.Print("face left");
		}
		if ((shooter != null) && shooter.directionToCursor.X > 0)
		{
			animatedSprite2D.Scale = new Vector2(1, 1);
			// GD.Print("face right");
		}

	}
}
