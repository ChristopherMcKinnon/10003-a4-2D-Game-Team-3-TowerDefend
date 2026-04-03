using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class ShopButton : Button
    {
        public ShopButton(Scene setScene, Texture2D setTargetSprite, float setCost) : base(setScene, setTargetSprite)
        {
            this.moneyCost = setCost;
        }
        public override void Update()
        {
            Click();
        }
        public override void Click() // Can be overriden
        {
            // Check if mouse button clicked first
            if (Input.IsMouseButtonPressed(MouseInput.Left))
            {
                // Check if mouse is actually on the button
                if (CheckHover())
                {
                    // Check cost
                    if (Scene.Player.money >= this.moneyCost)
                    {
                        // Check if Scene.drawGhostFlag is true
                        if (!Scene.drawGhostFlag)
                        {
                            Scene.drawGhostFlag = true;
                        }
                    }
                    
                }
            }
        }


    }
}
