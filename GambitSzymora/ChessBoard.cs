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
using GambitSzymora.ViewModels;
using GambitSzymora.Models;
using Newtonsoft.Json;

namespace GambitSzymora
{
    public class ChessBoard : UniformGrid
    {
        private Square[,] squares = new Square[8, 8];
        private Square selectedSquare = null;
        private int turn;
        private int Turn
        {
            get { return turn; }
            set
            {
                turn = value;
                turnLabel.Content = $"Numer ruchu: {value}";
            }
        }
        private Match match;
        private HttpService httpService = new HttpService();
        private Button buttonPrevious;
        private Button buttonContinue;
        private Button buttonNext;
        private ListBox movesList;
        private Label turnLabel;

        private void SetSquare(int column, int row, Square square) { squares[column - 1, row - 1] = square; }
        private Square GetSquare(int column, int row) { return squares[column - 1, row - 1]; }
        private void SetPiece(int column, int row, Piece piece) { GetSquare(column, row).piece = piece; }
        private void AddPiece(int column, int row, Type type, PieceColor color) { GetSquare(column, row).AddPiece(type, color); }
        private void RemovePiece(int column, int row) { GetSquare(column, row).RemovePiece(); }
        private Piece GetPiece(int column, int row) { return GetSquare(column, row).piece != null ? GetSquare(column, row).piece : null; }

        private bool HavePiece(int column, int row) { return GetPiece(column,row) != null ? true : false; }

        //  -- Create chessboard using black and white squares.
        private void InitBoard()
        {
            Rows = 8;
            Columns = 8;
            for (int i = 1; i <= 8; i++)
                for (int ii = 1; ii <= 8; ii++)
                {
                    Square square;
                    if (ii % 2 == i % 2) { square = new Square(Constants.WhiteSquareColor, i, ii); }
                    else { square = new Square(Constants.BlackSquareColor, i, ii); }
                    square.Click += new RoutedEventHandler(OnSquareClick);
                    SetSquare(i, ii, square);
                    this.Children.Add(square);
                }
        }

        //  -- Create starting point for game adding pieces to chessboard.
        private void PopulateBoard()
        {
            for (int i = 1; i <= 8; i++)
            {
                AddPiece(2, i, typeof(Pawn), PieceColor.Black);
                AddPiece(7, i, typeof(Pawn), PieceColor.White);
            }
            AddPiece(1, 1, typeof(Rook), PieceColor.Black);
            AddPiece(1, 8, typeof(Rook), PieceColor.Black);
            AddPiece(8, 1, typeof(Rook), PieceColor.White);
            AddPiece(8, 8, typeof(Rook), PieceColor.White);

            AddPiece(1, 2, typeof(Knight), PieceColor.Black);
            AddPiece(1, 7, typeof(Knight), PieceColor.Black);
            AddPiece(8, 2, typeof(Knight), PieceColor.White);
            AddPiece(8, 7, typeof(Knight), PieceColor.White);

            AddPiece(1, 3, typeof(Bishop), PieceColor.Black);
            AddPiece(1, 6, typeof(Bishop), PieceColor.Black);
            AddPiece(8, 3, typeof(Bishop), PieceColor.White);
            AddPiece(8, 6, typeof(Bishop), PieceColor.White);

            AddPiece(1, 4, typeof(Queen), PieceColor.Black);
            AddPiece(8, 4, typeof(Queen), PieceColor.White);

            AddPiece(1, 5, typeof(King), PieceColor.Black);
            AddPiece(8, 5, typeof(King), PieceColor.White);

        }

        private void SelectSquare(Square square)
        {
            selectedSquare?.Unselect();
            RefreshView();
            square.Select();
            selectedSquare = square;
            foreach (Move move in selectedSquare?.piece.GetMoves())
                if (IsOnChessBoard(square.column + move.columns, square.row + move.rows) && CanMove(square, move))
                    GetSquare(square.column + move.columns, square.row + move.rows).Highlight();
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
                if (move.rows != 0 && !HavePiece(startSquare.column + move.columns, startSquare.row + move.rows)) return false;
                else if (move.rows == 0 && HavePiece(startSquare.column + move.columns, startSquare.row)) return false;
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
                if (HavePiece(columnToCheck, rowToCheck)) return false;
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
            else if (endSquare.piece is Rook) { var piece = endSquare.piece as Rook; piece.canCastle = false; }
            Turn++;
            ClearView();
            RefreshView();
        }

        //  -- Move Piece from start Square to end Square.
        private void MakeForcedMove(Square startSquare, Square endSquare)
        {
            endSquare.AddPiece(startSquare.piece);
            startSquare.RemovePiece();
            Turn++;
            ClearView();
        }

        //  -- Check if King of player who makes move is under check.
        bool IsChecked(int playerTurn)
        {
            PieceColor playerColor;
            if ((playerTurn) % 2 == 1) playerColor = PieceColor.White;
            else playerColor = PieceColor.Black;
            foreach (Square square in squares)
                if (square.piece != null && square.piece.color != playerColor)
                    foreach (Move move in square.piece.GetMoves())
                        if (IsOnChessBoard(square.column + move.columns, square.row + move.rows) && CanMove(square, move))
                            if (GetPiece(square.column + move.columns, square.row + move.rows) is King)
                                return true;
            return false;
        }

        //  -- Check if King of player who makes move is under mate.
        bool IsMated()
        {
            PieceColor playerColor;
            if (Turn % 2 == 1) playerColor = PieceColor.White;
            else playerColor = PieceColor.Black;
            foreach (Square square in squares)
                if (square.piece != null && square.piece.color == playerColor)
                    foreach (Move move in square.piece.GetMoves())
                        if (IsOnChessBoard(square.column + move.columns, square.row + move.rows) && CanMove(square, move))
                        {
                            MakeForcedMove(square, GetSquare(square.column + move.columns, square.row + move.rows));
                            if (!IsChecked(Turn - 1))
                            {
                                LoadSnapshot(Turn - 1);
                                return false;
                            }
                            LoadSnapshot(Turn - 1);
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

        private void ClearView()
        {
            selectedSquare = null;
            foreach (Square square in squares)
            {
                square.Unselect();
            }
        }

        private bool IsOnChessBoard(int column, int row)
        {
            if (column >= 1 && row >= 1 && column <= 8 && row <= 8) return true;
            else return false;
        }

        //  -- Promote pawn if possible.
        void PawnPromotion(Piece piece)
        {
            if ((Turn % 2 == 0 && piece.column == 8) ||
                (Turn % 2 == 1 && piece.column == 1))
            {
                RemovePiece(piece.column - 1, piece.row - 1);
                AddPiece(piece.column, piece.row, typeof(Queen), piece.color);
            }
        }


        //  -- Handles clicking on chessboard allowing player to select and move pieces of his color.
        void OnSquareClick(object sender, RoutedEventArgs e)
        {
            PieceColor colorMove;
            if (Turn % 2 == 0) colorMove = PieceColor.Black;
            else colorMove = PieceColor.White;

            Square clickedSquare = e.Source as Square;

            //  Check if players wants to select piece.
            if (clickedSquare.piece != null && clickedSquare.piece.color == colorMove)
            {
                SelectSquare(clickedSquare);
            }
            //  Check if any piece is selected.
            else if (selectedSquare != null && Turn == match.turns)
            {
                //  Get possible moves for selected piece and check if player can move to choosen square.
                List<Move> availableMoves = selectedSquare?.piece.GetMoves();
                Move foundMove = availableMoves.Find(move => (selectedSquare.row + move.rows == clickedSquare.row) && (selectedSquare.column + move.columns == clickedSquare.column));
                //  Check if Piece can make selected move, path to Square is not blocked and king is not taken.
                if (foundMove != null && (CanMove(selectedSquare, foundMove)) && !(clickedSquare.piece is King))
                {
                    (int, int) startPosition = (selectedSquare.column, selectedSquare.row);
                    Piece movedPiece = selectedSquare.piece;
                    if (selectedSquare.piece is King && Math.Abs(foundMove.rows) >= 2)
                    {
                        if (!TryCastle(selectedSquare, foundMove)) return;
                    }
                    else MakeMove(clickedSquare);
                    if (IsChecked(Turn-1))
                    {
                        LoadSnapshot(Turn - 1);
                        if (IsMated()) 
                        {
                            ShowMate();
                            MessageBox.Show("Game Ended!");
                        } 
                        ShowCheck();
                    }
                    else
                    {
                        match.SaveSnap(new ChessBoardSnapshot(Turn, squares), new PieceMove(movedPiece, startPosition, foundMove));
                    }
                }
            }
        }

        //  - Highlight king if is checked
        void ShowCheck()
        {
            PieceColor color;
            if (Turn % 2 == 1) color = PieceColor.White;
            else color = PieceColor.Black;
            foreach (Square square in squares)
                if (square.piece is King && square.piece.color == color)
                    square.ShowCheck(); 
        }

        //  - Highlight king if is on mate.
        void ShowMate()
        {
            PieceColor color;
            if (Turn % 2 == 1) color = PieceColor.White;
            else color = PieceColor.Black;
            foreach (Square square in squares)
                if (square.piece is King && square.piece.color == color)
                    square.ShowMate();
        }

        bool TryCastle(Square square, Move move)
        {
            King king = square.piece as King;
            int direction = move.rows / Math.Abs(move.rows);
            Square rookSquare = GetSquare(king.column, king.row + move.rows + direction);
            Rook rook = rookSquare.piece as Rook;
            if (king.canCastle && rook.canCastle)
            {
                MakeForcedMove(square, GetSquare(king.column, king.row + move.rows));
                MakeForcedMove(rookSquare, GetSquare(king.column, king.row - direction));
                Turn--;
                return true;
            }
            return false;

        }
        public ChessBoard(Button buttonPrevious, Button buttonContinue, Button buttonNext, ListBox movesList, Label turnLabel) : base()
        {
            this.movesList = movesList;
            this.turnLabel = turnLabel;
            this.buttonPrevious = buttonPrevious;
            this.buttonContinue = buttonContinue;
            this.buttonNext = buttonNext;

            buttonPrevious.Click += OnButtonPreviousClick;
            buttonContinue.Click += OnButtonContinueClick;
            buttonNext.Click += OnButtonNextClick;

            Turn = 1;

            InitBoard();
            PopulateBoard();
            match = new Match(movesList);
            match.SaveSnap(new ChessBoardSnapshot(Turn, squares));



        }

        void OnButtonPreviousClick(object sender, RoutedEventArgs e)
        {
            LoadSnapshot(Turn-1);
            ClearView();
        }

        void OnButtonContinueClick(object sender, RoutedEventArgs e)
        {
            LoadSnapshot(match.turns);
            ClearView();
        }
        void OnButtonNextClick(object sender, RoutedEventArgs e)
        {
            LoadSnapshot(Turn + 1);
            ClearView();
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
            if (loadTurn < 1 || loadTurn > match.turns) return;
            ChessBoardSnapshot snapshot = match.GetSnapshot(loadTurn);
            ClearBoard();
            foreach (PieceSnapshot pieceSnapshot in snapshot.pieceSnapshots)
            {
                Piece piece = pieceSnapshot.LoadSnapshot();
                squares[piece.column - 1, piece.row - 1].AddPiece(piece);
            }
            Turn = loadTurn;
        }
    }

}
