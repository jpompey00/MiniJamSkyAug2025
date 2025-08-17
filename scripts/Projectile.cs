using Godot;
using System;

public partial class Projectile : CharacterBody2D, GodotLogging
{
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    Vector2 velocity;
    public Vector2 direction;
    float speed = Constants.SPEED;
    
    // NEW: Track elapsed time for accurate physics
    private float _elapsedTime = 0f;
    private Vector2 _initialVelocity;

    public override void _Ready()
    {
        _initialVelocity = direction * speed;
        velocity = _initialVelocity; // Initialize
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
        // if (collision != null) 
        // {
        //     velocity = velocity.Bounce(collision.GetNormal()) * 0.6f;
        // }
    }
}