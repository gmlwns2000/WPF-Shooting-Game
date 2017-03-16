using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using tanmak.Engine;

namespace tanmak.Game
{
    public class ObjPlayer : GameObject
    {
        public ScoreManager ScoreManager { get; set; }

        Engine.Random rand = new Engine.Random();
        DispatcherTimer bulletCreate;
        DispatcherTimer camaraShake;
        int cameraShakeCount = 0;
        double speed = 3;
        double dyingSize = 12;

        public ObjPlayer(World world) : base(world)
        {
            X = Math.Round(world.Width / 2);
            Y = world.Height - 50;

            Width = 14;
            Height = 14;

            Sprite = new RenctangleSprite(new SolidColorBrush(Color.FromRgb(255, 50, 50)), Width, Height);

            ScoreManager = new ScoreManager();

            ScoreManager.Comboed += ScoreManager_Comboed;
            ScoreManager.Dieded += ScoreManager_Dieded;
        }

        private void ScoreManager_Dieded(object sender, ScoreManager e)
        {
            Console.WriteLine("YOU DIED!");

            if (bulletCreate != null)
                bulletCreate.Stop();

            DispatcherTimer t = new DispatcherTimer();
            int tcount = 0;
            t.Interval = TimeSpan.FromMilliseconds(15);
            t.Tick += delegate
            {
                tcount++;

                if(tcount > 60)
                {
                    t.Stop();

                    return;
                }

                dyingSize = dyingSize + (24 - dyingSize) / 10;
            };
            t.Start();
        }

        private void ScoreManager_Comboed(object sender, ScoreManager e)
        {
            Console.WriteLine("COMBO : " + e.Combo.ToString());

            World.AddObject(new ObjComboMessage(World, e.Combo));
        }

        public override void OnUpdate()
        {
            if (!ScoreManager.IsDied)
            {
                if (Keyboard.IsKeyDown(Key.A))
                {
                    X -= speed;
                }
                else if (Keyboard.IsKeyDown(Key.D))
                {
                    X += speed;
                }

                if (Keyboard.IsKeyDown(Key.W))
                {
                    Y -= speed;
                }
                else if (Keyboard.IsKeyDown(Key.S))
                {
                    Y += speed;
                }

                if (Keyboard.IsKeyDown(Key.Space))
                {
                    if (bulletCreate == null)
                    {
                        bulletCreate = new DispatcherTimer();
                        bulletCreate.Interval = TimeSpan.FromMilliseconds(75);
                        bulletCreate.Tick += delegate
                        {
                            World.AddObject(new ObjOwnBullet(World, X + Width / 2, Y));
                        };
                    }

                    bulletCreate.Start();
                }
                else
                {
                    if (bulletCreate != null)
                    {
                        bulletCreate.Stop();
                    }
                }

                X = Math.Min(World.Width - Width, Math.Max(0, X));

                Y = Math.Min(World.Height - Height, Math.Max(0, Y));

                foreach (GameObject obj in World.Objects)
                {
                    if (!obj.IsDied && obj is EnemyBullet)
                    {
                        if (IsHitted(this, obj))
                        {
                            ScoreManager.HeroHitted(((EnemyBullet)obj).Damage);

                            if(camaraShake == null)
                            {
                                camaraShake = new DispatcherTimer();
                                camaraShake.Interval = TimeSpan.FromMilliseconds(25);
                                camaraShake.Tick += delegate
                                {
                                    cameraShakeCount++;
                                    if (cameraShakeCount > 7)
                                    {
                                        camaraShake.Stop();
                                        World.Plane.ViewOffsetX = 0;
                                        World.Plane.ViewOffsetY = 0;
                                        return;
                                    }
                                    World.Plane.ViewOffsetX = rand.NextDouble(-5,5);
                                    World.Plane.ViewOffsetY = rand.NextDouble(-5,5);
                                };
                            }

                            cameraShakeCount = 0;
                            camaraShake.Start();
                        }
                    }
                }
            }
        }
        
        public override void OnRender(DrawingContext dc)
        {
            if (!ScoreManager.IsDied)
            {
                base.OnRender(dc);
            }
            else
            {
                World.DrawText(dc, "YOU DIED", World.Width / 2, World.Height / 2, dyingSize, System.Windows.HorizontalAlignment.Center, System.Windows.VerticalAlignment.Center);
            }
        }
    }
}
