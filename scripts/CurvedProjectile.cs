using Godot;
using System;

public partial class CurvedProjectile : CharacterBody2D, GodotLogging
{

    public float gravity = Constants.GRAVITY;
    Vector2 velocity;
    public Vector2 direction = new Vector2(-1, 0);
    float speed = Constants.SPEED;

    // NEW: Track elapsed time for accurate physics
    private float _elapsedTime = 0f;
    private Vector2 _initialVelocity;

    private float _time;
    private Vector2 _startPos;
    private Vector2 _perpendicular;
    // public float Speed = 200;        // forward speed


    public override void _Ready()
    {
        _initialVelocity = direction * speed;
        velocity = _initialVelocity; // Initialize


        _startPos = GlobalPosition;
        _perpendicular = new Vector2(-direction.Y, direction.X).Normalized();
        GD.Print(_perpendicular);
    }

    public override void _PhysicsProcess(double delta)
    {
        // float Speed = 300f;        // forward speed
        //     float WaveAmplitude = 100f;// "sideways speed"
        //  float WaveFrequency = 5f;  // oscillation speed
        float projectileSpeed = 200;
        //fix this
        Vector2 Direction = Vector2.Left; // base direction

        float Amplitude = 100f;     // size of wave
        float Frequency = 10;

        _time += (float)delta;

        // Base forward motion aka distance
        float forwardDist = projectileSpeed * _time;
        Vector2 forward = Direction.Normalized() * forwardDist;

        // Sinusoidal offset (absolute, not per-frame)
        float offset = Mathf.Sin(_time * Frequency) * Amplitude;
        Vector2 wave = _perpendicular * offset;

        // Set velocity as the *change in position* since last frame
        Vector2 targetPos = _startPos + forward + wave;
        Velocity = (targetPos - GlobalPosition) / (float)delta;

        MoveAndSlide();
    }
    public void OnBodyEntered(Node2D node2D)
    {
        // GodotLogging.log(this, node2D.ToString());
        //ADD SCRIPT FOR WHEN TILE BUTTON IS INTERACTED WITH FUCKING SHIT AHHHHH
    }
}
