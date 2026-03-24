using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Tower : TileEntity
    {

        public Tower(Scene setScene) : base(setScene) // Entity requires Scene
        {
            this.sprite = Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\TowerCommon.png");
        }

        public override void StdDraw()
        {
            Graphics.Scale = 3;
            Graphics.Draw(sprite, this.position);
        }
    }
}
