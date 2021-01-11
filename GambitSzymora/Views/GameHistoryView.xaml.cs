using GambitSzymora.Models;
using GambitSzymora.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GambitSzymora.Views
{
    /// <summary>
    /// Interaction logic for GameHistoryView.xaml
    /// </summary>
    public partial class GameHistoryView : UserControl
    {
        HttpService httpService = new HttpService();

        public GameHistoryView()
        {
            InitializeComponent();
            DownloadHistory();
        }

        private async void DownloadHistory()
        {
            string response = await httpService.GetEndpoitResponse("https://history-service.azurewebsites.net/api/HistoryGames?");

            IEnumerable<GameModel> responseJson = JsonConvert.DeserializeObject<IEnumerable<GameModel>>(response);
            gameHistoryList.Items.Clear();
            foreach (var value in responseJson)
            {
                gameHistoryList.Items.Add(value);
            }

        }


        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var gameId = (gameHistoryList.SelectedItem as GameModel).id;
            GameMoves gameMoves = new GameMoves(gameId);
            gameMoves.Show();
        }
    }
}
