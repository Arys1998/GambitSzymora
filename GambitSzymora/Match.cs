using GambitSzymora.Models;
using GambitSzymora.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GambitSzymora
{
    class Match
    {
        public int turns;
        List<ChessBoardSnapshot> snapshots;
        List<PieceMove> pieceMoves;
        ListBox movesList;
        MoveModel moveModel = new MoveModel();
        HttpService httpService = new HttpService();
        int matchID;
        public Match(ListBox movesList)
        {
            turns = 0;
            snapshots = new List<ChessBoardSnapshot>();
            pieceMoves = new List<PieceMove>();
            this.movesList = movesList;
            GetMatchId();
        }

        public async Task GetMatchId()
        {
            string response = await httpService.GetEndpoitResponse("https://history-service.azurewebsites.net/api/GetCurrentGameID?");
            GameID responseJson = JsonConvert.DeserializeObject<GameID>(response);
            matchID = responseJson.id;
        }


        public async void SaveSnap(ChessBoardSnapshot snapshot, PieceMove pieceMove)
        {
            snapshots.Add(snapshot);
            pieceMoves.Add(pieceMove);

            ListBoxItem moveItem = new ListBoxItem();
            moveItem.Content = pieceMove.ToString();
            movesList.Items.Add(moveItem);


            string startPosition = pieceMove.getStartPosition();
            string endPosition = pieceMove.getEndPosition();
            moveModel.id_partii = matchID;
            moveModel.nr_ruchu = turns;
            moveModel.poz_pocz = startPosition;
            moveModel.poz_konc = endPosition;

            httpService.postMovesToDB(moveModel);
            turns++;
        }


        public void SaveSnap(ChessBoardSnapshot snapshot)
        {
            snapshots.Add(snapshot);
            turns++;
        }

        public void SaveSnap(ChessBoardSnapshot snapshot, int savedTurn)
        {
            snapshots[savedTurn-1] = snapshot;
        }
        public ChessBoardSnapshot GetSnapshot(int turn)
        {
            if (turn > turns) return null;
            else return snapshots[turn - 1];
        }
        public PieceMove GetPieceMove(int turn)
        {
            if (turn > turns) return null;
            else return pieceMoves[turn - 1];
        }
    }
}
