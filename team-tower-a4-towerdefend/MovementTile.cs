using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class MovementTile : TileEntity
    {
        public MovementTile(Scene setScene) : base(setScene)
        {
            sprite = Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\MovementTile.png");
        }
        public override void Update()
        {
        }
        public override void StdDraw()
        {
            Graphics.Scale = Scene.graphicsSize;
            Graphics.Draw(sprite, this.position);
        }

        // Detect if each enemy is on the tile
        
        public void MoveEnemy()
        {
            // First, make sure we're only affecting enemies within the tile
            Vector2 gridSpotXY = Scene.GetGridSpot(this);
            foreach (Entity Entity in Scene.entities)
            {

                
                if (Entity is Enemy Enemy)
                {
                    // Check if enemy is within the tile
                    Vector2 enemyPos = Enemy.position;
                    Vector2[] tileSize = Scene.GetTileBounds(this);
                    if (this.position.X < enemyPos.X &&
                        this.position.Y < enemyPos.Y &&
                        tileSize[0].X > enemyPos.X &&
                        tileSize[0].Y > enemyPos.Y
                        )
                    {
                        // Find cardinal movement tiles that haven't been moved on by enemy
                        
                    }

                }
            }
            // 
            // 
        }
        
        /*
        public void CheckEnemy()
        {
            Vector2 getCentre = this.GetCentreOnScreen();
            Vector2 gridSpotXY = this.GetGridSpot();
            foreach (Entity Entity in Scene.entities)
            {
                if (Entity is Enemy Enemy)
                {
                    /*
                    // Mark the closest tile (length of 2 tiles)
                    if (Vector2.DistanceSquared(MovementTile.position,))
                    {

                    }
                    


                    // Check collision
                    Vector2[] tileBounds = this.GetTileBounds();

                    if (
                        tileBounds[0].X < this.position.X &&
                        tileBounds[0].Y < this.position.Y &&
                        tileBounds[1].X > this.position.X &&
                        tileBounds[1].Y > this.position.Y
                        )
                    {
                        // Check enemy's pathing list for itself
                        bool foundInPathList = false;
                        foreach (MovementTile MovementTile in Enemy.tilePathMoved)
                        {
                            if (MovementTile == this)
                            {
                                foundInPathList = true;
                            }
                        }
                        // Add to enemy's pathing list if not already in there
                        if (!foundInPathList)
                        {
                            Enemy.tilePathMoved.Add(this);
                        }

                        // Check if enemy has hit the middle yet
                        if (Vector2.DistanceSquared(Enemy.position, getCentre) <= 0)
                        {
                            Enemy.hitMid = true;
                        }

                        // Move it based on where it is within the tile
                        if (!Enemy.hitMid)
                        {
                            Enemy.SetDirection(getCentre); // Move to the centre of the tile
                        }
                        else
                        {
                            // Find position of adjacent tile that enemy hasnt been over
                            // Look in cardinal directions of current tile in playArea
                            for (int i = 0; i < 4; i++)
                            {
                                switch (i)
                                {
                                    case 0: // Above
                                        foreach (MovementTile MovementTile in Enemy.tilePathMoved)
                                        {
                                            if (Scene.playArea[(int)gridSpotXY.X][(int)gridSpotXY.Y - 1] == MovementTile)
                                            {
                                                Enemy.nextTile = MovementTile;
                                            }
                                        }
                                            
                                        break;
                                    case 1: // Right
                                        foreach (MovementTile MovementTile in Enemy.tilePathMoved)
                                        {
                                            if (Scene.playArea[(int)gridSpotXY.X+ 1][(int)gridSpotXY.Y] == MovementTile)
                                            {
                                                Enemy.nextTile = MovementTile;
                                            }
                                        }
                                            
                                        break;
                                    case 2: // Down
                                        foreach (MovementTile MovementTile in Enemy.tilePathMoved)
                                        {
                                            if (Scene.playArea[(int)gridSpotXY.X][(int)gridSpotXY.Y + 1] == MovementTile)
                                            {
                                                Enemy.nextTile = MovementTile;
                                            }
                                        }
                                            
                                        break;
                                    case 3: // Left
                                        foreach (MovementTile MovementTile in Enemy.tilePathMoved)
                                        {
                                            if (Scene.playArea[(int)gridSpotXY.X - 1][(int)gridSpotXY.Y] == MovementTile)
                                            {
                                                Enemy.nextTile = MovementTile;
                                            }
                                        }
                                            
                                        break;
                                }
                            }
                            // Move enemy towards that position
                            Enemy.SetDirection(Enemy.nextTile.GetCentreOnScreen());
                        }
                    }
                }
            }
        } */
        
    }
}
