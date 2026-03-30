using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class EnemyTank : Enemy
    {
        public EnemyTank(Scene setScene) : base(setScene)
        {
            this.moveSpeed = 4f;
            this.moneyReward = 10f;
        } 
    }
}
