using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace tanmak.Engine
{
    public class CircleSprite : Sprite
    {
        double radius = 5;
        Brush brush;
        private SolidColorBrush solidColorBursh;

        public CircleSprite(SolidColorBrush solidColorBursh)
        {
            this.solidColorBursh = solidColorBursh.Clone();

            this.solidColorBursh.Freeze();
        }

        public CircleSprite(Brush brush, double radius)
        {
            this.brush = brush;

            this.radius = radius;
        }

        public override void Render(GameObject Parent, DrawingContext dc)
        {
            dc.DrawEllipse(brush, null, new Point(Parent.X + radius, Parent.Y + radius), radius, radius);
        }
    }
}
