using System;
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
    public class Pieces : ObservableCollection<Piece>
    {
        public Pieces()
        {
            for (byte b = 0; b < 8; ++b)
                Add(new Piece(PlayerColor.White, PieceType.Pawn, b, 6));
            
            Add(new Piece(PlayerColor.White, PieceType.Rook, 0, 7));
            Add(new Piece(PlayerColor.White, PieceType.Rook, 7, 7));

            Add(new Piece(PlayerColor.White, PieceType.Knight, 1, 7));
            Add(new Piece(PlayerColor.White, PieceType.Knight, 6, 7));

            Add(new Piece(PlayerColor.White, PieceType.Bishop, 2, 7));
            Add(new Piece(PlayerColor.White, PieceType.Bishop, 5, 7));

            Add(new Piece(PlayerColor.White, PieceType.King, 4, 7));
            Add(new Piece(PlayerColor.White, PieceType.Queen, 3, 7));
 
            
            for (byte b = 0; b < 8; ++b)
                Add(new Piece(PlayerColor.Black, PieceType.Pawn, b, 1));

            Add(new Piece(PlayerColor.Black, PieceType.Rook, 0, 0));
            Add(new Piece(PlayerColor.Black, PieceType.Rook, 7, 0));

            Add(new Piece(PlayerColor.Black, PieceType.Knight, 1, 0));
            Add(new Piece(PlayerColor.Black, PieceType.Knight, 6, 0));

            Add(new Piece(PlayerColor.Black, PieceType.Bishop, 2, 0));
            Add(new Piece(PlayerColor.Black, PieceType.Bishop, 5, 0));

            Add(new Piece(PlayerColor.Black, PieceType.Queen, 3, 0));
            Add(new Piece(PlayerColor.Black, PieceType.King, 4, 0));

        }
    }

    public partial class MainWindow : Window
    {
        Piece movingPiece = null;
        PlayerColor currentPlayer;
        Point dragPoint;
        

        public MainWindow()
        {
            var p = new Pieces();
            InitializeComponent();
            ChessBoard.ItemsSource = p;
            currentPlayer = PlayerColor.White;
        }

        private void ChessPiece_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            movingPiece = ((Piece)(((Image)sender).DataContext));
            dragPoint = e.GetPosition(null);
        }

        //ToDo: If using SystemParameters.MinimumXYDragDistance, consider grid_mousemove. If the Image is 
        // left too soon, or the travelled mouse distance within the Image is too short, this event will not fire.
        private void ChessPiece_MouseMove(object sender, MouseEventArgs e)
        {
            Vector diff = dragPoint - e.GetPosition(null);
            Image img = sender as Image;


            if (e.LeftButton == MouseButtonState.Pressed &&
                //Math.Abs(diff.X) //SystemParameters.MinimumHorizontalDragDistance &&
                //Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance &&
                img != null

            )
            {
                Debug.WriteLine("dragging");
                DragDrop.DoDragDrop(img, img, DragDropEffects.Move);


            }
        }

        private void ChessBoardGrid_Drop(object sender, DragEventArgs e)
        {
            Debug.WriteLine("dropped");
            var x = e.GetPosition((Grid)sender).X / ((Grid)sender).ColumnDefinitions[0].ActualWidth;
            var y = e.GetPosition((Grid)sender).Y / ((Grid)sender).RowDefinitions[0].ActualHeight;
            Debug.WriteLine((int)x + " " + (int)y);


            movingPiece.x = (byte)x;
            movingPiece.y = (byte)y;
        }

    }
}
