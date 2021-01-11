using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambitSzymora
{
    class ChessBoardSnapshot
    {
        public int turn;
        public List<PieceSnapshot> pieceSnapshots;

        public ChessBoardSnapshot(int turn, Square[,] squares)
        {
            this.turn = turn;
            pieceSnapshots = new List<PieceSnapshot>();
            foreach (Square square in squares)
                if (square.piece != null) pieceSnapshots.Add(new PieceSnapshot(square.piece));
        }
    }

}
