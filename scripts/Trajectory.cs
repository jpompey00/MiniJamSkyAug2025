using Godot;
using System.Collections.Generic;

public partial class Trajectory : Line2D
{
    [Export] public int NumPoints = 50;
    [Export] public float TimeStep = 0.05f;
    
    private Vector2 GetGravity()
    {
        return new Vector2(0, Constants.GRAVITY);
    }

    public void ShowTrajectory(Vector2 startGlobal, Vector2 initialVelocity)
{
    float gravity = Constants.GRAVITY;
    Vector2[] pts = new Vector2[NumPoints];

    for (int i = 0; i < NumPoints; i++)
    {
        float t = i * TimeStep;
        // More precise calculation:
        Vector2 displacement = initialVelocity * t + new Vector2(0, 0.5f * gravity * t * t);
        pts[i] = ToLocal(startGlobal + displacement);
    }

    Points = pts;
}
    
    }
		// GD.Print(pts.Length);

		
	