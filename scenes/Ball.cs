using Godot;
using System;

public partial class Ball : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void Start(Vector2 startPositionPosition)
	{
		GD.Print("Ball: " + startPositionPosition);
		GlobalPosition = startPositionPosition;
		Show();
	}
}
