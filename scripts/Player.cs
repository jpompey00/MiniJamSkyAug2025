using Godot;
using Microsoft.VisualBasic;
using System;
using System.Reflection.Metadata;
using System.Threading;
using Timer = Godot.Timer;


public partial class Player : CharacterBody2D
{
	public float gravity = Constants.GRAVITY;
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

	//set these at the ready
	int actionCount = -1;
	int baseAmmoCount = -1;
	int specialShotLimit;

	[Signal]
	public delegate void NoAmmoLeftEventHandler();

	[Signal]
	public delegate void NoMovesLeftEventHandler();

	[Signal]
	public delegate void CollisionWithButtonEventHandler(Vector2I coordiantes);
	[Signal]
	public delegate void CollisionWithPressurePlateEventHandler(Vector2I coordinates);
	[Signal]
	public delegate void ReleasedPressurePlateEventHandler(Vector2I coordinates);
	[Signal]
	public delegate void AmmoDecreasedEventHandler();
	[Signal]
	public delegate void MovementDecreasedEventHandler();

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

	}



	public void OnBodyEntered(Node2D node)
	{
		// GodotLogging.log(this, node.ToString());
		//for the pressure plate.

		GodotLogging.log(this, node.GetType().ToString());
		if (node.GetType().ToString().Equals("Godot.TileMap"))
		{
			TileMap tileMap = (TileMap)node;
			Vector2 position = Position;
			Vector2I coords = tileMap.LocalToMap(tileMap.ToLocal(position));

			GD.Print("Cell: " + coords); //Emit signal w/ the vector 2 in it
										 // node.CollisionWithButtonEmitter(coords);
			EmitSignal(SignalName.CollisionWithPressurePlate, coords);

		}

	}

	public void OnBodyExited(Node2D node)
	{
		if (node.GetType().ToString().Equals("Godot.TileMap"))
		{
			TileMap tileMap = (TileMap)node;
			Vector2 position = Position;
			Vector2I coords = tileMap.LocalToMap(tileMap.ToLocal(position));

			GD.Print("Cell: " + coords); //Emit signal w/ the vector 2 in it
			EmitSignal(SignalName.ReleasedPressurePlate, coords);
		}

	}


	public void CollisionWithButtonEmitter(Vector2I coordinates)
	{
		GD.Print("Emitter Called");
		EmitSignal(SignalName.CollisionWithButton, coordinates);
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


	public void setActionAndAmmo(int action, int ammo)
	{
		actionCount = action;
		baseAmmoCount = ammo;
	}

	public void resetActionAndAmmo()
	{
		actionCount = -1;
		baseAmmoCount = -1;
	}

	public void decreaseAmmo()
	{
		baseAmmoCount -= 1;
		EmitSignal(SignalName.AmmoDecreased);
		if (baseAmmoCount == 0)
		{
			// EmitSignal(SignalName.NoAmmoLeft);
		}
	}

	public void decrementMovement()
	{
		if (actionCount != 999)
		{
			actionCount -= 1;
			EmitSignal(SignalName.MovementDecreased);
			if (actionCount == 0)
			{
				// EmitSignal(SignalName.NoMovesLeft);
			}
		}

	}

	public void addMovement()
	{
		actionCount = 999;
		GD.Print(actionCount);
	}

	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector(Constants.CONTROLS_LEFT, Constants.CONTROLS_RIGHT, Constants.CONTROLS_THROW1, Constants.CONTROLS_THROW2);
		GD.Print(inputDirection);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (actionCount == -1 || baseAmmoCount == -1)
		{
			//setting up
			return;
		}

		if (Input.IsActionJustPressed(Constants.CONTROLS_LEFT) || Input.IsActionJustPressed(Constants.CONTROLS_RIGHT))
		{
			tiledata = tileMap.GetCellTileData(0, tileMap.LocalToMap(Position));
			// GD.Print(tiledata.GetCustomData(Constants.CUSTOM_DATA_TILE_POSITION));
			positionOnTile = (int)tiledata.GetCustomData(Constants.CUSTOM_DATA_TILE_POSITION);
		}

		if (Input.IsActionJustPressed(Constants.CONTROLS_LEFT) && !(positionOnTile == (int)Constants.POSITION_ON_TILE_ON_LEFT_EDGE) && actionCount > 0)
		{
			// GD.Print("Left Pressed, allowed to move left");
			// animatedSprite2D.Scale = new Vector2(-1, 1);
			Position = new Vector2(Position.X - 32, Position.Y);
			if (actionCount != 999)
			{
				decrementMovement();
			}

		}
		if (Input.IsActionJustPressed(Constants.CONTROLS_RIGHT) && !(positionOnTile == (int)Constants.POSITION_ON_TILE_ON_RIGHT_EDGE) && actionCount > 0)
		{
			// GD.Print("Right Pressed, allowed to move right");
			// animatedSprite2D.Scale = new Vector2(1, 1);
			Position = new Vector2(Position.X + 32, Position.Y);
			if (actionCount != 999)
			{
				decrementMovement();
			}
		}
		if (Input.IsActionJustPressed(Constants.CONTROLS_THROW1))
		{
			// GD.Print("Throw-1 Pressed");
			// GD.Print(Position);
			if (!shotIsOnCooldown && actionCount > 0 && baseAmmoCount > 0)
			{
				animatedSprite2D.Play("throw");
				shooter.ShootProjectile();
				shotIsOnCooldown = true;
				cooldownTimer.Start(Constants.COOLDOWN_TIME_SHOT);
				// decrementMovement();
				decreaseAmmo();
				//issue here.

				// animatedSprite2D.Stop();
			}

		}
		// if (Input.IsActionJustPressed(Constants.CONTROLS_THROW2))
		// {
		// 	GD.Print("Throw-2 Pressed");
		// 	shooter.ShootCurvedProjectile();
		// }


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
