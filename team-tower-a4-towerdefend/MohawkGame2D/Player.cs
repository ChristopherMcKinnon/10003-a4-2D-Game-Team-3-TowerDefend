using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Player : TileEntity
    {
        public float money;

        public Player(Scene setScene) : base(setScene)
        {
            this.sprite = Scene.textures["TilePlayer"];
            this.money = 50f;
        }


        public override void Update()
        {

        }
        public override void StdDraw()
        {
            Graphics.Scale = Scene.graphicsSize;
            Graphics.Draw(sprite, this.position);
        }
        public void AddMoney(float money)
        {
            this.money += money;
        }


    }
}
