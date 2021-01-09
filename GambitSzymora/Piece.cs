using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;
using System.Windows;

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

    class Pawn : Piece
    {

        public Pawn(PieceColor color, int column, int row) : base(color, column, row)
        {
            String imagePath;
            if (color == PieceColor.White) imagePath = Constants.WhitePawnImage;
            else imagePath = Constants.BlackPawnImage;
            image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath,UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform,
                Height = Constants.PieceHeight,
                Width = Constants.PieceWidth
            };
        }
        public override List<Move> GetMoves()
        {
            if (color == PieceColor.White)
                if (column == 7) return new List<Move> { new Move(-1, 0), new Move(-1, 1), new Move(-1, -1), new Move(-2, 0)};
                else return new List<Move> { new Move(-1, 0) , new Move(-1, 1), new Move(-1, -1)};
            else
                if (column == 2) return new List<Move> { new Move(1, 0), new Move(1, 1), new Move(1, -1), new Move(2, 0) };
            else return new List<Move> { new Move(1, 0), new Move(1, 1), new Move(1, -1) };
        }
    }

    class Rook : Piece
    {

        public Rook(PieceColor color, int column, int row) : base(color, column, row)
        {
            String imagePath;
            if (color == PieceColor.White) imagePath = Constants.WhiteRookImage;
            else imagePath = Constants.BlackRookImage;
            image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform,
                Height = Constants.PieceHeight,
                Width = Constants.PieceWidth
            };
        }

        public override List<Move> GetMoves()
        {
            List<Move> moves = new List<Move>();
            for (int i = 1; i <= 8; i++)
            {
                moves.Add(new Move(i - column, 0)); //Move Vertical
                moves.Add(new Move(0, i - row));    //Move Horizontal
            }
            return moves;
        }
    }

    class Knight : Piece
    {

        public Knight(PieceColor color, int column, int row) : base(color, column, row)
        {
            String imagePath;
            if (color == PieceColor.White) imagePath = Constants.WhiteKnightImage;
            else imagePath = Constants.BlackKnightImage;
            image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform,
                Height = Constants.PieceHeight,
                Width = Constants.PieceWidth
            };
        }

        public override List<Move> GetMoves()
        {
            return new List<Move>
            {
                new Move(2, 1), new Move(2, -1),
                new Move(-2, 1), new Move(-2, -1),
                new Move(1, 2), new Move(-1, 2),
                new Move(1, -2), new Move(-1, -2)
            };
        }
    }

    class Bishop : Piece
    {

        public Bishop(PieceColor color, int column, int row) : base(color, column, row)
        {
            String imagePath;
            if (color == PieceColor.White) imagePath = Constants.WhiteBishopImage;
            else imagePath = Constants.BlackBishopImage;
            image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform,
                Height = Constants.PieceHeight,
                Width = Constants.PieceWidth
            };
        }

        public override List<Move> GetMoves()
        {
            List<Move> moves = new List<Move>();

            for (int i = 1; i <= 7; i++)
            {
                moves.Add(new Move(i, i));          //Move Down-Right
                moves.Add(new Move(i, -i));         //Move Down-Left
                moves.Add(new Move(-i, i));         //Move Up-Right
                moves.Add(new Move(-i, -i));        //Move Up-Left
            }
            return moves;
        }
    }
    class Queen : Piece
    {

        public Queen(PieceColor color, int column, int row) : base(color, column, row)
        {
            String imagePath;
            if (color == PieceColor.White) imagePath = Constants.WhiteQueenImage;
            else imagePath = Constants.BlackQueenImage;
            image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform,
                Height = Constants.PieceHeight,
                Width = Constants.PieceWidth
            };
        }

        public override List<Move> GetMoves()
        {
            List<Move> moves = new List<Move>();

            for (int i = 1; i <= 8; i++)
            {
                moves.Add(new Move(i, i));          //Move Down-Right
                moves.Add(new Move(i, -i));         //Move Down-Left
                moves.Add(new Move(-i, i));         //Move Up-Right
                moves.Add(new Move(-i, -i));        //Move Up-Left
                moves.Add(new Move(i - column, 0)); //Move Vertical
                moves.Add(new Move(0, i - row));    //Move Horizontal
            }
            return moves;
        }
    }

    class King : Piece
    {
        public bool canCastle = true;

        public King(PieceColor color, int column, int row) : base(color, column, row)
        {
            String imagePath;
            if (color == PieceColor.White) imagePath = Constants.WhiteKingImage;
            else imagePath = Constants.BlackKingImage;
            image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform,
                Height = Constants.PieceHeight,
                Width = Constants.PieceWidth
            };
        }

        public override List<Move> GetMoves()
        {
            return new List<Move> { new Move(0, 1), new Move(1, 0), new Move(1, 1), new Move(0, -1), new Move(-1, 0), new Move(-1, -1), new Move(1, -1), new Move(-1, 1) };
        }
    }
}

