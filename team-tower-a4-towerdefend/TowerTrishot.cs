using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class TowerTrishot : TileEntity
    {

        public TowerTrishot(Scene setScene) : base(setScene) // Entity requires Scene
        {
            this.sprite = Scene.textures["TowerTrishot"];
        }

        public override void StdDraw()
        {
            Graphics.Scale = Scene.graphicsSize;
            Graphics.Draw(sprite, this.position);
        }
    }
}
