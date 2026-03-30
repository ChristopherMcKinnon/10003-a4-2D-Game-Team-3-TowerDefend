using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class TowerCommon : TileEntity
    {

        public TowerCommon(Scene setScene) : base(setScene) // Entity requires Scene
        {
            this.sprite = Scene.textures["TowerCommon"];
        }

        public override void StdDraw()
        {
            Graphics.Scale = Scene.graphicsSize;
            Graphics.Draw(sprite, this.position);
        }
    }
}
