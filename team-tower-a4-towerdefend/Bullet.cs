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
        
        public float damage;
        public Bullet(Scene setScene, Entity setOwner, Enemy setTarget) : base(setScene)
        {
            this.sprite = Scene.textures["Bullet"];
            this.moveSpeed = 400f;
            this.Owner = setOwner;
            this.Target = setTarget;
            this.position = Owner.position;
            this.direction = Vector2.Normalize(Target.position - position);
            this.velocity = this.direction * moveSpeed;
        }
        public override void Update()
        {
            Move();
            CheckHit();
        }
        public override void StdDraw()
        {
            Graphics.Scale = 5;
            Graphics.Draw(sprite, position);
        }
        public void Move()
        {
            this.position += velocity * Time.DeltaTime;
            //Console.WriteLine(this.direction);
        }
        public void CheckHit()
        {
            if (Vector2.DistanceSquared(this.position, Target.FindCentreOnScreen())<= Target.FindCentre().X)
            {
                Scene.RemoveEntity(this);
            }
        }
        public void HandleOutOfBounds()
        {
            
        }
    }
}
