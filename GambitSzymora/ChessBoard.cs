using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace GambitSzymora
{
    public class ChessBoard : UniformGrid
    {
        private Square[,] squares = new Square[8, 8];
        private Square selectedSquare = null;
        private int turn = 1;
        private Match match;

        //  -- Create chessboard using black and white squares.
        private void InitBoard()
        {
            Rows = 8;
            Columns = 8;
            for (int i = 0; i < 8; i++)
                for (int ii = 0; ii < 8; ii++)
                {
                    Square square;
                    if (ii % 2 == i % 2) { square = new Square(Constants.WhiteSquareColor, i + 1, ii + 1); }
                    else { square = new Square(Constants.BlackSquareColor, i + 1, ii + 1); }
                    square.Click += new RoutedEventHandler(OnSquareClick);
                    squares[i, ii] = square;
                    this.Children.Add(square);
                }
        }

        //  -- Create starting point for game adding pieces to chessboard.
        private void PopulateBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                squares[1, i].AddPiece(typeof(Pawn), PieceColor.Black);
                squares[6, i].AddPiece(typeof(Pawn), PieceColor.White);
            }
            squares[0, 0].AddPiece(typeof(Rook), PieceColor.Black);
            squares[0, 7].AddPiece(typeof(Rook), PieceColor.Black);
            squares[7, 0].AddPiece(typeof(Rook), PieceColor.White);
            squares[7, 7].AddPiece(typeof(Rook), PieceColor.White);

            squares[0, 1].AddPiece(typeof(Knight), PieceColor.Black);
            squares[0, 6].AddPiece(typeof(Knight), PieceColor.Black);
            squares[7, 1].AddPiece(typeof(Knight), PieceColor.White);
            squares[7, 6].AddPiece(typeof(Knight), PieceColor.White);

            squares[0, 2].AddPiece(typeof(Bishop), PieceColor.Black);
            squares[0, 5].AddPiece(typeof(Bishop), PieceColor.Black);
            squares[7, 2].AddPiece(typeof(Bishop), PieceColor.White);
            squares[7, 5].AddPiece(typeof(Bishop), PieceColor.White);

            squares[0, 3].AddPiece(typeof(Queen), PieceColor.Black);
            squares[7, 3].AddPiece(typeof(Queen), PieceColor.White);

            squares[0, 4].AddPiece(typeof(King), PieceColor.Black);
            squares[7, 4].AddPiece(typeof(King), PieceColor.White);

        }

        private void Castle() { }
        private void SelectSquare(Square square)
        {
            selectedSquare?.Unselect();
            RefreshView();
            square.Select();
            selectedSquare = square;
            foreach (Move move in selectedSquare?.piece.GetMoves())
                if (IsOnChessBoard(square.column + move.columns, square.row + move.rows) && CanMove(square, move))
                    squares[square.column - 1 + move.columns, square.row - 1 + move.rows].Highlight();
        }

        //  -- Check if piece is not blocked and can move to choosen Square.
        private bool CanMove(Square startSquare, Move move)
        {
            int rowDirection, columnDirection, maxIncrement;


            //  Check if destination Square has Piece of the same color.
            Square destinationSquare = squares[startSquare.column - 1 + move.columns, startSquare.row - 1 + move.rows];
            if (destinationSquare.piece != null && destinationSquare.piece.color == startSquare.piece.color) return false;

            //  Knight can always move through other Pieces.
            if (startSquare.piece is Knight) return true;

            //  Check if player wants to move with Pawn.
            if (startSquare.piece is Pawn)
            {
                if (move.rows != 0 && squares[startSquare.column - 1 + move.columns, startSquare.row - 1 + move.rows].piece == null) return false;
                else if (move.rows == 0 && squares[startSquare.piece.column - 1 + move.columns, startSquare.piece.row - 1].piece != null) return false;
            }

            maxIncrement = Math.Max(Math.Abs(move.columns), Math.Abs(move.rows));
            if (move.columns != 0) columnDirection = move.columns / Math.Abs(move.columns);
            else columnDirection = 0;
            if (move.rows != 0) rowDirection = move.rows / Math.Abs(move.rows);
            else rowDirection = 0;

            //  Check if any square between selected and to which we want to move contains piece.
            for (int i = 1; i < maxIncrement; i++)
            {
                int columnToCheck = startSquare.column + i * columnDirection;
                int rowToCheck = startSquare.row + i * rowDirection;
                if (squares[columnToCheck - 1, rowToCheck - 1].piece != null) return false;
            }
            return true;
        }

        //  -- Make move with piece on selected Square.
        private void MakeMove(Square endSquare)
        {
            endSquare.AddPiece(selectedSquare.piece);
            selectedSquare.RemovePiece();
            selectedSquare.Unselect();
            selectedSquare = null;
            if (endSquare.piece is Pawn) PawnPromotion(endSquare.piece);
            else if (endSquare.piece is King) { var piece = endSquare.piece as King; piece.canCastle = false; }
            turn++;
            RefreshView();
        }

        //  -- Move Piece from start Square to end Square.
        private void MakeForcedMove(Square startSquare, Square endSquare)
        {
            endSquare.AddPiece(startSquare.piece);
            startSquare.RemovePiece();
            turn++;
        }

        //  -- Check if King of player who makes move is under check.
        bool IsChecked()
        {
            PieceColor playerColor;
            if ((turn - 1) % 2 == 1) playerColor = PieceColor.White;
            else playerColor = PieceColor.Black;
            foreach (Square square in squares)
                if (square.piece != null && square.piece.color != playerColor)
                    foreach (Move move in square.piece.GetMoves())
                        if (IsOnChessBoard(square.column + move.columns, square.row + move.rows) && CanMove(square, move))
                            if (squares[square.column - 1 + move.columns, square.row - 1 + move.rows].piece is King)
                                return true;
            return false;
        }

        //  -- Check if King of player who makes move is under mate.
        bool IsMated()
        {
            PieceColor playerColor;
            if (turn % 2 == 1) playerColor = PieceColor.White;
            else playerColor = PieceColor.Black;
            foreach (Square square in squares)
                if (square.piece != null && square.piece.color == playerColor)
                    foreach (Move move in square.piece.GetMoves())
                        if (IsOnChessBoard(square.column + move.columns, square.row + move.rows) && CanMove(square, move))
                        {
                            MakeForcedMove(square, squares[square.column - 1 + move.columns, square.row - 1 + move.rows]);
                            if (!IsChecked())
                            {
                                LoadSnapshot(turn - 1);
                                return false;
                            }
                            LoadSnapshot(turn - 1);
                        }
            return true;
        }

        //  -- Repaint Squares.
        private void RefreshView()
        {
            foreach (Square square in squares)
                if (square.isSelected) { square.BorderBrush = Constants.SelectedSquareBorderColor; square.BorderThickness = new Thickness(Constants.SelectedSquareBorderThickness); }
                else { square.BorderBrush = Constants.SquareBorderColor; square.BorderThickness = new Thickness(Constants.SquareBorderThickness); }
        }

        private bool IsOnChessBoard(int column, int row)
        {
            if (column >= 1 && row >= 1 && column <= 8 && row <= 8)
                return true;
            else
                return false;
        }

        //  -- Promote pawn if possible.
        void PawnPromotion(Piece piece)
        {
            if ((turn % 2 == 0 && piece.column == 8) ||
                (turn % 2 == 1 && piece.column == 1))
            {
                squares[piece.column - 1, piece.row - 1].RemovePiece();
                squares[piece.column - 1, piece.row - 1].AddPiece(typeof(Queen), piece.color);
            }
        }


        //  -- Handles clicking on chessboard allowing player to select and move pieces of his color.
        void OnSquareClick(object sender, RoutedEventArgs e)
        {
            PieceColor colorMove;
            if (turn % 2 == 0) colorMove = PieceColor.Black;
            else colorMove = PieceColor.White;

            Square clickedSquare = e.Source as Square;

            //  Check if players wants to select piece.
            if (clickedSquare.piece != null && clickedSquare.piece.color == colorMove)
            {
                SelectSquare(clickedSquare);
            }
            //  Check if any piece is selected.
            else if (selectedSquare != null)
            {
                //  Get possible moves for selected piece and check if player can move to choosen square.
                List<Move> availableMoves = selectedSquare?.piece.GetMoves();
                Move foundMove = availableMoves.Find(move => (selectedSquare.row + move.rows == clickedSquare.row) && (selectedSquare.column + move.columns == clickedSquare.column));
                if (foundMove != null && (CanMove(selectedSquare, foundMove)) && !(clickedSquare.piece is King))
                {
                    MakeMove(clickedSquare);
                    if (IsChecked())
                    {
                        LoadSnapshot(turn - 1);
                        ShowCheck();
                        if (IsMated()) ClearBoard();
                    }
                    else match.SaveSnap(new ChessBoardSnapshot(turn, squares));
                }
            }
        }

        void ShowCheck()
        {
            PieceColor color;
            if (turn % 2 == 1) color = PieceColor.White;
            else color = PieceColor.Black;
            foreach (Square square in squares)
                if (square.piece is King && square.piece.color == color) square.ShowCheck(); 
        }
        public ChessBoard(Button buttonNext, Button buttonPrevious) : base()
        {
            InitBoard();
            PopulateBoard();
            match = new Match();
            match.SaveSnap(new ChessBoardSnapshot(turn, squares));
        }

        //  -- Remove all Pieces from ChessBoard.
        void ClearBoard()
        {
            foreach (Square square in squares)
                if (square.piece != null)
                {
                    square.RemovePiece();
                    square.Unselect();
                }
            RefreshView();
        }

        //  -- Load state of ChessBoard from given turn.
        public void LoadSnapshot(int loadTurn)
        {
            ChessBoardSnapshot snapshot = match.GetSnapshot(loadTurn);
            ClearBoard();
            foreach (PieceSnapshot pieceSnapshot in snapshot.pieceSnapshots)
            {
                Piece piece = pieceSnapshot.LoadSnapshot();
                squares[piece.column - 1, piece.row - 1].AddPiece(piece);
            }
            turn = loadTurn;
        }
    }

}
