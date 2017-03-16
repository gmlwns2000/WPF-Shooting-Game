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
    public class ObjComboMessage : GameObject
    {
        int combo;
        FormattedText text;
        Color color = Colors.LightPink;
        double opacity = 1;
        bool fade = false;

        public ObjComboMessage(World world, int Combo) : base(world)
        {
            combo = Combo;

            text = new FormattedText(Combo.ToString() + " Combo!", System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, Defaults.Typeface, 12 + 3 * Math.Sqrt(combo), Brushes.Magenta);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(45);

            timer.Tick += delegate
            {
                fade = true;
            };

            timer.Start();
        }

        public override void OnUpdate()
        {
            if (!fade)
                return;

            opacity -= 0.3;
            if (opacity < 0)
            {
                IsDied = true;
                return;
            }

            SolidColorBrush brush = new SolidColorBrush(color);

            brush.Opacity = opacity;

            brush.Freeze();

            text.SetForegroundBrush(brush);
        }

        public override void OnRender(DrawingContext dc)
        {
            dc.DrawText(text, new System.Windows.Point(World.Width / 2 - text.Width / 2, World.Height / 2 - text.Height / 2));
        }
    }
}
