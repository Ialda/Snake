using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSnake
{
    public class WinformsSnake : Form
    {
        Gameboard GameBoard;
        ScoreBoard ScoreBoard;
        MenuBoard MenuBoard;
        Renderer Renderer;
        Engine Engine;
        Timer GameTimer = new Timer();
        int FPS = 7;
        public WinformsSnake() : base()
        {
            //Width = 800;
            //Height = 600;

            Width = 800;
            Height = 500;
            int ScoreHeight = 100;

            BackColor = Color.DarkBlue;
            int width = 40;
            int height = 30;
            GameBoard = new Gameboard(Width, Height - ScoreHeight);
            ScoreBoard = new ScoreBoard(Width, ScoreHeight);
            MenuBoard = new MenuBoard(Width, Height - ScoreHeight);
            ScoreBoard.Location = new Point(0, GameBoard.Height); //Kan åka igenom orm
            Renderer = new Renderer(width, height - ScoreHeight/10);
            Engine = new Engine(width, height - ScoreHeight/10, Renderer);
            /*BackColor = System.Drawing.Color.DarkBlue;
            int width = 40;
            int height = 20;
            GameBoard = new Gameboard(Width, Height-100);
            ScoreBoard = new ScoreBoard();
            Renderer = new Renderer(Width, Height-100, Engine);
            Engine = new Engine(Width, Height-100, Renderer);*/
            
            Controls.Add(GameBoard);
            Controls.Add(ScoreBoard);
            Controls.Add(MenuBoard);
            KeyPreview = true;
            KeyDown += WinformsSnake_KeyDown;
            GameTimer.Interval = 1000 / FPS;
            GameTimer.Tick += Timer_Tick;
            GameBoard.Paint += Renderer.Board_Paint;
            ScoreBoard.Paint += Renderer.Score_Paint;
            MenuBoard.Paint += Renderer.GameOver_Paint;
            
            GameTimer.Start();

        }

         private void Timer_Tick(object sender, EventArgs e)
        {
            Engine.Tick();

            if (Engine.IsGameOver())
            {
                StopGame();
            }

            Refresh();
        }

        private void StopGame()
        {
            GameBoard.Hide();
            GameTimer.Stop();
        }

        private void WinformsSnake_KeyDown(object sender, KeyEventArgs e)
        {
            Engine.Move(e);   
        }

    }
}
