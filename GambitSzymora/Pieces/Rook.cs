using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace GambitSzymora
{
    class Rook : Piece
    {
        public bool canCastle = true;
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
}

