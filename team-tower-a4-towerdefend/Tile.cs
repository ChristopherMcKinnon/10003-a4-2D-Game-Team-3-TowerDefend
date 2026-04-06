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
            this.sprite = Scene.textures["TileBase"];
        }

        public override void Update()
        {
            
        }
        public override void StdDraw()
        {
            Graphics.Scale = graphicsSize;
            Graphics.Draw(sprite, this.position);
        }
    }
}
