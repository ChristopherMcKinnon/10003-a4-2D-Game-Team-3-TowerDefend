using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Controls
    {
        Scene Scene;
        Vector2 mousePos;
        public Controls(Scene setScene)
        {
            this.Scene = setScene;
            
        }
        public void Update()
        {
            this.mousePos = Scene.mousePos;
            GameStateChanges();
            CheckPlaceTower();
            CheckTile();
        }
        public void CheckTile()
        {
            if (Input.IsMouseButtonPressed(MouseInput.Left))
            {
                Vector2 gridSpot = Scene.CheckMouseHoverTile();
                Console.Write(gridSpot);
                //Scene.AddTower(new TowerCommon(Scene), gridSpot);
            }
        }
        public void CheckPlaceTower()
        {
            if (Input.IsMouseButtonPressed(MouseInput.Left))
            {
                Vector2 gridSpot = Scene.FindGridSpot(mousePos);
                if (
                    Scene.selectedShopTower != null &&
                    Scene.CheckInBounds(mousePos) &&
                    !Scene.CheckTileOccupied(gridSpot))
                {
                    float placeTowerCost = Scene.selectedShopTower.moneyCost;
                    if (Scene.Player.CheckDetractMoney(placeTowerCost))
                    {
                        // Instantiate a copy of the tower
                        if (Scene.selectedShopTower is TowerCommon) // Common Tower
                        {
                            Scene.ReplaceTile(new TowerCommon(Scene, Scene.towerCommonCost), gridSpot);
                            Scene.Player.DetractMoney(placeTowerCost);
                            if (!Scene.Player.CheckDetractMoney(placeTowerCost))
                            {
                                Scene.selectedShopTower = null;
                            }
                        }
                        if (Scene.selectedShopTower is TowerTrishot) // Trishot Tower
                        {
                            Scene.ReplaceTile(new TowerTrishot(Scene, Scene.towerTrishotCost), gridSpot);
                            Scene.Player.DetractMoney(placeTowerCost);
                            if (!Scene.Player.CheckDetractMoney(placeTowerCost))
                            {
                                Scene.selectedShopTower = null;
                            }
                        }
                        if (Scene.selectedShopTower is TowerSniper) // Sniper Tower
                        {
                            Scene.ReplaceTile(new TowerSniper(Scene, Scene.towerSniperCost), gridSpot);
                            Scene.Player.DetractMoney(placeTowerCost);
                            if (!Scene.Player.CheckDetractMoney(placeTowerCost))
                            {
                                Scene.selectedShopTower = null;
                            }
                        }
                    } else
                    {
                        Console.WriteLine("You outta money dawg");
                        Scene.selectedShopTower = null;
                    }
                }
            }
        }
        // Game Logic
        public void GameStateChanges()
        {
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Space))
            {
                if (!Scene.gamePause)
                {
                    Scene.GamePause();
                }
                else
                {
                    Scene.GameUnpause();
                }
            }
        }
        
    }
}
