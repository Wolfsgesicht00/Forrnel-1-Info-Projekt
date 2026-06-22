using Godot;
using System;

public partial class GameController : Node
{
 private Auto1 Player;
 private Area2D PlayerCollision;
 private TileMapLayer Track;

 public override void _Ready(){
    Player = GetNode<Auto1>("/root/Game/Auto");
    PlayerCollision = GetNode<Area2D>("Auto/Area2D");
    Track =  GetNode<TileMapLayer>("/root/Game/Track2/TileMapLayer");
    GD.Print(Track);
    
 }

   public Vector2 CalcTilemapPos(){
   Vector2 playerGlobal = Player.GlobalPosition;
   Vector2 playerLocalToTilemap = Track.ToLocal(playerGlobal);
   Vector2I cell = Track.LocalToMap(playerLocalToTilemap);
   return cell;
   }

   public void _on_area_2d_body_entered(Node2D body){
      if(body is TileMapLayer) Player.Set_IsOffTrack(false);}
     
   
   public void _on_area_2d_body_exited(Node2D body){
      if(body is TileMapLayer) Player.Set_IsOffTrack(true);}

public override void _PhysicsProcess(double delta){

}


}
