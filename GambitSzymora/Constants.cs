using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GambitSzymora
{
    public static class Constants
    {
        //  White pieces image paths.
        public const string WhitePawnImage = "Images\\WPawn.png";
        public const string WhiteRookImage = "Images\\WRook.png";
        public const string WhiteKnightImage = "Images\\WKnight.png";
        public const string WhiteBishopImage = "Images\\WBishop.png";
        public const string WhiteKingImage = "Images\\WKing.png";
        public const string WhiteQueenImage = "Images\\WQueen.png";
        //  Black pieces image paths.
        public const string BlackPawnImage = "Images\\BPawn.png";
        public const string BlackRookImage = "Images\\BRook.png";
        public const string BlackKnightImage = "Images\\BKnight.png";
        public const string BlackBishopImage = "Images\\BBishop.png";
        public const string BlackKingImage = "Images\\BKing.png";
        public const string BlackQueenImage = "Images\\BQueen.png";
        //  Piece image size.
        public const int PieceHeight = 96;
        public const int PieceWidth = 96;

        //  Squares.
        public const int SquareBorderThickness = 1;
        public const int SelectedSquareBorderThickness = 6;
        public const int HighlightedSquareBorderThickness = 4;
        public const int HighlightedTakeSquareBorderThickness = 5;
        public const int CheckedSquareBorderThickness = 6;
        public const int MatedSquareBorderThickness = 12;

        public static Brush SquareBorderColor = Brushes.Pink;
        public static Brush SelectedSquareBorderColor = Brushes.Blue;
        public static Brush HighlightedSquareBorderColor = Brushes.SkyBlue;
        public static Brush HighlightedTakeSquareBorderColor = Brushes.DarkRed;
        public static Brush CheckedSquareBorderColor = Brushes.Red;
        public static Brush MatedSquareBorderColor = Brushes.Red;

        public static Brush WhiteSquareColor = (Brush)(new BrushConverter().ConvertFrom("#dfb995"));
        public static Brush BlackSquareColor = (Brush)(new BrushConverter().ConvertFrom("#4d2c26"));

        public static Brush AvailableMoveColor = Brushes.LightSkyBlue;
        public static Brush SelectedSquareColor = Brushes.Brown;

        public static char ToChar(int column)
        {
            switch (column)
            {
                case 1:
                    return 'A';
                case 2:
                    return 'B';
                case 3:
                    return 'C';
                case 4:
                    return 'D';
                case 5:
                    return 'E';
                case 6:
                    return 'F';
                case 7:
                    return 'G';
                case 8:
                    return 'H';
            }
            return '1';
        }
    }
}
