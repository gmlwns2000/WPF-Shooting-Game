using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using tanmak.Engine;

namespace tanmak.Game
{
    public class TanmakWorld : World
    {
        ObjPlayer ObjPlayer;
        ObjEnemyCreater ObjEnemyCreater;

        public TanmakWorld(GamePlane Plane) : base(Plane)
        {
            ObjPlayer = new ObjPlayer(this);
            ObjEnemyCreater = new ObjEnemyCreater(this, ObjPlayer);

            Objects.Add(ObjPlayer);
            Objects.Add(ObjEnemyCreater);
        }

        public override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            dc.DrawText(new FormattedText("Score: " + ObjPlayer.ScoreManager.Score.ToString(), System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, Defaults.Typeface, 12, Brushes.Black), new System.Windows.Point(5, 5));
            dc.DrawText(new FormattedText("HP: " + ObjPlayer.ScoreManager.HP.ToString(), System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, Defaults.Typeface, 12, Brushes.Black), new System.Windows.Point(5, 20));
        }
    }
}
