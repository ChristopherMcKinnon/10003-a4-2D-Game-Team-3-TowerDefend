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
        public float bulletSize;
        public float bulletSpeed;
        public float damage;
        public Enemy Target;
        public List<Enemy> withinRange = new List<Enemy>();
        public List<float> enemyDistances;
        public float shotCooldown;
        public float shotInterval;
        public Vector2 centre;

        public TowerEntity(Scene setScene, float setMoneyCost) : base(setScene)
        {
            this.moneyCost = setMoneyCost;
            this.Target = null;
            this.shotInterval = 0f;
            
        }
        public override void Update()
        {
            CheckInRadius();
            CheckNearest();
            Shoot();
        }
        public override void StdDraw()
        {
        }
        public virtual void Shoot()
        {
            shotInterval -= Time.DeltaTime;
            if (shotInterval <= 0)
            {
                if (this.Target != null)
                {
                    Scene.AddEntity(new Bullet(Scene, this, Target, damage, bulletSize, bulletSpeed));
                    shotInterval = shotCooldown;
                }
            }
        }
        public virtual void CheckInRadius() // If any enemies are within radius, then make sure they are in the list. If not, make sure they are not in the list
        {
            this.centre = this.FindCentreOnScreen();
            //Enemy currentClosestEnemy = null;
            //float Closestdistance = Vector2.Distance(this.centre, currentClosestEnemy.position);
            foreach (Entity Entity in Scene.entities)
            {
                if (Entity is Enemy Enemy)
                {
                    float distance = Vector2.Distance(this.centre, Enemy.position);
                    if (distance < shotRadius) // Check if enemy is within range
                    {
                        if (!withinRange.Contains(Enemy))
                        {
                            withinRange.Add(Enemy);
                        }
                    }
                    else
                    {
                        if (withinRange.Contains(Enemy))
                        {
                            withinRange.Remove(Enemy);
                        }
                    }
                }
            }
        }
        public virtual void CheckNearest()
        {
            if (withinRange.Count > 0)
            {
                Enemy closestEnemy = withinRange[0]; // Start at the first enemy
                foreach (Enemy Enemy in withinRange)
                {
                    // Compare distances between each enemy and find the closest
                    float closestEnemyDistance = Vector2.Distance(this.centre, closestEnemy.position);
                    float thisEnemyDistance = Vector2.Distance(this.centre, Enemy.position);
                    if (thisEnemyDistance < closestEnemyDistance)
                    {
                        closestEnemy = Enemy;
                    }
                }
                // Set the target to the closest enemy
                this.Target = closestEnemy;
            }
        }
        public virtual void CheckFurthest()
        {
            if (withinRange.Count > 0)
            {
                Enemy furthestEnemy = withinRange[0]; // Start at the first enemy
                foreach (Enemy Enemy in withinRange)
                {
                    // Compare distances between each enemy and find the furthest
                    float furthestEnemyDistance = Vector2.Distance(this.centre, furthestEnemy.position);
                    float thisEnemyDistance = Vector2.Distance(this.centre, Enemy.position);
                    if (thisEnemyDistance > furthestEnemyDistance)
                    {
                        furthestEnemy = Enemy;
                    }
                }
                // Set the target to the furthest enemy
                this.Target = furthestEnemy;
            }
        }
    }
}
