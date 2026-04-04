using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D
{
    internal class TowerEntity : TileEntity
    {
        public float moneyCost;

        public TowerEntity(Scene setScene, float setMoneyCost) : base(setScene)
        {
            this.moneyCost = setMoneyCost;
        }
    }
}
