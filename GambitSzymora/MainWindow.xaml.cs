using GambitSzymora.Models;
using GambitSzymora.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        HttpService httpService = new HttpService();
        public MainWindow()
        {
            InitializeComponent();
            ChessBoard chessBoard = new ChessBoard(ButtonNext, ButtonPrevious);
            Grid.SetRow(chessBoard, 1);
            GamePanel.Children.Add(chessBoard);
            DataContext = new MainViewModel();
            
        }

     

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private async void StartNewGame(object sender, RoutedEventArgs e)
        {
            await httpService.GetEndpoitResponse("https://history-service.azurewebsites.net/api/StartNewGame?");
            MoveModel moveModel = new MoveModel();

            this.Close();
        }

    }
}
