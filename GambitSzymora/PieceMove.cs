using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambitSzymora
{
    class PieceMove
    {
        Piece piece;
        (int, int) startPosition;
        (int, int) endPosition;
        
        public PieceMove(Piece piece, (int, int) startPosition, Move move)
        {
            this.piece = piece;
            this.startPosition = startPosition;
            endPosition = (startPosition.Item1 + move.columns, startPosition.Item2 + move.rows);
        }

        public string ToString()
        {
            char startColumn = Constants.ToChar(startPosition.Item2);
            char endColumn = Constants.ToChar(endPosition.Item2);
            string result = $"{piece.color} moves {piece.GetType().Name} from {startColumn}{startPosition.Item1} to {endColumn}{endPosition.Item1}";
            return result;
        }
    }
}
