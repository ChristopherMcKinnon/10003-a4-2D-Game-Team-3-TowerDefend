using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Bullet : Entity
    {

        public Entity Owner;
        public Enemy Target;
        public Vector2 velocity;
        public Vector2 direction;
        public float moveSpeed;
        public float graphicsSize;
        public float damage;
        public Bullet(Scene setScene, Entity setOwner, Enemy setTarget, float setDamage, float setBulletSize, float setBulletSpeed) : base(setScene)
        {
            this.sprite = Scene.textures["Bullet"];
            this.damage = setDamage;
            this.moveSpeed = setBulletSpeed;
            this.Owner = setOwner;
            this.Target = setTarget;
            this.position = Owner.FindCentreOnScreen();
            this.direction = Vector2.Normalize(Target.position - position);
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
            this.position += velocity * Time.DeltaTime;
            //Console.WriteLine(this.direction);
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
