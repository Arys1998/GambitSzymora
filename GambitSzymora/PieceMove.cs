using GambitSzymora.ViewModels;
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
        HttpService httpService = new HttpService();
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
            string result = $"{piece.color} moves {piece.GetType().Name} from {startColumn}{9-startPosition.Item1} to {endColumn}{9-endPosition.Item1}";
            return result;
        }

       public string getStartPosition()
        {
            char startColumn = Constants.ToChar(startPosition.Item2);
            return $"{startColumn}{9-startPosition.Item1}";
        }
        public string getEndPosition()
        {
            char endColumn = Constants.ToChar(endPosition.Item2);
            return $"{endColumn}{9-endPosition.Item1}";
        }

    }
}
