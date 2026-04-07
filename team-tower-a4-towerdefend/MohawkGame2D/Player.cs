using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class Player : TileEntity
    {
        public float money;
        public float health;
        public Vector2 playerPos;

        public Player(Scene setScene) : base(setScene)
        {
            this.sprite = Scene.textures["TilePlayer"];
            this.money = 200f;
            this.health = 10;
            this.playerPos = new Vector2(1, 0);
            this.graphicsSize = Scene.graphicsSize;
        }


        public override void Update()
        {
            //Console.WriteLine($"Pos: {this.position}, Centre: {this.FindCentreOnScreen()}");
        }
        public override void StdDraw()
        {
            Graphics.Scale = this.graphicsSize;
            Graphics.Draw(sprite, this.position);
        }
        public void AddMoney(float money)
        {
            this.money += money;
        }
        public bool CheckDetractMoney(float money) // Returns true if it can detract
        {
            if (this.money-money >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DetractMoney(float money)
        {
            this.money -= money;
        }
        public void GetHit()
        {
            this.health -= 1;
            if (this.health <= 0)
            {
                Scene.GameEnd();
            }
        }
        

    }
}
