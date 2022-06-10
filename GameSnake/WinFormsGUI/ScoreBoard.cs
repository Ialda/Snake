using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GameSnake
{
    public class ScoreBoard : Panel
    {
        public ScoreBoard(int width, int height) : base()
        {
            BackColor = Color.DarkBlue;
            DoubleBuffered = true;
            Width = width;
            Height = height;
        }
    }
}