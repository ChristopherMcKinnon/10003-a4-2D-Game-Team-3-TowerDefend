// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    public class Game
    {
        // Place your variables here:
        public Vector2 windowSize;
        public Vector2 windowCentre;
        public Color selectedBGC;
        public Color baseBGC;
        public Color towerPlaceBGC;
        string windowTitle;
        int windowFPS;
        Scene Scene;

        public void Setup()
        {

            // CONFIG
            windowSize = new Vector2(512*3, 512*3);
            windowCentre = new Vector2(windowSize.X / 2, windowSize.Y / 2);

            windowTitle = "Tower Defend Game By Christopher, Jose, and Olsen";
            windowFPS = 60;

            baseBGC = new Color(50, 50, 100);
            towerPlaceBGC = new Color(50, 0, 50);
            selectedBGC = baseBGC;
            // Window

            Window.SetSize((int)windowSize.X, (int)windowSize.Y);
            Window.SetTitle(windowTitle);
            Window.TargetFPS = windowFPS;
            Scene = new Scene(this);
        }

        public void Update()
        {
            Window.ClearBackground(selectedBGC);
            Scene.Update();
        }
    }

}
