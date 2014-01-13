using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace WpfApplication2
{
    public class Gamestate
    {
        public string Name { get; set; }
        public int GamestateId { get; set; }
        public ObservableCollection<Piece> Pieces { get; set; }
        public bool Turn { get; set; }
        //0 = white

        public Gamestate()
        {
            Pieces = new ObservableCollection<Piece>();
            Turn = false;
            for (byte b = 0; b < 8; ++b)
                Pieces.Add(new Piece(PlayerColor.White, PieceType.Pawn, b, 6));

            Pieces.Add(new Piece(PlayerColor.White, PieceType.Rook, 0, 7));
            Pieces.Add(new Piece(PlayerColor.White, PieceType.Rook, 7, 7));

            Pieces.Add(new Piece(PlayerColor.White, PieceType.Knight, 1, 7));
            Pieces.Add(new Piece(PlayerColor.White, PieceType.Knight, 6, 7));

            Pieces.Add(new Piece(PlayerColor.White, PieceType.Bishop, 2, 7));
            Pieces.Add(new Piece(PlayerColor.White, PieceType.Bishop, 5, 7));

            Pieces.Add(new Piece(PlayerColor.White, PieceType.King, 4, 7));
            Pieces.Add(new Piece(PlayerColor.White, PieceType.Queen, 3, 7));


            for (byte b = 0; b < 8; ++b)
                Pieces.Add(new Piece(PlayerColor.Black, PieceType.Pawn, b, 1));

            Pieces.Add(new Piece(PlayerColor.Black, PieceType.Rook, 0, 0));
            Pieces.Add(new Piece(PlayerColor.Black, PieceType.Rook, 7, 0));

            Pieces.Add(new Piece(PlayerColor.Black, PieceType.Knight, 1, 0));
            Pieces.Add(new Piece(PlayerColor.Black, PieceType.Knight, 6, 0));

            Pieces.Add(new Piece(PlayerColor.Black, PieceType.Bishop, 2, 0));
            Pieces.Add(new Piece(PlayerColor.Black, PieceType.Bishop, 5, 0));

            Pieces.Add(new Piece(PlayerColor.Black, PieceType.Queen, 3, 0));
            Pieces.Add(new Piece(PlayerColor.Black, PieceType.King, 4, 0));
        }
    }

    public class GamestateContext : DbContext
    {
        public DbSet<Gamestate> Gamestates { get; set; }
    }

    public partial class MainWindow : Window
    {
        Piece movingPiece = null;
        GamestateContext db;

        public MainWindow()
        {
            var p = new Gamestate();
           db = new GamestateContext();
            var game = p;
           db.Gamestates.Add(game);
           db.SaveChanges();
            InitializeComponent();
            ChessBoard.ItemsSource = p.Pieces;
        }

        private void ChessPiece_MouseDown(object sender, MouseButtonEventArgs e)
        {
            movingPiece = ((Piece)(((Image)sender).DataContext));
            Debug.WriteLine("(" + movingPiece.x + ":" + movingPiece.y + ")");
        }

        private void ChessBoard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var x = e.GetPosition((Grid)sender).X / ((Grid)sender).ColumnDefinitions[0].ActualWidth;
            var y = e.GetPosition((Grid)sender).Y / ((Grid)sender).RowDefinitions[0].ActualHeight;
            Debug.WriteLine((int)x + " " + (int)y);
        }

        private void ChessBoardGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("x");
            e.Handled = true;
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.SaveChanges();
        }

    }
}
