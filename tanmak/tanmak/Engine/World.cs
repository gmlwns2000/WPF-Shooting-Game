using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace tanmak.Engine
{
    public abstract class World
    {
        public List<GameObject> Objects = new List<GameObject>();
        public List<GameObject> PaddingObjects = new List<GameObject>();

        public double Width { get; set; }

        public double Height { get; set; }

        public GamePlane Plane { get; set; }

        public World(GamePlane Plane)
        {
            this.Plane = Plane;

            Width = Plane.ActualWidth;

            Height = Plane.ActualHeight;
        }

        public virtual void OnRender(DrawingContext dc)
        {
            foreach (GameObject obj in Objects)
            {
                if (!obj.IsDied)
                {
                    obj.OnRender(dc);
                }
            }
        }

        public virtual void OnUpdate()
        {
            Width = Plane.ActualWidth;

            Height = Plane.ActualHeight;

            ProcessPaddingObjects();

            foreach(GameObject obj in Objects)
            {
                if (!obj.IsDied)
                {
                    obj.OnUpdate();
                }
            }

            ProcessPaddingObjects(true);
        }

        internal void ProcessPaddingObjects(bool doUpdate = false)
        {
            if (doUpdate)
            {
                foreach (GameObject obj in PaddingObjects)
                {
                    obj.OnUpdate();
                }
            }

            if (PaddingObjects.Count > 0)
            {
                Objects.AddRange(PaddingObjects);
            }

            PaddingObjects.Clear();
        }

        public void AddObject(GameObject obj)
        {
            PaddingObjects.Add(obj);
        }
        
        public void DrawText(DrawingContext dc, string text = "", double x = 0, double y = 0, double size = 12, HorizontalAlignment ha = HorizontalAlignment.Left, VerticalAlignment va = VerticalAlignment.Top)
        {
            FormattedText ft = new FormattedText(text, System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, Defaults.Typeface, size, Brushes.Black);
            double xOffset = 0;
            switch (ha)
            {
                case HorizontalAlignment.Center:
                    xOffset = -ft.Width / 2;
                    break;
                case HorizontalAlignment.Right:
                    xOffset = -ft.Width;
                    break;
            }
            double yOffset = 0;
            switch (va)
            {
                case VerticalAlignment.Center:
                    yOffset = -ft.Height / 2;
                    break;
                case VerticalAlignment.Bottom:
                    yOffset = -ft.Height;
                    break;
            }
            dc.DrawText(ft, new Point(Math.Round(x + xOffset), Math.Round(y + yOffset)));
        }

        internal void GarbageCollection()
        {
            bool on = true;
            int index = 0;
            while (on)
            {
                if (index >= Objects.Count)
                {
                    on = false;

                    break;
                }
                else
                {
                    if (Objects[index].IsDied)
                    {
                        Objects.RemoveAt(index);
                    }
                    else
                    {
                        index++;
                    }
                }
            }
        }
    }
}
