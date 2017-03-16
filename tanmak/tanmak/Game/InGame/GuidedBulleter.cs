using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using tanmak.Engine;

namespace tanmak.Game
{
    public class GuidedBulleter : Bulleter
    {
        GameObject player;

        public GuidedBulleter(GameObject parent, GameObject player) : base(parent)
        {
            this.player = player;
        }

        DispatcherTimer creater;
        int count = 0;
        public override double Shoot()
        {
            if(creater == null)
            {
                creater = new DispatcherTimer();
                creater.Interval = TimeSpan.FromMilliseconds(40);
                creater.Tick += delegate
                {
                    if (count > 5)
                    {
                        creater.Stop();
                        return;
                    }
                    count++;
                    World.AddObject(new GuidedBullet(World, Parent, player, Parent.X + Parent.Width / 2, Parent.Y + Parent.Height / 2));
                };
            }

            count = 0;
            creater.Start();

            return 800;
        }
    }
}
