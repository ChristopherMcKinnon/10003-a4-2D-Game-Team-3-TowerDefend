using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class TowerTrishot : TowerEntity
    {

        public TowerTrishot(Scene setScene, float setMoneyCost) : base(setScene, setMoneyCost) // Entity requires Scene
        {
            this.sprite = Scene.textures["TowerTrishot"];
            this.shotRadius = 250f;
            this.bulletSize = 5f;
            this.damage = 1.5f;
            this.shotCooldown = 0.75f;
        }

        public override void StdDraw()
        {
            Graphics.Scale = Scene.graphicsSize;
            Graphics.Draw(sprite, this.position);
        }
    }
}
