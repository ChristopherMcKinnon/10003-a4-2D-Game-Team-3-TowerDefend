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
            this.health = 2;
            this.playerPos = new Vector2(1, 0);
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
