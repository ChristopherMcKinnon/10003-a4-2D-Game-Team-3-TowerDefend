using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Bullet : Entity
    {

        public TowerEntity Owner;
        public Enemy Target;
        public Vector2 velocity;
        public Vector2 direction;
        public float moveSpeed;
        public float graphicsSize;
        public float damage;
        public float angleRadians;
        public Bullet(Scene setScene, TowerEntity setOwner, Enemy setTarget, float setDamage, float setBulletSize, float setBulletSpeed, float setAngleRadians) : base(setScene)
        {
            this.sprite = Scene.textures["Bullet"];
            this.damage = setDamage;
            this.moveSpeed = setBulletSpeed;
            this.Owner = setOwner;
            this.Target = setTarget;
            this.position = Owner.FindCentreOnScreen();
            this.angleRadians = setAngleRadians;
            this.direction = Vector2.Normalize(this.Target.position - this.position); // First step
            this.direction = new Vector2(this.direction.X + (float)Math.Cos(angleRadians), this.direction.Y + (float)Math.Sin(angleRadians));
            this.velocity = this.direction * moveSpeed;
            this.graphicsSize = setBulletSize;
        }
        public override void Update()
        {
            Move();
            CheckHit();
        }
        public override void StdDraw()
        {
            Graphics.Scale = graphicsSize;
            Graphics.Draw(sprite, this.position - this.FindCentre());
        }
        public void Move()
        {
            if (this.Target != null && Scene.entities.Contains(this.Target) && !Scene.removeEntityQueue.Contains(this.Target))
            {
                this.direction = Vector2.Normalize(this.Target.position - this.position);
                this.velocity = this.direction * moveSpeed;
                this.position += velocity * Time.DeltaTime;
            }
            else
            {
                this.velocity = this.direction * moveSpeed;
                this.position += velocity * Time.DeltaTime;
            }
        }
        public void CheckHit()
        {
            float targetCentre = Target.FindCentre().X; // Assumes square
            if (Vector2.DistanceSquared(this.position, Target.position) <= targetCentre*targetCentre)
            {
                Scene.RemoveEntity(this);
                Target.GetHit(this.damage);
            }
        }
        public void HandleOutOfBounds()
        {
            
        }
        
    }
}
