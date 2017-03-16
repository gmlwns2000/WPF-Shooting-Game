using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace tanmak.Engine
{
    public class GamePlane : FrameworkElement
    {
        private VisualCollection canvas;

        public GamePlaneControl PlaneControl { get; set; }

        private World _world;
        public virtual World World
        {
            get
            {
                return _world;
            }
            set
            {
                _world = value;
            }
        }

        public double ViewOffsetX { get; set; }
        public double ViewOffsetY { get; set; }

        public double ViewScaleOriginX { get; set; } = 0.5;
        public double ViewScaleOriginY { get; set; } = 0.5;

        public double ViewScaleX { get; set; } = 1;
        public double ViewScaleY { get; set; } = 1;

        protected override Visual GetVisualChild(int index)
        {
            return canvas[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return canvas.Count;
            }
        }

        public GamePlane()
        {
            canvas = new VisualCollection(this);

            Loaded += GamePlane_Loaded;
        }

        private GamePlaneControl GetGamePlaneParent(DependencyObject obj)
        {
            if(obj != null && obj is FrameworkElement)
            {
                if (obj is GamePlaneControl)
                    return (GamePlaneControl)obj;

                return GetGamePlaneParent(((FrameworkElement)obj).Parent);
            }
            return null;
        }

        private void GamePlane_Loaded(object sender, RoutedEventArgs e)
        {
            PlaneControl = GetGamePlaneParent(Parent);

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            Render();
        }

        private void Render()
        {
            if (_world != null)
            {
                _world.OnUpdate();

                DrawingVisual view = new DrawingVisual();

                using (DrawingContext dc = view.RenderOpen())
                {
                    _world.OnRender(dc);
                }
                
                TransformGroup group = new TransformGroup();
                group.Children.Add(new TranslateTransform(ViewOffsetX, ViewOffsetY));
                group.Children.Add(new ScaleTransform() { CenterX = ViewScaleOriginX, CenterY = ViewScaleOriginY, ScaleX = ViewScaleX, ScaleY = ViewScaleY });
                PlaneControl.SetTransfromOrigin(new Point(ViewScaleOriginX, ViewScaleOriginY));
                PlaneControl.SetTransfrom(group);

                PushVisual(view);
            }
        }

        public void PushVisual(Visual visual)
        {
            canvas.Clear();
            canvas.Add(visual);
        }
    }
}
