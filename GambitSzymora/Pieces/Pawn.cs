using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace GambitSzymora
{
    class Pawn : Piece
    {

        public Pawn(PieceColor color, int column, int row) : base(color, column, row)
        {
            String imagePath;
            if (color == PieceColor.White) imagePath = Constants.WhitePawnImage;
            else imagePath = Constants.BlackPawnImage;
            image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform,
                Height = Constants.PieceHeight,
                Width = Constants.PieceWidth
            };
            Console.WriteLine(image);
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
}

