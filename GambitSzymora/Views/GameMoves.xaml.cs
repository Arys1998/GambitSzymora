using GambitSzymora.Models;
using GambitSzymora.ViewModels;
using GambitSzymora.Views;
using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace GambitSzymora
{
    /// <summary>
    /// Interaction logic for GameMoves.xaml
    /// </summary>
    public partial class GameMoves : Window
    {
        HttpService httpService = new HttpService();
        //GameHistoryView gameHistoryView = new GameHistoryView();
        public GameMoves(int id)
        {
            InitializeComponent();
            this.LoadGameMoves(id);

        }

        private async void LoadGameMoves(int id)
        {
            string response = await httpService.GetEndpoitResponse("https://history-service.azurewebsites.net/api/GameMoves?id=" + id);
            IEnumerable<MoveModel> responseJson = JsonConvert.DeserializeObject<IEnumerable<MoveModel>>(response);
            foreach (var value in responseJson)
            {
                gameMovesList.Items.Add(value);
            }
        }
    }
}
