using Godot;
using System;

public partial class Projectile : CharacterBody2D, GodotLogging
{

    public float gravity = Constants.GRAVITY;
    Vector2 velocity;
    public Vector2 direction;
    float speed = Constants.SPEED;
    Player player;
    BaseStage stage;
    Timer timer;
    AnimationPlayer animationPlayer;
    [Signal]
    public delegate void ProjecileExpendedEventHandler();

    // [Signal]
    // public delegate void CollisionWithButtonEventHandler(Vector2I coordinates);

    // NEW: Track elapsed time for accurate physics
    private float _elapsedTime = 0f;
    private Vector2 _initialVelocity;

    public override void _Ready()
    {
        timer = new Timer();

        _initialVelocity = direction * speed;
        velocity = _initialVelocity; // Initialize
        player = GetParent().GetNode<Player>("Player");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("BoomerangFly");

        stage = (BaseStage)GetTree().CurrentScene;
        // GD.Print("stage");
        ProjecileExpended += stage.projectileExpended;

        //if projectile gets stuck.
        timer.Timeout += () => {EmitSignal(SignalName.ProjecileExpended); QueueFree(); };
        this.AddChild(timer);
        timer.Start(7f);
        
    }

    public override void _PhysicsProcess(double delta)
    {
        _elapsedTime += (float)delta;

        // 1. Calculate gravity effect using continuous time (matches trajectory math)
        float gravityEffect = 0.5f * gravity * Mathf.Pow(_elapsedTime, 2);

        // 2. Apply to velocity while preserving MoveAndCollide
        velocity.Y = _initialVelocity.Y + (gravity * _elapsedTime);

        // 3. Move with collision (unchanged from your original)
        Velocity = velocity;
        KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

        // Optional bounce (uncomment if needed)
        if (collision != null)
        {
            velocity = velocity.Bounce(collision.GetNormal()) * 0.6f;
        }

        if (Position.Y > 1000f)
        {
            EmitSignal(SignalName.ProjecileExpended);
            QueueFree();
        }


    }
    public void OnBodyEntered(Node2D node2D)
    {
        GodotLogging.log(this, node2D.GetType().ToString());
        if (node2D.GetType().ToString().Equals("Godot.TileMap"))
        {
            TileMap tileMap = (TileMap)node2D;
            Vector2 position = Position;
            Vector2I coords = tileMap.LocalToMap(tileMap.ToLocal(position));

            GD.Print("Cell: " + coords); //Emit signal w/ the vector 2 in it
            player.CollisionWithButtonEmitter(coords);
        // EmitSignal(SignalName.CollisionWithButton, position);

        }
     
    }
}
