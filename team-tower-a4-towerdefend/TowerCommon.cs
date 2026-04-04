using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class TowerCommon : TowerEntity
    {

        public TowerCommon(Scene setScene, float setMoneyCost) : base(setScene, setMoneyCost) // Entity requires Scene
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
