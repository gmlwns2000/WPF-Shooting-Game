using Microsoft.Win32;
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
using tanmak.Game;

namespace tanmak
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            bool on = false;

            while (on)
            {
                Console.Write(">>> ");

                string line = Console.ReadLine();

                string low = line.ToLower();

                if (low == "exit")
                {
                    on = false;
                }
                else if(low == "suiside")
                {
                    Convert.ToInt32("aaaaaaaaaaaaaa");
                }
                else if(low == "quit")
                {
                    Environment.Exit(0);
                }
                else if(low == "hello")
                {
                    Console.WriteLine("Hello, world");
                }
                else if(low == "fortest")
                {
                    ForTest();
                }
                else
                {
                    Console.WriteLine("Unknown Command : " + line);
                }
            }

            grid.Children.Add(new InGamePlane());
        }

        private void ForTest()
        {
            try
            {
                Console.Write("Set Count >>> ");

                int count = Convert.ToInt32(Console.ReadLine());

                //for ([];[];[])
                for (int i = 0; i < count; i++)
                {
                    string output = i.ToString();

                    Console.WriteLine(output);
                }
            }
            catch
            {
                Console.WriteLine("Errored");
            }
        }
    }
}
