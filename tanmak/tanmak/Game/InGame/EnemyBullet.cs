using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;

namespace tanmak.Game
{
    public abstract class EnemyBullet : GameObject
    {
        public EnemyBullet(World world) : base(world)
        {
        }

        public int Damage { get; set; } = 1;
    }
}
