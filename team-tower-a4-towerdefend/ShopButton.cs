using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class ShopButton : Button
    {
        TowerEntity baseTower;
        KeyboardInput keyShortcut;
        public ShopButton(Scene setScene, TowerEntity setTower, float setCost, KeyboardInput setKeyShortcut) : base(setScene, setCost)
        {
            this.targetSprite = setTower.sprite;
            this.baseTower = setTower;
            this.keyShortcut = setKeyShortcut;
        }
        public override void Update()
        {
            Click();
        }
        public override void Click() // Can be overriden
        {
            // Check if mouse button clicked first
            if ((Input.IsMouseButtonPressed(MouseInput.Left) && CheckHover() || Input.IsKeyboardKeyPressed(keyShortcut)))
            {
                // Check cost
                if (Scene.Player.money >= this.baseTower.moneyCost)
                {
                    // Check if it has already been clicked, if so, disable it
                    if (Scene.selectedShopTower == baseTower)
                    {
                        Scene.selectedShopTower = null;
                    } else
                    {
                        Scene.selectedShopTower = baseTower;
                    }
                }
            }
        }
    }
}
