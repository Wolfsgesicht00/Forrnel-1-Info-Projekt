using Godot;
using System;

public partial class GameController : Node
{
 private Auto Player;
 private Area2D PlayerCollision;
 private TileMapLayer Track;

 public override void _Ready(){
    Player = GetNode<Auto>("/root/Game/Auto");
    PlayerCollision = GetNode<Area2D>("Auto/Area2D");
    Track =  GetNode<TileMapLayer>("Track1/TileMapLayer");
    GD.Print(Track);
    
 }

   public void _on_area_2d_body_entered(Node2D body){
      if(body is TileMapLayer) Player.Set_IsOffTrack(false);}
     
   
   public void _on_area_2d_body_exited(Node2D body){
      if(body is TileMapLayer) Player.Set_IsOffTrack(true);}

public override void _PhysicsProcess(double delta){

}


}
