using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class EnemyCommon : Enemy
    {
        public EnemyCommon(Scene setScene) : base(setScene)
        {
            this.moveSpeed = 10f;
            this.moneyReward = 10f;
        }
    }
}
