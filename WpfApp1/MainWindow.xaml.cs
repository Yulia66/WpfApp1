using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace WpfApp1
{
    public partial class MainWindow : Window

    {
        Random random = new Random();
        Rectangle rect;
        public MainWindow()
        {
            InitializeComponent();
            CreateRectangles(10);
        }
        private void CreateRectangles(int nRect)
        {
            for (int i = 0; i < nRect; i++)
            {
                rect = new Rectangle();
                rect.Width = 200;
                rect.Height = 50;
                rect.HorizontalAlignment = HorizontalAlignment.Left;
                rect.VerticalAlignment = VerticalAlignment.Top;

                int x = random.Next((int)(this.Width - rect.Width));
                int y = random.Next((int)(this.Height - rect.Height));
                rect.Margin = new Thickness(x, y, 0, 0);
                rect.Stroke = Brushes.Black;
                byte r = (byte)random.Next(255);
                byte g = (byte)random.Next(255);
                byte b = (byte)random.Next(255);


                rect.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                rect.MouseDown += Rect_MouseDown;

                grid.Children.Add(rect);
            }
        }

        private void Rect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;

            Rect r = new Rect(rect.Margin.Left, rect.Margin.Top, rect.Width, rect.Height);
            // перебрать все
            if (r.IntersectsWith(r))
            {
                grid.Children.Remove(rect);
            }
        }
        private void CheckCoveredObjects()
        {
            foreach (Rectangle r1 in rect)
            {
                Rect bounds1 = new Rect(Canvas.GetLeft(r1), Canvas.GetTop(r1), r1.Width, r1.Height);
                bool isCovered = false;

                foreach (Rectangle r2 in rect)
                {
                    if (r1 != r2)
                    {
                        Rect bounds2 = new Rect(Canvas.GetLeft(r2), Canvas.GetTop(r2), r2.Width, r2.Height);

                        if (bounds1.IntersectsWith(bounds2))
                        {
                            isCovered = true;
                            break;
                        }
                    }
                }

                if (!isCovered)
                {
                    MainCanvas.Children.Remove(r1);
                }
            }
        }
    }
}   

