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

        public Match(ListBox movesList)
        {
            turns = 0;
            snapshots = new List<ChessBoardSnapshot>();
            pieceMoves = new List<PieceMove>();
            this.movesList = movesList;
        }

        public void SaveSnap(ChessBoardSnapshot snapshot, PieceMove pieceMove)
        {
            snapshots.Add(snapshot);
            pieceMoves.Add(pieceMove);

            ListBoxItem moveItem = new ListBoxItem();
            moveItem.Content = pieceMove.ToString();
            movesList.Items.Add(moveItem);
            turns++;
        }
        public void SaveSnap(ChessBoardSnapshot snapshot)
        {
            snapshots.Add(snapshot);
            turns++;
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
