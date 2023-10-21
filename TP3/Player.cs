using Godot;
using System;
using System.Data;

public partial class Player : CharacterBody2D
{
	[Export]
	private float Speed = 50.0f;
	const int ACCELERATION = 100;
	private Vector2 starting_direction;
	private Vector2 direction;
	private AnimationNodeStateMachinePlayback stateMachine;
	private AnimationTree animationTree;

	 void _ready() {
		stateMachine = GetNode<AnimationTree>("AnimationTree").Get("parameters/playback").As<AnimationNodeStateMachinePlayback>();
	    animationTree = GetNode<AnimationTree>("AnimationTree");
		starting_direction =  new Vector2(0,(float)0.1);
		updateAnimationParameter(starting_direction);
	 }

	private Vector2 GetInput() {
		direction = new Vector2(		
			Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
			Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
		);
		return direction;
	}

	private void updateAnimationParameter(Vector2 moveInput) {
		if(moveInput != Vector2.Zero) 
		{
			animationTree.Set("parameters/idle/blend_position", moveInput);
			animationTree.Set("parameters/walk/blend_position", moveInput);
		}
	}

	private void pickNewState() {
		if(Velocity != Vector2.Zero) {
			stateMachine.Travel("walk");
		}
		else {
			stateMachine.Travel("idle");
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		// Velocity is a property of RigidBody2D.

		//parameters/idle/blend_position
		Vector2 velocity = Velocity;

		// You should replace UI actions with custom gameplay actions.
		Vector2 direction = GetInput();
		//GD.Print(Position);
		updateAnimationParameter(direction);
		//GD.Print(velocity);

		velocity = direction  * ACCELERATION * Speed * (float)delta;	

		Velocity = velocity;
		MoveAndSlide();
		pickNewState();
	}
}
