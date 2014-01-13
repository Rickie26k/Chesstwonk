using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApplication2
{
    public enum PieceType
    {
        King, Queen, Rook, Knight, Bishop, Pawn
    }

    public enum PlayerColor
    {
        White,
        Black
    }


    // ToDo?: Compare setter values to previous value for PropertyChanged
    public class Piece : INotifyPropertyChanged
    {
        private PlayerColor playerColorValue;
        private PieceType pieceTypeValue;
        private byte xValue;
        private byte yValue;
        private bool hasMoved { get; set; }

        public Piece(PlayerColor playerColor, PieceType pieceType, byte x, byte y)
        {
            playerColorValue = playerColor;
            pieceTypeValue = pieceType;
            xValue = x;
            yValue = y;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public PlayerColor playerColor
        {
            get
            {
                return playerColorValue;
            }
        }

        public PieceType pieceType
        {
            get
            {
                return pieceTypeValue;
            }
            set
            {
                pieceTypeValue = value;
                NotifyPropertyChanged();
            }
        }

        public byte x
        {
            get
            {
                return xValue;
            }
            set
            {
                xValue = value;
                NotifyPropertyChanged();
            }
        }

        public byte y
        {
            get
            {
                return yValue;
            }
            set
            {
                yValue = value;
                NotifyPropertyChanged();
            }
        }

    }
}
