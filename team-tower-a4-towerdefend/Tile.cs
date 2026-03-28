using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Tile : TileEntity
    {

        public Tile(Scene setScene) : base(setScene) // Entity requires Scene
        {
            this.sprite = Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\TileBase.png");
        }

        public override void Update()
        {
            
        }
        public override void StdDraw()
        {
            Graphics.Scale = Scene.graphicsSize;
            Graphics.Draw(sprite, this.position);
        }
    }
}
