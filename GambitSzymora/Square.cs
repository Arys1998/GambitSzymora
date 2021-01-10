using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GambitSzymora
{
    class Square : Button
    {
        public Piece piece = null;
        public bool isMated = false;
        public bool isSelected = false;
        public int column;
        public int row;
        public Brush BrushColor;

        public Square(Brush color, int column, int row)
        {
            this.BrushColor = color;
            this.column = column;
            this.row = row;
            Background = color;
            BorderBrush = Constants.SquareBorderColor;
            BorderThickness = new Thickness(Constants.SquareBorderThickness);
            Click += SquareClick;
        }

        private void SquareClick(object sender, EventArgs e)
        {
        }

        public void AddPiece(Type pieceType, PieceColor color)
        {
            this.piece = Activator.CreateInstance(pieceType, color, column, row) as Piece;
            Content = piece.image;
        }
        public void AddPiece(Piece piece) { this.piece = piece; piece.column = column; piece.row = row; Content = piece.image; }
        public void RemovePiece() { this.piece = null; Content = null; }

        public void Select()
        {
            if (piece != null)
            {
                isSelected = true;
                //Background = Constants.SelectedSquareColor;
                BorderBrush = Constants.SelectedSquareBorderColor;
                BorderThickness = new Thickness(Constants.SelectedSquareBorderThickness);
            }
        }

        public void Highlight()
        {
            if (piece == null)
            {
                BorderBrush = Constants.HighlightedSquareBorderColor;
                BorderThickness = new Thickness(Constants.HighlightedSquareBorderThickness);
            }
            else
            {
                BorderBrush = Constants.HighlightedTakeSquareBorderColor;
                BorderThickness = new Thickness(Constants.HighlightedTakeSquareBorderThickness);
            }
        }

        public void ShowCheck()
        {
            BorderBrush = Constants.CheckedSquareBorderColor;
            BorderThickness = new Thickness(Constants.CheckedSquareBorderThickness);
        }

        public void ShowMate()
        {
            BorderBrush = Constants.MatedSquareBorderColor;
            BorderThickness = new Thickness(Constants.MatedSquareBorderThickness);
        }
        public void Unselect()
        {
            if (isSelected)
            {
                isSelected = false;
                BorderBrush = Constants.SquareBorderColor;
                BorderThickness = new Thickness(Constants.SquareBorderThickness);
            }
        }
    }
}
