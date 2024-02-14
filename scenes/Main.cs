using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void NewGame()
	{
		var playerField = GetNode<Control>("PlayerField");
		var playerFieldRect = playerField.GetGlobalRect();

		var ball = GetNode<Ball>("Ball");
		var ballStartPosition = GetNode<Marker2D>("BallPosition");
		ball.Start(ballStartPosition.GlobalPosition);
		
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.GlobalPosition, playerFieldRect);
	}
}
