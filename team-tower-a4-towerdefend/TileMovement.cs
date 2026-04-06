using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class TileMovement : TileEntity
    {
        public TileMovement(Scene setScene) : base(setScene)
        {
            this.sprite = Scene.textures["MovementTile"];
        }
        public override void Update()
        {
        }
        public override void StdDraw()
        {
            Graphics.Scale = this.graphicsSize;
            Graphics.Draw(sprite, this.position);
        }
        
    }
}
