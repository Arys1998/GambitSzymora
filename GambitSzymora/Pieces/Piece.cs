using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace GambitSzymora
{

    class Move
    {
        public int columns;
        public int rows;

        public Move(int columns, int rows) { this.columns = columns; this.rows = rows; }
    }
    enum PieceColor
    {
        White,
        Black
    }
    abstract class Piece
    {
        public PieceColor color;
        public Image image;
        public int column;
        public int row;
        public abstract List<Move> GetMoves();

        public Piece(PieceColor color, int column, int row)
        {
            this.color = color;
            this.column = column;
            this.row = row;
        }

    }
}

