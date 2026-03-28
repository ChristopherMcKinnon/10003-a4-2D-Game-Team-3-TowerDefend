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
    }
}
