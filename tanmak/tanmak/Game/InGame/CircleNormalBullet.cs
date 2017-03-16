using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using tanmak.Engine;

namespace tanmak.Game
{
    public class CircleNormalBullet : EnemyBullet
    {
        double x_vec;
        double y_vec;
        double radius;

        public CircleNormalBullet(World world, double x, double y, double x_vec, double y_vec, double radius) : base(world)
        {
            X = x;
            Y = y;

            this.x_vec = x_vec;
            this.y_vec = y_vec;
            this.radius = radius;

            Damage = ScoreManager.NormalBulletDamage;

            Sprite = new Engine.CircleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 255)), radius);
        }

        public override void OnUpdate()
        {
            X += x_vec;
            Y += y_vec;

            CheckOutOfBounds();
        }
    }
}
