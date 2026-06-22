using Godot;
using System;

public partial class Auto1 : CharacterBody2D
{
private bool isOffTrack = false;
private float currentRotation;
private float kurvenradius = 0;
private float reibung = 2.0f; //Wert anpassen je nach Fahrzeug Todo
private double maxv = 100;
float deltatime = 0;
Vector2 forward;
bool drifting = false;

public override void _PhysicsProcess(double delta)
{
		//GD.Print(Velocity);
		float dt = (float) delta;

//Berechnung Reibung und Kurvenfahrt
	BerechnungTurn(Velocity, dt);

//Rotation
	float turnInput = 0f;
    if (Input.IsActionPressed("Turn-Right")) turnInput += 1f;
    if (Input.IsActionPressed("Turn-Left")) turnInput -= 1f;
  	Rotation += turnInput * 2.5f * dt;	

//Movement
	float moveInput = 0f;
    if (Input.IsActionPressed("Acceleration")) moveInput = 1.7f;
    if (Input.IsActionPressed("Brake")) {if(Velocity.Length() > 0) {Velocity = Velocity.Normalized()*(Velocity.Length()-1.7f);
	moveInput = 0;}}
   	forward = Vector2.Up.Rotated(Rotation);
	//GD.Print(forward);
	//Friction Braking
	if(!Input.IsActionPressed("Acceleration") && !Input.IsActionPressed("Brake")){
      Velocity *= 0.999f; 
	  moveInput = 0;}
    	Vector2 neuGeschwindigkeit = (Velocity.Length() + moveInput)*forward;
    	neuGeschwindigkeit = neuGeschwindigkeit.LimitLength(400);
		if(Velocity.Length() <= maxv && drifting == false){
			Velocity = neuGeschwindigkeit;
			//GD.Print("driving forward");
		}else if (Velocity.Length() > maxv){
		//Velocity = Drift(dt) * Velocity.Length();
		Velocity = 0.6f*Velocity;
		//GD.Print("drifting");
		drifting = true;}
		if(Velocity.Length()< 30) drifting = false;
		deltatime += dt;
		MoveAndSlide();
}
	


	public Vector2 Drift(float dt){
		Vector2 velocity = new Vector2(0,1);
	if(Velocity.Length() > 1){
		float minradius = (Velocity.Length()*Velocity.Length())/(reibung*10f);
    float deltaomega = Velocity.Length()/minradius;
	//GD.Print(deltaomega + "Delta Omega");
    float maxTurn = deltaomega * dt*2000;
	//GD.Print("Maxturn" + maxTurn);
	double angleTo = Velocity.AngleTo(forward);
	//GD.Print(angleTo + "Angleto");
	float turn = (float) angleTo * 2;
	turn = Mathf.Clamp(turn, -maxTurn, maxTurn);
	velocity = Velocity.Rotated(turn).LimitLength(1);
	//GD.Print("velocity " + velocity + " " + Velocity);
	return velocity;}
	else return new Vector2(0,0);

}
	public void BerechnungTurn(Vector2 vel, float dt){
	 if(isOffTrack) reibung = 1f;
  		else reibung = 2.0f;
	float drotation = Mathf.AngleDifference(Rotation, currentRotation);
	if (vel.Length() > 0 && Math.Abs(drotation) > 0.001f){
        kurvenradius = 2*Math.Abs(vel.Length() / (drotation / dt));}
    else kurvenradius = 9999f;
	maxv = 6* Math.Sqrt(kurvenradius * 10f * reibung);
    currentRotation = Rotation;}

	public void Set_IsOffTrack (bool input){
    isOffTrack = input;
  }
}
