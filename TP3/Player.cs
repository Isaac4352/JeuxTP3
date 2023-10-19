using Godot;
using System;
using System.Data;

public partial class Player : CharacterBody2D
{
	[Export]
	private float Speed = 50.0f;

	const int ACCELERATION = 100;

	private Vector2 GetInput() {
		var direction = new Vector2();

		direction.X = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		direction.Y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		return direction;
	}
	public override void _PhysicsProcess(double delta)
	{
		// Velocity is a property of RigidBody2D.
		var dt = (float)delta;

		Vector2 velocity = Velocity;
		AnimationPlayer animation = GetNode<AnimationPlayer>("AnimationPlayer");

		// You should replace UI actions with custom gameplay actions.
		Vector2 direction = GetInput().Normalized();
		if (direction != Vector2.Zero)
		{
			velocity = direction  * ACCELERATION * Speed * (float)delta; //peut aller trop loin

			if(	Input.IsActionPressed("ui_right")) animation.Play("walk");
			
			
			if(	Input.IsActionPressed("ui_left")) animation.Play("walk_left");
		
			if(	Input.IsActionPressed("ui_up")) animation.Play("walk_up");
				
	
			if(	Input.IsActionPressed("ui_down")) animation.Play("walk_down");
			
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			animation.Stop();
		}		

		Velocity = velocity;
		MoveAndSlide();
	}
}
