using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace tanmak.Engine
{
    public abstract class Sprite
    {
        public abstract void Render(GameObject Parent, DrawingContext dc);
    }
}
