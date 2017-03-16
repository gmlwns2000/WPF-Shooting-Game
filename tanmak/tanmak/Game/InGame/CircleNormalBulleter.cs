using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;

namespace tanmak.Game
{
    public class CircleNormalBulleter : Bulleter
    {
        Engine.Random Random = new Engine.Random();

        public CircleNormalBulleter(GameObject parent) : base(parent)
        {

        }

        public override double Shoot()
        {
            double count = Random.Next(8, 50);

            double baseAngle = Random.NextDouble(0, 360);

            for (int i = 0; i < count; i++)
            {
                double angle = (360 / count) * i + baseAngle;
                angle = angle % 360;

                double angle_rad = angle / 180 * Math.PI;
                double speed = 3;

                double xvec = Math.Cos(angle_rad) * speed;
                double yvec = Math.Sin(angle_rad) * speed;

                World.AddObject(new CircleNormalBullet(World, Parent.X + Parent.Width / 2, Parent.Y + Parent.Height / 2, xvec, yvec, 3));
            }

            return 280;
        }
    }
}
