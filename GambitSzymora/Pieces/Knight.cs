using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace GambitSzymora
{
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
}

