using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MohawkGame2D
{
    internal class Scene
    {

        // All variables that come innate with each scene
        public List<Entity> entities = new List<Entity>(); // Where all entities are referenced
        public List<Entity> addEntityQueue = new List<Entity>(); // Where all new entities are queued for addition
        public List<Entity> removeEntityQueue = new List<Entity>(); // Where all entities are queued for removal

        public Dictionary <string, Texture2D> textures;
        public Game Game;
        public Controls Controls;
        public Player Player;

        public bool gameOver;
        public bool gamePause;
        public bool drawGhostFlag;

        public int playAreaHeight;
        public int playAreaWidth;
        public TileEntity[][] playArea;
        public float shopWidth;
        public float shopIndentHeight;
        public Button[] shopButtons;

        public Vector2 mousePos;

        public int tileSize;
        public int gridTileSize;
        public int graphicsSize;
        

        public float spawnTimer;
        public float spawnInterval;


        // Initialize
        public Scene(Game setGame)
        {
            // Variables that must be initialized when creating a new scene
            this.Game = setGame;
            this.Controls = new Controls(this);

            // Load all textures
            textures = new Dictionary<string, Texture2D>()
            {
                {"TileBase", Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\TileBase.png")},
                {"TowerCommon", Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\TowerCommon.png")},
                {"TowerTrishot", Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\TowerTrishot.png")},
                {"TowerSniper", Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\TowerSniper.png")},
                {"MovementTile", Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\MovementTile.png")},
                {"TilePlayer", Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\TilePlayer.png")},
                {"EnemyCommon", Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\EnemyCommon.png")},
                {"Button", Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\Button.png")},
                {"ButtonCover", Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\ButtonCover.png")},
                {"Money", Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\Money.png")},

            };

            // Load all audio files

            // Set Player
            this.Player = new Player(this); // Player must be loaded after textures because it has a texture

            // Set play area
            playAreaWidth = 12;
            playAreaHeight = 12;
            playArea = new TileEntity[playAreaWidth][];
            shopWidth = 288; // The amount of space given between the edge of the screen and the play area
            shopIndentHeight = 192; // The amount of space between the top of the screen and the shop (shop exclusive indent)
            gridTileSize = 32; // Square
            graphicsSize = 3;
            for (int i = 0; i < playArea.Length; i++) // Init the nested arrays
            {
                playArea[i] = new TileEntity[playAreaHeight];
            }
            for (int x = 0; x < playArea.Length; x++) // Set each spot to tile
            {
                for(int y = 0; y < playArea[x].Length; y++)
                {
                    playArea[x][y] = new Tile(this);
                    AddEntity(playArea[x][y]);
                }
            }
            for (int x = 0; x < playArea.Length; x++) // Set position of each tile
            {
                for(int y = 0; y < playArea[x].Length; y++)
                {
                    TileEntity currentTile = playArea[x][y];
                    
                    playArea[x][y].position = FindTileScreenPosition(new Vector2(x, y));
                }

            }
            gameOver = false;
            gamePause = false;
            drawGhostFlag = false;

            // Spawner variables
            spawnTimer = 2.5f;
            spawnInterval = 0;

            // Init shop buttons
            tileSize = GetTileSize();
            shopButtons = [
                new ShopButton(this, textures["TowerCommon"], 50f),
                new ShopButton(this, textures["TowerTrishot"], 175f),
                new ShopButton(this, textures["TowerSniper"], 150f),
                ];
            // Set shop button variables 
            for (int i = 0; i < shopButtons.Length; i++)
            {

                // Set button variables
                shopButtons[i].position.Y = shopIndentHeight + (i * tileSize);
                AddEntity(shopButtons[i]);
                
            }


            SetMovementPaths();

        }
        public void Update() // Control all things within the scene
        {
            mousePos = Input.GetMousePosition(); // Set global mouse pos
            this.Controls.Update();


            if (!gamePause) // While the game is unpaused
            {
                // Update each entity (do not remove or add any new ones yet)
                foreach (Entity entity in entities)
                {
                    entity.Update();
                }
                    
                if (!gameOver) // While game is not over
                {

                    // Add new entities from entity queue
                    foreach (Entity entity in addEntityQueue.ToList())
                    {
                        entities.Add(entity);
                    }
                    addEntityQueue.Clear(); // This allows the entity removals to only reference the main entities list once every frame
                }

                // Remove entities from entity queue
                foreach (Entity entity in removeEntityQueue.ToList())
                {
                    entities.Remove(entity);
                }
                removeEntityQueue.Clear();

                SpawnEnemies();

                
                
                // UI Elements
                DrawShop();
                DrawMoney();
                
                //DrawEnemyCount();
            }

            // Game Pause
            if (gamePause)
            {
                DrawPauseGameUI();
            }

            // Draw each entity
            
            foreach (Entity entity in entities)
            {
                entity.StdDraw();
            }
            if (!gameOver)
            {
                for (int i = 0; i < shopButtons.Length; i++)
                {
                    CheckDrawGhost(shopButtons[i]);
                }
            }
            // Process Game End
            if (gameOver)
            {
                GameEnd();
            }


        }

        // Entity related
        public void AddEntity(Entity setEntity) // Add to addEntityQueue
        {
            addEntityQueue.Add(setEntity);
        }
        public void RemoveEntity(Entity setEntity)// Add to removeEntityQueue
        {
            removeEntityQueue.Add(setEntity);
        }
        public void SpawnEnemies()
        {
            // Reduce timer
            spawnInterval -= Time.DeltaTime;
            if (spawnInterval <= 0)
            {
                AddEntity(new Enemy(this));
                spawnInterval = spawnTimer; // Reset cooldown
            }
        }

        // Tile related
        public int GetTileSize()
        {
            return gridTileSize * graphicsSize;
        }
        public Vector2 GetTileSizeBounds() // Same as GetSize but with Vector2
        {
            return new Vector2(gridTileSize * graphicsSize, gridTileSize * graphicsSize);
        }
        public Vector2 GetTileCentre()
        {
            float size = GetTileSize();
            return new Vector2(size / 2, size / 2);
        }
        public Vector2 GetTileCentreScreen(TileEntity SetTileEntity)

        {
            float size = GetTileSize();
            return new Vector2(SetTileEntity.position.X + (size / 2), SetTileEntity.position.Y + (size / 2));
        }
        public Vector2[] GetTileBounds(TileEntity SetTileEntity)
        {
            return new Vector2[] { new Vector2(SetTileEntity.position.X, SetTileEntity.position.Y), new Vector2(SetTileEntity.position.X + gridTileSize, SetTileEntity.position.Y + gridTileSize) };
        }
        public Vector2 GetGridSpot(TileEntity SetTileEntity)
        {
            for (int x = 0; x < playArea.Length; x++)
            {
                for (int y = 0; y < playArea[x].Length; y++)
                {
                    TileEntity currentTile = playArea[x][y];
                    if (currentTile == SetTileEntity)
                    {
                        return new Vector2(x, y); // Return a Vector2 that gives the x and y of its spot in TileEntity playArea in Scene
                    }
                }
            }
            return new Vector2(0, 0); // This is in case of error
        }
        public Vector2 FindTileScreenPosition(Vector2 gridSpot) // Turns a grid position into screen coordinates
        {
            Vector2 tileSizeBounds = GetTileSizeBounds();
            return new Vector2(gridSpot.X * tileSizeBounds.X + shopWidth, gridSpot.Y * tileSizeBounds.Y);
        }
        public bool CheckTileOccupied(Vector2 gridSpot)
        {
            if (CheckInBounds(gridSpot)){
                
                if (playArea[(int)gridSpot.X][(int)gridSpot.Y] is TowerCommon || playArea[(int)gridSpot.X][(int)gridSpot.Y] is TileMovement)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        public bool CheckInBounds(Vector2 position)
        {
            int tileSize = GetTileSize();
            // Convert play area from screen coordinates to small coordinates
            int indexX = (int)((position.X - shopWidth) / tileSize);
            int indexY = (int)position.Y / tileSize;
            if (indexX >= 0 && indexX < playAreaWidth && indexY >= 0 && indexY < playAreaHeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Vector2 FindGridSpot(Vector2 position)
        {
            int tileSize = GetTileSize();
            if (CheckInBounds(position))
            {
                return new Vector2((int)((position.X - shopWidth) / tileSize), (int)position.Y / tileSize);
            } else
            {
                return new Vector2(0, 0); // In case of failure
            }
        }
        public void AddTower(TileEntity TowerType, Vector2 gridSpot)
        {
            if (!CheckTileOccupied(gridSpot))
            {
                RemoveEntity(playArea[(int)gridSpot.X][(int)gridSpot.Y]); // First remove tile
                playArea[(int)gridSpot.X][(int)gridSpot.Y] = new TowerCommon(this);
                TileEntity currentSpot = playArea[(int)gridSpot.X][(int)gridSpot.Y]; // Set reference to what tile entity the loop is on
                playArea[(int)gridSpot.X][(int)gridSpot.Y].position = FindTileScreenPosition(gridSpot);

                AddEntity(playArea[(int)gridSpot.X][(int)gridSpot.Y]);
            } else
            {
                Console.WriteLine("There is already a tower there!");
            }
        }
        public void SetMovementPaths() // Not finished
        {
            Vector2[] moveTileMap = [// Ordered in rows
                new Vector2(11, 0), 
                new Vector2(0,1), new Vector2(1,1), new Vector2(2,1), new Vector2(3,1), new Vector2(4,1), new Vector2(5,1), new Vector2(6,1), new Vector2(7,1), new Vector2(8,1), new Vector2(9,1), /*new Vector2(10,1) TEMP REMOVE ,*/ new Vector2(11,1),
                new Vector2(0,2), new Vector2(11,2),
                new Vector2(0,3), new Vector2(1,3), new Vector2(2,3), new Vector2(3,3), new Vector2(8,3), new Vector2(9,3), new Vector2(11,3),
                new Vector2(9,4), new Vector2(11,4),
                new Vector2(0,5), new Vector2(1,5), new Vector2(2,5), new Vector2(9,5), new Vector2(11,5),
                new Vector2(0,6), new Vector2(2,6), new Vector2(9,6), new Vector2(10,6), new Vector2(11,6),
                new Vector2(0,7), new Vector2(2,7),
                new Vector2(0,8), new Vector2(2,8), new Vector2(3,8), new Vector2(8,8), new Vector2(9,8), new Vector2(10,8), new Vector2(11,8),
                new Vector2(0,9), new Vector2(11,9),
                new Vector2(0,10), new Vector2(1,10),new Vector2(2,10),new Vector2(3,10),new Vector2(4,10),new Vector2(5,10),new Vector2(6,10),new Vector2(7,10),new Vector2(8,10),new Vector2(9,10),new Vector2(10,10),new Vector2(11,10),
                new Vector2(0,11)
            ];
            for (int i = 0; i < moveTileMap.Length; i++)
            {
                RemoveEntity(playArea[(int)moveTileMap[i].X][(int)moveTileMap[i].Y]); // Remove all tile entities for replacement
                playArea[(int)moveTileMap[i].X][(int)moveTileMap[i].Y] = new TileMovement(this); // Set the movement tile to grid
                playArea[(int)moveTileMap[i].X][(int)moveTileMap[i].Y].position = FindTileScreenPosition(moveTileMap[i]); // Update position
                AddEntity(playArea[(int)moveTileMap[i].X][(int)moveTileMap[i].Y]); // Officially add the entity to the entity list
            }

            // Set the spawn tiles    
        }
        public Vector2 CheckMouseHoverTile()
        {

            for (int x = 0; x < playArea.Length; x++)
            {
                for (int y = 0; y < playArea[x].Length; y++)
                {
                    Vector2 currentTile = playArea[x][y].position;
                    Vector2 currentTileSize = GetTileSizeBounds();

                    // Check if mouse pos is within the current tile borders
                    if (mousePos.X >= currentTile.X && mousePos.Y >= currentTile.Y && mousePos.X < currentTile.X + currentTileSize.X && mousePos.Y < currentTile.Y + currentTileSize.Y)
                    {
                        return new Vector2(x, y); // Return the grid spot
                    }
                }
            }
            return new Vector2(-1, -1); // This is in case of an error

        }
        public void CheckDrawGhost(Button Button)
        {
            if (drawGhostFlag == true)
            {
                DrawGhost(Button);
            }
        }
        public void DrawGhost(Button Button)
        {
            Vector2 middle = GetTileSizeBounds() / 2;
            Graphics.Scale = graphicsSize;

            // If not in play area, follow the mouse
            if (!CheckInBounds(mousePos))
            {
                Graphics.Draw(Button.sprite, mousePos - middle);
                Graphics.Draw(Button.targetSprite, mousePos - middle);

            }
            // If in play area, orient to the grid spot its on
            else
            {
                Vector2 ghostGridSpot = FindGridSpot(mousePos);
                Vector2 ghostGridPos = FindTileScreenPosition(ghostGridSpot);
                if (!CheckTileOccupied(ghostGridSpot))
                {
                    Graphics.Draw(Button.sprite, ghostGridPos);
                    Graphics.Draw(Button.targetSprite, ghostGridPos);
                }
                
            }
            // If not on grid:

            // If on grid:

        }
        // Game ending
        public void GameEnd() // Should always be called within the update loop
        {
            if (!gameOver) // Only runs once at the end of the game
            {
                gameOver = true; // Permanently set to true past this point
            }

            // Add all entities to the remove queue (this happens every frame after gameOver set to true in this case (considering Update();)
            foreach (Entity entity in entities)
            {
                RemoveEntity(entity);
            }
        }

        // UI related
        public void DrawEnemyCount()
        {
            int counter = 0;
            foreach (Entity Entity in entities)
            {
                if (Entity is Enemy Enemy)
                {
                    counter++;
                }
            }
            Text.Draw($"{counter}", new Vector2(0, 0));
        }
        public void DrawMoney()
        {
            Graphics.Draw(textures["Money"], new Vector2(0, 0));
            Text.Draw($"{Player.money}", new Vector2(50, 50));
        }
        public void DrawShop()
        {
            for (int i = 0; i < shopButtons.Length; i++)
            {

                // Set shop button text
                Text.Draw($"${shopButtons[i].moneyCost}", new Vector2(100, shopIndentHeight + tileSize/3 + (i * tileSize)));

            }
        }
        public void tempShop()
        {
            Console.WriteLine("you clicked it ");
        }

        // Pausing
        public void GamePause() // Set flag for pausing game in update loop
        {
            gamePause = true;
        }
        public void GameUnpause() // Set flag for when in 
        {
            gamePause = false;
        }
        public void DrawPauseGameUI()
        {
            Text.Draw("The game is paused now!", Game.windowCentre);
        }

    }
    
}
