using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class TowerSniper : TowerEntity
    {

        public TowerSniper(Scene setScene, float setMoneyCost) : base(setScene, setMoneyCost) // Entity requires Scene
        {
            this.sprite = Scene.textures["TowerSniper"]; 
            this.shotRadius = 1000f;
            this.bulletSize = 15f;
            this.bulletSpeed = 1500f;
            this.damage = 5f;
            this.shotCooldown = 2.5f;
        }
        
        public override void StdDraw()
        {
            Graphics.Scale = Scene.graphicsSize;
            Graphics.Draw(sprite, this.position);
        }
    }
}
