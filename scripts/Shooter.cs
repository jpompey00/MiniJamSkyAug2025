using Godot;

public partial class Shooter : Node2D, GodotLogging
{
	public Vector2 directionToCursor;
	public Projectile projectile;
	public PackedScene projetilePackedScene;
	public Sprite2D arrow;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		projetilePackedScene = GD.Load<PackedScene>("res://Scenes/projectile.tscn");
		// projectile = projetilePackedScene.Instantiate<Projectile>();
		arrow = GetNode<Sprite2D>("Sprite2D");

		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mousePostion = GetGlobalMousePosition();
		directionToCursor = GlobalPosition.DirectionTo(mousePostion);
		float rotationAngle = GlobalPosition.AngleToPoint(mousePostion);
		Rotation = rotationAngle;

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
}
