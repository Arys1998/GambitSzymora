using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambitSzymora
{
    class PieceSnapshot
    {
        Piece piece;
        int column;
        int row;

        public PieceSnapshot(Piece piece)
        {
            this.piece = piece;
            column = piece.column;
            row = piece.row;
        }
        public Piece LoadSnapshot() { piece.column = column; piece.row = row; return piece; }
    }
}
