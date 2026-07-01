using Godot;
using System;

public partial class Track_Checkpoint : Area2D
{
	private bool isFinishLine = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public bool on_area_entered(Node2D body){
	 	return true;

	}
	public bool on_area_exited(Node2D body){
		return true;
	}
}
