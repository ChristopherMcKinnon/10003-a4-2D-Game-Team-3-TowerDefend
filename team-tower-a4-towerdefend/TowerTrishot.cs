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
            this.bulletSpeed = 1000f;
            this.damage = 1.5f;
            this.shotCooldown = 0.75f;
            this.shotSpreadAngle = 90f;
        }
        public override void Update()
        {
            CheckInRadius();
            CheckNearest();
            Shoot();
        }
        public override void StdDraw()
        {
            Graphics.Scale = Scene.graphicsSize;
            Graphics.Draw(sprite, this.position);
        }
        public override void Shoot()
        {
            this.shotSpreadRadians = this.shotSpreadAngle * (float)(Math.PI * 180f);
            shotInterval -= Time.DeltaTime;
            if (shotInterval <= 0)
            {
                if (this.Target != null)
                {
                    Scene.AddEntity(new Bullet(Scene, this, Target, damage, bulletSize, bulletSpeed, 0f));
                    Scene.AddEntity(new Bullet(Scene, this, Target, damage, bulletSize, bulletSpeed, shotSpreadRadians));
                    Scene.AddEntity(new Bullet(Scene, this, Target, damage, bulletSize, bulletSpeed, shotSpreadRadians));
                    shotInterval = shotCooldown;
                }
            }
        }
    }
}
