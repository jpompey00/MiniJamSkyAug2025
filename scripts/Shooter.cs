
using Godot;

public partial class Shooter : Node2D, GodotLogging
{
	public Vector2 directionToCursor;
	public Projectile projectile;
	public PackedScene projetilePackedScene;
	public PackedScene curvedProjectilePackedScene;
	public Sprite2D arrow;
	Trajectory trajectory;
	Node2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		projetilePackedScene = GD.Load<PackedScene>("res://Scenes/projectile.tscn");
		curvedProjectilePackedScene = GD.Load<PackedScene>("res://Scenes/CurvedProjectile.tscn");
		// projectile = projetilePackedScene.Instantiate<Projectile>();
		arrow = GetNode<Sprite2D>("Sprite2D");
		trajectory = GetNode<Trajectory>("Line2D");
		// Node test = GetChild(1);
		player = GetNode<Node2D>("../Sprite2D");




	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mousePostion = GetGlobalMousePosition();
		directionToCursor = GlobalPosition.DirectionTo(mousePostion);
		float rotationAngle = GlobalPosition.AngleToPoint(mousePostion);
		Rotation = rotationAngle;


		// Show trajectory line

		Vector2 start = arrow.GlobalPosition;
		trajectory.GlobalPosition = start;
		Vector2 dir = directionToCursor;
		bool isFacingLeft = player.Scale.X < 0;
		Vector2 velocity = dir * Constants.SPEED;
		trajectory.ShowTrajectory(start, velocity);




		// GD.Print(Mathf.DegToRad(rotationAngle));
		// GD.Print(directionToCursor);
		// GodotLogging.log(this, "Direction: " + directionToCursor);
		// GD.Print(arrow.GlobalPosition);
	}


	public void ShootProjectile()
	{
		// GetParent().GetParent().AddChild(projectile);
		Projectile projectile = projetilePackedScene.Instantiate<Projectile>();
		projectile.direction = directionToCursor;
		GetTree().CurrentScene.AddChild(projectile);
		projectile.Position = new Vector2(arrow.GlobalPosition.X, arrow.GlobalPosition.Y);

		// GodotLogging.log(this, "projectile position: " + projectile.Position);
		// GodotLogging.log(this, "projectile direction: " + projectile.direction);

	}


	public void ShootCurvedProjectile()
	{
		CurvedProjectile projectile = curvedProjectilePackedScene.Instantiate<CurvedProjectile>();
		projectile.direction = directionToCursor;
		GetTree().CurrentScene.AddChild(projectile);
		projectile.Position = new Vector2(arrow.GlobalPosition.X, arrow.GlobalPosition.Y);
	}
}
