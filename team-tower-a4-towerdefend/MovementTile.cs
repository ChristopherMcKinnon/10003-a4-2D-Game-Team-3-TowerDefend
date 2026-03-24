using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class MovementTile : TileEntity
    {
        string startSide;
        string endSide;
        public MovementTile(Scene setScene, string setStartSide, string setEndSide) : base(setScene)
        {
            this.sprite = Graphics.LoadTexture("..\\..\\..\\..\\..\\10003-a4-2D-Game-Team-3-TowerDefend\\team-tower-a4-towerdefend\\Assets\\MovementTile.png");
            this.startSide = setStartSide;
            this.endSide = setEndSide;
        }
        public override void StdDraw()
        {
            Graphics.Scale = 3;
            Graphics.Draw(sprite, this.position);
        }
    }
}
