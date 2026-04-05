using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class TowerEntity : TileEntity
    {
        public float moneyCost;
        public float shotRadius;
        public Enemy Target;
        public List<Enemy> withinRange = new List<Enemy>();
        public List<float> enemyDistances;
        public float shotCooldown;
        public float shotInterval;

        public TowerEntity(Scene setScene, float setMoneyCost) : base(setScene)
        {
            this.moneyCost = setMoneyCost;
            this.shotRadius = 500f;
            this.Target = null;
            this.shotCooldown = 0.25f;
            this.shotInterval = 0f;
        }
        public override void Update()
        {
            CheckInRadius();
            Shoot();
        }
        public override void StdDraw()
        {
            Draw.Circle(position, shotRadius);
        }
        public void Shoot()
        {
            shotInterval -= Time.DeltaTime;
            if (shotInterval <= 0)
            {
                if (withinRange != null)
                {
                    if (withinRange.Count > 0)
                    {
                        Scene.AddEntity(new Bullet(Scene, this, Target));
                        shotInterval = shotCooldown;
                    }
                }
            }
        }
        public void CheckInRadius() // If any enemies are within radius, then make sure they are in the list. If not, make sure they are not in the list
        {
            float closest = shotRadius * shotRadius;
            foreach (Entity Entity in Scene.entities)
            {
                if (Entity is Enemy Enemy)
                {
                    float distance = Vector2.DistanceSquared(this.position, Enemy.FindCentreOnScreen());
                    if (distance < shotRadius * shotRadius)
                    {
                        if (!withinRange.Contains(Enemy))
                        {
                            withinRange.Add(Enemy);
                        }
                        // Check closest enemy
                        if (distance < closest)
                        {
                            closest = distance;
                            this.Target = Enemy;
                        }
                    }
                    else
                    {
                        if (withinRange != null)
                        {
                            if (withinRange.Contains(Enemy))
                            {
                                withinRange.Remove(Enemy);
                            }
                        }
                    }
                }
            }
        }
    }
}
