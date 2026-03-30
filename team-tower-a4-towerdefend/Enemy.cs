using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Enemy : Entity
    {
        public float moveSpeed;
        public float health;
        public float healthFactor;
        public float moneyReward;
        public Vector2 direction;
        public Vector2 velocity;
        public Vector2 spawnPos;
        public List<TileMovement> tilePathMoved;
        public TileMovement nextTile;
        public TileMovement currentTile;
        public Vector2 currentTileGridSpot;
        public float graphicsSize;
        public Vector2 centrePos;

        public bool hitMid;

        public Enemy(Scene setScene) : base(setScene)
        {
            tilePathMoved = new List<TileMovement>(); // All tiles previously moved on (touched)
            hitMid = false; // Flag for hitting the middle of the movement square
            graphicsSize = 2f;
            this.sprite = Scene.textures["EnemyCommon"];
            this.healthFactor = 1f;
            this.health = 10f * healthFactor;
            this.moveSpeed = 40f;
            this.velocity = new Vector2(moveSpeed, moveSpeed); // Moves at the same pace in all cardinal directions
            this.direction = new Vector2(-1, 1);

            if (Scene.playArea[11][1] is TileMovement MovementTile)
            {
                nextTile = MovementTile;
            }

            Spawn();
        }
        public override void Update()
        {
            CheckCurrentTile();
            CheckNextTile();
            Move();
        }
        public override void StdDraw()
        {
            Vector2 size = GetSize();
            Graphics.Scale = graphicsSize;
            Graphics.Draw(sprite, this.centrePos);
        }
        public Vector2 GetSize()
        {
            return sprite.Size * graphicsSize;
        }
        public void Spawn()
        {
            this.position = Scene.GetTileCentreScreen(Scene.playArea[11][0]); // Temp set spawn to top right
            this.centrePos = position - GetSize()/2;


        }
        public void Move()
        {
            SetDirection(Vector2.Normalize(Scene.GetTileCentreScreen(nextTile) - this.position));
            this.position += direction * velocity * Time.DeltaTime;
            this.centrePos = position - GetSize() / 2;
        }
        public void SetDirection(Vector2 setDirection)
        {
            this.direction = setDirection;
        }
        public void GetHit()
        {
            if (health <= 0)
            {
                Scene.RemoveEntity(this);
                Scene.Player.AddMoney(moneyReward);
            }
        }
        public void CheckCurrentTile()
        {

            // Get the position of the enemy and divide it by the tile size to get integer that represents tile position
            // Check if array index is not out of bounds
            if (Scene.CheckInBounds(this.position))
            {
                currentTileGridSpot = Scene.FindGridSpot(this.position);
                TileEntity Tile = Scene.playArea[(int)currentTileGridSpot.X][(int)currentTileGridSpot.Y];

                if (Tile is TileMovement MovementTile)
                {
                    currentTile = MovementTile;


                }
                if (tilePathMoved.Contains(currentTile)) // Check if in enemy's path list
                {

                }
                else
                {
                    tilePathMoved.Add(currentTile);
                }
            }
        }
        public void CheckNextTile()
        {
            TileMovement nextPossibleTile;

            // Check all 4 cardinal directions for movementTile

            int indexX = 0;
            int indexY = 0;
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0: // Above

                        indexX = (int)currentTileGridSpot.X;
                        indexY = (int)currentTileGridSpot.Y - 1;
                        // Check if array index is not out of bounds
                        if (indexX >= 0 && indexX < Scene.playAreaWidth && indexY >= 0 && indexY < Scene.playAreaHeight)
                        {
                            if (Scene.playArea[indexX][indexY] is TileMovement MoveTile)
                            {
                                // Check if this cardinal tile has already been crossed over, if not then make it the next tile
                                if (!tilePathMoved.Contains(MoveTile))
                                {
                                    nextTile = MoveTile;
                                }
                            }
                        }

                        break;
                    case 1: // Right
                        indexX = (int)currentTileGridSpot.X + 1;
                        indexY = (int)currentTileGridSpot.Y;
                        // Check if array index is not out of bounds
                        if (indexX >= 0 && indexX < Scene.playAreaWidth && indexY >= 0 && indexY < Scene.playAreaHeight)
                        {
                            if (Scene.playArea[indexX][indexY] is TileMovement MoveTile)
                            {
                                // Check if this cardinal tile has already been crossed over, if not then make it the next tile
                                if (!tilePathMoved.Contains(MoveTile))
                                {
                                    nextTile = MoveTile;
                                }
                            }
                        }

                        break;
                    case 2: // Down
                        indexX = (int)currentTileGridSpot.X;
                        indexY = (int)currentTileGridSpot.Y + 1;
                        // Check if array index is not out of bounds
                        if (indexX >= 0 && indexX < Scene.playAreaWidth && indexY >= 0 && indexY < Scene.playAreaHeight)
                        {
                            if (Scene.playArea[indexX][indexY] is TileMovement MoveTile)
                            {
                                // Check if this cardinal tile has already been crossed over, if not then make it the next tile
                                if (!tilePathMoved.Contains(MoveTile))
                                {
                                    nextTile = MoveTile;
                                }
                            }
                        }


                        break;
                    case 3: // Left
                        indexX = (int)currentTileGridSpot.X - 1;
                        indexY = (int)currentTileGridSpot.Y;
                        // Check if array index is not out of bounds
                        if (indexX >= 0 && indexX < Scene.playAreaWidth && indexY >= 0 && indexY < Scene.playAreaHeight)
                        {
                            if (Scene.playArea[indexX][indexY] is TileMovement MoveTile)
                            {
                                // Check if this cardinal tile has already been crossed over, if not then make it the next tile
                                if (!tilePathMoved.Contains(MoveTile))
                                {
                                    nextTile = MoveTile;
                                }
                            }
                        }

                        break;
                }
            }

        }
    }
}
