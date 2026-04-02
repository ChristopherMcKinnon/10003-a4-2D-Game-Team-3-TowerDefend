using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Controls
    {
        Scene Scene;
        public Controls(Scene setScene)
        {
            this.Scene = setScene;
        }
        public void Update()
        {
            GameStateChanges();
            CheckTile();
        }
        public void CheckTile()
        {
            if (Input.IsMouseButtonPressed(MouseInput.Left))
            {
                Vector2 gridSpot = Scene.CheckMouseHoverTile();
                Console.Write(gridSpot);
                Scene.AddTower(new Tower(Scene), gridSpot);

                // Add sounds here
            }
        }

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
