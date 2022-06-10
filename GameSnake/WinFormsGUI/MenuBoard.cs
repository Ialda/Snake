using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GameSnake
{
    public class MenuBoard : Panel
    {
        public MenuBoard(int width, int height) : base()
        {
            BackColor = Color.DarkBlue;
            DoubleBuffered = true;
            Width = width;
            Height = height;
        }
    }
}