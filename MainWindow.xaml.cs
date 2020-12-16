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

namespace G04_LabelMove
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point startMouse, startMove;
        private bool bCanvaseMove = false;
        private bool bGridMove = false;

        private Point  currMouse;           // current Mouse-Position
        private Vector delta;               // delta: from Start to Current
        private Point  newMove;             // new position of element


        public MainWindow()
        {
            InitializeComponent();
        }

        private void DisplayInfo( )
        {
            if (Mouse.OverrideCursor == Cursors.Hand)
            {
                Ampel1.Background = Brushes.LightPink;
                Ampel2.Background = Brushes.LightSalmon;
                Ampel3.Background = Brushes.LightSalmon;
                Ampel4.Background = Brushes.LightPink;


                Ampel1.Content = $"startMove: {startMove.X} / {startMove.Y}";

                Ampel2.Content = $"currMouse: {currMouse.X} / {currMouse.Y}";
                Ampel3.Content = $"delta:     {delta.X} / {delta.Y}";

                Ampel4.Content = $"newMove:   {newMove.X} / {newMove.Y}";
            }
            else 
            {
                Ampel1.Background = Brushes.LightBlue;
                Ampel2.Background = Brushes.LightBlue;
                Ampel3.Background = Brushes.LightBlue;
                Ampel4.Background = Brushes.LightBlue;

                Ampel1.Content = "No Hand";
                Ampel2.Content = "No Hand";
                Ampel3.Content = "No Hand";
                Ampel4.Content = "No Hand";
            }
        }


        private void CMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            bCanvaseMove = true;

            startMouse = e.GetPosition(this);
            startMove.X = Canvas.GetLeft(CMove);
            startMove.Y = Canvas.GetTop(CMove);

            currMouse = startMouse;
            newMove = startMove;
            delta = new Vector { X = 0, Y = 0 };

            DisplayInfo();
        }

        private void GMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            bGridMove = true;

            startMouse = e.GetPosition(this);
            startMove.X = GMove.Margin.Left;
            startMove.Y = GMove.Margin.Top;

            currMouse = startMouse;
            newMove = startMove;
            delta = new Vector { X = 0, Y = 0 };

            DisplayInfo();
        }



        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {            
            currMouse = e.GetPosition(this);
            delta = startMouse - currMouse;
            newMove = startMove - delta;

            DisplayInfo();

            if (bCanvaseMove)
            {
                Canvas.SetLeft(CMove, newMove.X);
                Canvas.SetTop(CMove, newMove.Y);
            }

            if (bGridMove)
            {
                //GMove.Margin.Left = newMove.X;                // does not work!
                //GMove.Margin.Top = newMove.Y;                 // => new Thickness()
                GMove.Margin = new Thickness(newMove.X, newMove.Y, 0, 0);
            }
        }



        private void MainGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            bGridMove = false;
            bCanvaseMove = false;

            DisplayInfo();
        }



    }// MainWindow : Window
}
