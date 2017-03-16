using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using tanmak.Engine;

namespace tanmak.Game
{
    public class ObjEnemyOne : GameObject
    {
        Engine.Random rand = new Engine.Random();
        List<Bulleter> bullets = new List<Bulleter>();
        DispatcherTimer dispancer;
        ObjPlayer player;

        public ObjEnemyOne(World world, ObjPlayer player) : base(world)
        {
            MoveToRandom();

            Width = 80;
            Height = 48;

            Sprite = new RenctangleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 0)), Width, Height);

            this.player = player;

            bullets.Add(new CircleNormalBulleter(this));
            bullets.Add(new GuidedBulleter(this, player));

            dispancer = new DispatcherTimer();
            dispancer.Interval = TimeSpan.FromMilliseconds(600);
            dispancer.Tick += delegate
            {
                int bulletIndex = rand.Next(0, bullets.Count);

                double wait = bullets[bulletIndex].Shoot();

                dispancer.Interval = TimeSpan.FromMilliseconds(wait);
            };

            dispancer.Start();
        }

        public override void OnUpdate()
        {
            foreach(GameObject obj in World.Objects)
            {
                if (!obj.IsDied && obj is ObjOwnBullet)
                {
                    if (IsHitted(this, obj))
                    {
                        player.ScoreManager.EnemyHiited(ScoreManager.NormalMissileDamage);
                    }
                }
            }
        }

        private void MoveToRandom()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += delegate
            {
                double x = rand.NextDouble(10, World.Width - 10 - 80);

                double duration = Math.Abs(x - X) * 8;

                timer.Interval = TimeSpan.FromMilliseconds(duration);

                MoveTo(x, rand.NextDouble(10, 30), duration);
            };

            timer.Start();
        }
    }
}
