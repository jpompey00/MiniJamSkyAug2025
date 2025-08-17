using Godot;
using Microsoft.VisualBasic;
using System;
using System.Reflection.Metadata;
using System.Threading;
using Timer = Godot.Timer;


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
	//add timer 
	Timer cooldownTimer;
	Boolean shotIsOnCooldown;
	int actionLimit = 6;
	int baseShotLimit = 3;
	int specialShotLimit;




	public override void _Ready()
	{
		tileMap = GetNode<TileMap>("../TileMap");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		shooter = GetNode<Shooter>("Shooter");


		cooldownTimer = new Timer();
		// CallDeferred()
		GetTree().CurrentScene.CallDeferred(MethodName.AddChild, cooldownTimer);
		// AddChild(cooldownTimer);
		cooldownTimer.Timeout += () =>
		{
			shotIsOnCooldown = false;
			GD.Print("is off cooldown");
			cooldownTimer.Stop();

		};
		// GD.Print(tileMap);
		// tiledata = tileMap.GetCellTileData(0, new Vector2I(1, 1));

		//gets the vector2I Position for this, pretty sick
		// GD.Print(Position);
		// GD.Print(tileMap.LocalToMap(Position));
	}


	public void OnBodyEntered(Node2D node)
	{
		GodotLogging.log(this, node.ToString());
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
		Vector2 inputDirection = Input.GetVector(Constants.CONTROLS_LEFT, Constants.CONTROLS_RIGHT, Constants.CONTROLS_THROW1, Constants.CONTROLS_THROW2);
		GD.Print(inputDirection);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//todo kyok - clean this up using this way of doing it instead of my way :D
		// Vector2 inputDirection = Input.GetVector(Controls.Left.ToString(), Controls.Right.ToString(), Controls.Throw1.ToString(), Controls.Throw2.ToString());

		//this can likely be fixed to one if statement that just changes based on the vectors of the input actions
		//i thinks
		if (Input.IsActionJustPressed(Constants.CONTROLS_LEFT) || Input.IsActionJustPressed(Constants.CONTROLS_RIGHT))
		{
			tiledata = tileMap.GetCellTileData(0, tileMap.LocalToMap(Position));
			GD.Print(tiledata.GetCustomData(Constants.CUSTOM_DATA_TILE_POSITION));
			positionOnTile = (int)tiledata.GetCustomData(Constants.CUSTOM_DATA_TILE_POSITION);
		}

		if (Input.IsActionJustPressed(Constants.CONTROLS_LEFT) && !(positionOnTile == (int)Constants.POSITION_ON_TILE_ON_LEFT_EDGE))
		{
			// GD.Print("Left Pressed, allowed to move left");
			// animatedSprite2D.Scale = new Vector2(-1, 1);
			Position = new Vector2(Position.X - 32, Position.Y);

		}
		if (Input.IsActionJustPressed(Constants.CONTROLS_RIGHT) && !(positionOnTile == (int)Constants.POSITION_ON_TILE_ON_RIGHT_EDGE))
		{
			// GD.Print("Right Pressed, allowed to move right");
			// animatedSprite2D.Scale = new Vector2(1, 1);
			Position = new Vector2(Position.X + 32, Position.Y);
		}
		if (Input.IsActionJustPressed(Constants.CONTROLS_THROW1))
		{
			// GD.Print("Throw-1 Pressed");
			// GD.Print(Position);
			if (!shotIsOnCooldown && actionLimit > 0 && baseShotLimit > 0)
			{
				shooter.ShootProjectile();
				shotIsOnCooldown = true;
				cooldownTimer.Start(Constants.COOLDOWN_TIME_SHOT);
				actionLimit -= 1;
				baseShotLimit -= 1;
				GodotLogging.log(this, "Action Limit: " + actionLimit);
				GodotLogging.log(this, "Base Shot Limit: " + baseShotLimit);
			}

		}
		if (Input.IsActionJustPressed(Constants.CONTROLS_THROW2))
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
