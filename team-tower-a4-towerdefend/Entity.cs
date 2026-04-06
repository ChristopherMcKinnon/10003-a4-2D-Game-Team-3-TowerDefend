using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Entity
    {
        public Scene Scene;
        public Texture2D sprite;
        public Vector2 position;
        public float graphicsSize;

        // All entities (subclasses of Entity) require : base(setScene)
        public Entity(Scene setScene)
        {
            Scene = setScene;
        }
        public virtual void Update()
        {

        }
        public virtual void StdDraw() // "Standard draw"
        {

        }
        public Vector2 GetSize()
        {
            return this.sprite.Size * this.graphicsSize;
        }
        public Vector2 FindCentreOnScreen()
        {
            return this.position + FindCentre();
        }
        public Vector2 FindCentre()
        {
            return GetSize() / 2;
        }
    }
}
