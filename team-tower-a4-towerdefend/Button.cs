using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Button : Entity
    {

        Vector2 scale;
        public Texture2D targetSprite;
        public Texture2D coverSprite;
        public Vector2[] buttonBounds;
        public float moneyCost;
        Vector2 mousePos;


        public Button(Scene setScene, float setCost) : base(setScene)
        {
            this.moneyCost = setCost;
            this.scale = Scene.GetTileSizeBounds();
            sprite = Scene.textures["Button"];
            coverSprite = Scene.textures["ButtonCover"];
        }
        public override void Update()
        {
            Click();
        }
        public override void StdDraw()
        {            

            // Inner Graphic
            Graphics.Draw(targetSprite, position);

            // Button Graphic
            Graphics.Draw(sprite, position);

            DrawCostOverlay();
        }
        public virtual void Click() // Can be overriden
        {
            // Check if mouse button clicked first
            if (Input.IsMouseButtonPressed(MouseInput.Left))
            {
                // Check if mouse is actually on the button
                if (CheckHover())
                {
                }
            }
        }
        public void DrawCostOverlay()
        {
            // Check cost
            if (Scene.Player.money < moneyCost)
            {
                // Draw additional sprite for not enough money
                Graphics.Draw(coverSprite, position);
            }
        }
        public bool CheckHover() // Returns true if mouse is over the button and is not null
        {
            this.buttonBounds = GetButtonBounds(this);
            Vector2 getMouse = Scene.mousePos;
            if (getMouse != null)
            {
                // Mouse does not hover over:
                if (getMouse.X < this.buttonBounds[0].X ||
                    getMouse.Y < this.buttonBounds[0].Y ||
                    getMouse.X > this.buttonBounds[1].X ||
                    getMouse.Y > this.buttonBounds[1].Y
                    )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public Vector2[] GetButtonBounds(Button Button) // Returns top left and bottom right positions
        {
            return [new Vector2(this.position.X, this.position.Y), new Vector2(this.position.X + this.scale.X, this.position.Y + this.scale.Y)];
        }
    }
}
