using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class TileEntity : Entity
    {

        public Texture2D sprite;
        public TileEntity(Scene setScene) : base(setScene)
        {

        }
        public virtual Vector2 GetSize()
        {
            Graphics.Scale = 3;
            return sprite.Size * Graphics.Scale;
        }
    }
}
