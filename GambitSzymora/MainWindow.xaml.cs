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

namespace GambitSzymora
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChessBoard chessBoard = new ChessBoard(ButtonPrevious, ButtonContinue, ButtonNext, MovesList, TurnLabel);
            Grid.SetRow(chessBoard, 1);
            GamePanel.Children.Add(chessBoard);
        }
    }
}
