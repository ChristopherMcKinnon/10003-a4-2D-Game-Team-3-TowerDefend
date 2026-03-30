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
        bool drawGhostFlag;
        public Controls(Scene setScene)
        {
            this.Scene = setScene;
            this.drawGhostFlag = false;
        }
        public void Update()
        {
            this.mousePos = Scene.mousePos;
            GameStateChanges();
            CheckTile();
        }
        public void CheckTile()
        {
            if (Input.IsMouseButtonPressed(MouseInput.Left))
            {
                Vector2 gridSpot = Scene.CheckMouseHoverTile();
                Console.Write(gridSpot);
                Scene.AddTower(new TowerCommon(Scene), gridSpot);
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
        public void CheckDrawGhost()
        {
            if (drawGhostFlag == true)
            {
                DrawGhost();
            }
        }
        public void DrawGhost()
        {/*
            Vector2 middle = Scene.GetTileSizeBounds() / 2;
            Vector2 mousePos = Scene.mousePos;
            Graphics.Draw(sprite, mousePos - middle);
            Graphics.Draw(targetSprite, mousePos - middle);
            */
        }
    }
}
