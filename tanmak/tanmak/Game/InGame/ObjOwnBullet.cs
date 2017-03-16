using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using tanmak.Engine;

namespace tanmak.Game
{
    public class ObjOwnBullet : GameObject
    {
        public ObjOwnBullet(World world, double X, double Y) : base(world)
        {
            this.X = X;
            this.Y = Y;

            Width = 3;
            Height = 15;

            Sprite = new RenctangleSprite(new SolidColorBrush(Color.FromRgb(255, 50, 220)), Width, Height);
        }

        public override void OnUpdate()
        {
            Y -= 15;

            if(Y < -100)
            {
                IsDied = true;
            }
        }
    }
}
