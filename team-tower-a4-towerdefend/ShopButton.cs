using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class ShopButton : Button
    {
        bool drawGhostFlag;
        public ShopButton(Scene setScene, Texture2D setTargetSprite, Action setButtonAction, float setCost) : base(setScene, setTargetSprite, setButtonAction)
        {
            this.moneyCost = setCost;
            this.drawGhostFlag = false;
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
                    buttonAction();
                }
            }
        }


    }
}
