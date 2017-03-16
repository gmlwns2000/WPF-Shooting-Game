using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using tanmak.Engine;

namespace tanmak.Game
{
    /// <summary>
    /// InGamePlane.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InGamePlane : UserControl, GamePlaneControl
    {
        public InGamePlane()
        {
            InitializeComponent();

            Loaded += InGamePlane_Loaded;
        }

        private void InGamePlane_Loaded(object sender, RoutedEventArgs e)
        {
            plane.World = new TanmakWorld(plane);
        }

        private void Bt_Restart_Click(object sender, RoutedEventArgs e)
        {
            plane.World = new TanmakWorld(plane);
        }

        public void SetTransfrom(Transform transfrom)
        {
            RenderTransform = transfrom;
        }

        public void SetTransfromOrigin(Point pt)
        {
            RenderTransformOrigin = pt;
        }
    }
}
