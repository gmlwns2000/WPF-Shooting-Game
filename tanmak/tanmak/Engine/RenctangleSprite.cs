using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace tanmak.Engine
{
    public class RenctangleSprite : Sprite
    {
        private Brush _brush;

        public double Width { get; set; }
        public double Height { get; set; }

        public RenctangleSprite(Brush Brush, double width, double height)
        {
            _brush = Brush.Clone();
            _brush.Freeze();

            Width = width;
            Height = height;
        }

        public override void Render(GameObject Parent, DrawingContext dc)
        {
            dc.DrawRectangle(_brush, null, new System.Windows.Rect(Parent.X, Parent.Y, Width, Height));
        }
    }
}
