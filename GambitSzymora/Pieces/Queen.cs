using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace GambitSzymora
{
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
}

