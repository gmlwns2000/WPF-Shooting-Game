using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace tanmak.Engine
{
    public abstract class GameObject : DependencyObject
    {
        public static DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(GameObject));
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(GameObject));
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public double Width { get; set; }

        public double Height { get; set; }

        public Sprite Sprite { get; set; }

        public World World { get; set; }

        public bool IsDied { get; set; } = false;

        public GameObject(World world)
        {
            World = world;
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnRender(DrawingContext dc)
        {
            if(Sprite != null)
                Sprite.Render(this, dc);
        }

        private Storyboard MoveToStoryboard;
        public Storyboard MoveTo(double x, double y, double durationMs)
        {
            if(MoveToStoryboard != null)
            {
                MoveToStoryboard.Stop();
                MoveToStoryboard.Remove();
            }

            Storyboard sb = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(durationMs));

            DoubleAnimation xani = new DoubleAnimation(x, duration);
            DoubleAnimation yani = new DoubleAnimation(y, duration);

            Storyboard.SetTargetProperty(xani, new PropertyPath(XProperty));
            Storyboard.SetTargetProperty(yani, new PropertyPath(YProperty));

            Storyboard.SetTarget(xani, this);
            Storyboard.SetTarget(yani, this);

            sb.Children.Add(xani);
            sb.Children.Add(yani);

            sb.Begin();

            MoveToStoryboard = sb;

            return sb;
        }

        public static bool IsHitted(GameObject me, GameObject other)
        {
            if(other.X >= me.X - other.Width && other.X <= me.X + me.Width && other.Y >= me.Y - other.Height && other.Y <= me.Y + me.Height)
            {
                return true;
            }

            return false;
        }

        public bool IsHitted(GameObject other)
        {
            return IsHitted(this, other);
        }

        public void CheckOutOfBounds()
        {
            if (X < -Width || X > World.Width + Width || Y < -Height || Y > World.Height + Height)
            {
                IsDied = true;
            }
        }
    }
}