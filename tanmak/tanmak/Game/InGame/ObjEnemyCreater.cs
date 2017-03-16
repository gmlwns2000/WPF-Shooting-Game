using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;

namespace tanmak.Game
{
    public class ObjEnemyCreater : GameObject
    {
        public ObjEnemyCreater(World world, ObjPlayer player) : base(world)
        {
            world.AddObject(new ObjEnemyOne(world, player));
        }
    }
}
