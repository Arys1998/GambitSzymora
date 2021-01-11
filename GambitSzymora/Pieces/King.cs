using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace GambitSzymora
{
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
            List<Move> result = new List<Move> { new Move(0, 1), new Move(1, 0), new Move(1, 1), new Move(0, -1), new Move(-1, 0), new Move(-1, -1), new Move(1, -1), new Move(-1, 1) };
            if (canCastle) { result.Add(new Move(0, -3)); result.Add(new Move(0, 2)); }
            return result;
        }
    }
}

