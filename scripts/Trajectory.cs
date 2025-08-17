using Godot;


public partial class Trajectory : Line2D
{

	public Vector2 directionToCursor;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	Vector2 velocity;
	int[] max_points = new int[10];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}


	public void Update_trajectory(Vector2 direction, float speed)
	{
		Position = Vector2.Zero;

		//AddPoint(Position += new Vector2(1, 0));
		velocity = Vector2.Zero;
		gravity = gravity / 200;
		speed = speed/200;
		ClearPoints();
		foreach (int i in max_points)
		{
			AddPoint(Position);
			velocity.X = direction.X * speed;
			velocity.Y += gravity;
			GD.Print(velocity);
			Position += velocity;
			//AddPoint(Position += new Vector2(5, 0));
		}


	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mousePostion = GetGlobalMousePosition();
		Update_trajectory(GlobalPosition.DirectionTo(mousePostion), Constants.SPEED);
		
		
	}
}
