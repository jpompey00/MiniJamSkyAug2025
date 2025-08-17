using Godot;
using System;

public partial class CurvedProjectile : CharacterBody2D, GodotLogging
{

    public float gravity = Constants.GRAVITY;
    Vector2 velocity;
    public Vector2 direction = new Vector2(-1, 0);
    float speed = Constants.SPEED-300;

    // // NEW: Track elapsed time for accurate physics
    // private float _elapsedTime = 0f;
    // private Vector2 _initialVelocity;

    // private float _time;
    // private Vector2 _startPos;
    // private Vector2 _perpendicular;
    // // public float Speed = 200;        // forward speed
    // Boomerang physics
    private float _flightTime;
    private bool _isReturning;
    private Node2D _thrower;
    [Export] public float MaxRange = 400f;
    [Export] public float CurveIntensity = 3f;
    private Vector2 _startPosition;
    private float _peakCurve; // Stores maximum curve achieved

    public void Launch(Vector2 launchDirection, Node2D thrower)
    {
        direction = launchDirection.Normalized();
        velocity = direction * speed;
        _thrower = thrower;
        _startPosition = GlobalPosition;
        _flightTime = 0f;
        _peakCurve = 0f;
        Rotation = direction.Angle();
    }

    public override void _PhysicsProcess(double delta)
    {
        _flightTime += (float)delta;
        
        // 1. Calculate flight progress (0-1 = outward, 1-2 = returning)
        float progress = Mathf.Clamp(_flightTime * speed / MaxRange, 0, 2);
        
        // 2. Update curve only during outward flight
        if (!_isReturning)
        {
            // Sine wave from 0 to π (half cycle)
            float curvePhase = progress * Mathf.Pi;
            _peakCurve = Mathf.Sin(curvePhase) * CurveIntensity;
            
            // Check for return transition
            if (progress >= 1f)
            {
                _isReturning = true;
            }
        }
        
        // 3. Apply movement with consistent curve
        Vector2 moveDirection = direction.Rotated(_peakCurve);
        velocity = moveDirection * speed * (_isReturning ? -1 : 1);
        velocity.Y += gravity * (float)delta * 0.2f; // Reduced gravity
        
        // 4. Execute movement
        Velocity = velocity;
        MoveAndCollide(Velocity * (float)delta);
        
        // 5. Visual rotation (continuous in one direction)
        Rotation += 12f * (float)delta;
        
        // 6. Catch detection
        if (_isReturning && GlobalPosition.DistanceTo(_thrower.GlobalPosition) < 25f)
        {
            QueueFree();
        }
        
    }
    public void OnBodyEntered(Node2D node2D)
    {
        // GodotLogging.log(this, node2D.ToString());
        //ADD SCRIPT FOR WHEN TILE BUTTON IS INTERACTED WITH FUCKING SHIT AHHHHH
    }
}
