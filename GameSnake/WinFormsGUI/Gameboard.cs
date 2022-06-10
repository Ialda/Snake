using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GameSnake
{
    public class Gameboard : Panel
    {
        public Gameboard(int width, int height) : base()
        {
            BackColor = Color.AliceBlue;
            DoubleBuffered = true;
            Width = width;
            Height = height;
        }
    }
}
