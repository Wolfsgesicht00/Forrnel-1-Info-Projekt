using Godot;
using System;

public partial class GameController : Node
{
 private Auto1 Player;
 private Area2D PlayerCollision;
 private TileMapLayer Track;
 private Path2D track1;
 private Path2D track2;
 private PackedScene checkpointscene;
 private Godot.Collections.Array checkpoints; 

 public override void _Ready(){
    Player = GetNode<Auto1>("/root/Game/Auto");
    PlayerCollision = GetNode<Area2D>("/root/Game/Auto/Area2D");
    Track =  GetNode<TileMapLayer>("/root/Game/Tracks/TileMapLayer");
    //GD.Print(Track);
    checkpoints = new Godot.Collections.Array<Track_Checkpoint>{};
    track1 = GetNode<Path2D>("/root/Game/Tracks/TileMapLayer/Track_1");
    checkpointscene = GD.Load<PackedScene>("res://Track_Checkpoint.tscn");
    instanceCheckpointsTrack1();
 }

   public Vector2 CalcTileMapPos(){ //Methode falsch rum gedacht?
   Vector2 playerGlobal = Player.GlobalPosition;
   Vector2 playerLocalToTilemap = Track.ToGlobal(playerGlobal);
   Vector2I cell = Track.LocalToMap(playerLocalToTilemap);
   //GD.Print("Cell: " + cell);
   return cell;
   }

  

   public void _on_area_2d_body_entered(Node2D body){
      if(body is TileMapLayer) Player.Set_IsOffTrack(false);}
     
   
   public void _on_area_2d_body_exited(Node2D body){
      if(body is TileMapLayer) Player.Set_IsOffTrack(true);}

public override void _PhysicsProcess(double delta){
   CalcTileMapPos();
}

   public void instanceCheckpointsTrack1()
   {
      for (int i = 0; i< track1.getPointCount(); i++)
      {
         checkpoints.add(checkpointscene.Instantiate());
         Vector2 current = track1.getPointPosition(i);
         Vector2 next = track1.getPointPosition(i+1);
         checkpoints.get(i).Rotation = current.AngletoPoint(next);
      }

   }



}
