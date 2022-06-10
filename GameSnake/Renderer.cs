using System;
using System.Windows.Forms;
using System.Drawing;

namespace GameSnake
{
    class Renderer
    {

        public enum Entity { P1Bodypart, P2Bodypart, P3Bodypart, FoodDiet, FoodStandard, FoodValuable, FoodSpecial,Empty }
        private int Width;
        private int Height;
        Entity[,] EntityBoard;
        /*Pen BluePen = new Pen(Color.Blue);
        Pen RedPen = new Pen(Color.Red);
        Pen BlackPen = new Pen(Color.Black);
        Pen GreenPen = new Pen(Color.Green);
        Pen HotPinkPen = new Pen(Color.HotPink);
        Pen MediumPurplePen = new Pen(Color.MediumPurple);*/

        SolidBrush Player1Brush = new SolidBrush(Color.Blue);
        SolidBrush Player2Brush = new SolidBrush(Color.Red);
        SolidBrush Player3Brush = new SolidBrush(Color.Green);
        SolidBrush DietFoodBrush = new SolidBrush(Color.HotPink);
        SolidBrush StandardFoodBrush = new SolidBrush(Color.Black);
        SolidBrush ValuableFoodBrush = new SolidBrush(Color.Purple);
        SolidBrush SpecialFoodBrush = new SolidBrush(Color.DarkRed);
        SolidBrush WhiteBrush = new SolidBrush(Color.White);
        

        public Renderer(int width, int height)//Engine engine
        {
            Width = width;
            Height = height;
            EntityBoard = new Entity[width, height];
            //Engine = engine;
            Clear();
        }
        
        public void Draw(int x, int y, Entity entity)
        {
            EntityBoard[x, y] = entity;
        }

        public void Clear()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    EntityBoard[x, y] = Entity.Empty;
                }
            }
        }

        public void Board_Paint(object sender, PaintEventArgs a)
        {
            var gameBoard = (Gameboard)sender;
            var xSize = gameBoard.Width / Width;
            var ySize = gameBoard.Height / Height;
            

            for(int y = 0; y < Height; y++)
            {
                for(int x = 0; x < Width; x++)
                {
                    switch(EntityBoard[x ,y])
                    {
                        case Entity.P1Bodypart:
                            //a.Graphics.DrawRectangle(RedPen, x * xSize, y * ySize, xSize, ySize);
                            a.Graphics.FillRectangle(Player1Brush, new Rectangle(x * xSize, y * ySize, xSize, ySize));
                            break;
                        case Entity.P2Bodypart:
                            //a.Graphics.DrawRectangle(BluePen, x * xSize, y * ySize, xSize, ySize);
                            a.Graphics.FillRectangle(Player2Brush, new Rectangle(x * xSize, y * ySize, xSize, ySize));
                            break;
                        case Entity.P3Bodypart:
                            //a.Graphics.DrawRectangle(GreenPen, x * xSize, y * ySize, xSize, ySize);
                            a.Graphics.FillRectangle(Player3Brush, new Rectangle(x * xSize, y * ySize, xSize, ySize));
                            break;
                        case Entity.FoodDiet:
                            //a.Graphics.DrawRectangle(HotPinkPen, x * xSize, y * ySize, xSize, ySize);
                            a.Graphics.FillRectangle(DietFoodBrush, new Rectangle(x * xSize, y * ySize, xSize, ySize));
                            break;
                        case Entity.FoodStandard:
                            //a.Graphics.DrawRectangle(BlackPen, x * xSize, y * ySize, xSize, ySize);
                            a.Graphics.FillRectangle(StandardFoodBrush, new Rectangle(x * xSize, y * ySize, xSize, ySize));
                            break;
                        case Entity.FoodValuable:
                            //a.Graphics.DrawRectangle(MediumPurplePen, x * xSize, y * ySize, xSize, ySize);
                            a.Graphics.FillRectangle(ValuableFoodBrush, new Rectangle(x * xSize, y * ySize, xSize, ySize));
                            break;
                        case Entity.FoodSpecial:
                            a.Graphics.FillRectangle(SpecialFoodBrush, new Rectangle(x * xSize, y * ySize, xSize, ySize));
                            break;
                        case Entity.Empty:
                            break;
                    }
                }
            }
        }

        public int P1Score, P2Score, P3Score;

        Font ScoreFont = new Font("Arial", 16);
        Font MenuFont = new Font("Arial", 26);
        Brush ScoreBrush = new SolidBrush(Color.White);

        public void Score_Paint(object sender, PaintEventArgs e)
        {
            var scorePanel = (ScoreBoard)sender;
            e.Graphics.DrawString($"Score {P1Score}", ScoreFont, Player1Brush, 5, (scorePanel.Height - 16) / 2);
            e.Graphics.DrawString($"Score {P2Score}", ScoreFont, Player2Brush, scorePanel.Width - 150, (scorePanel.Height - 16) / 2);
            e.Graphics.DrawString($"Score {P3Score}", ScoreFont, Player3Brush, scorePanel.Width / 2 - 50, (scorePanel.Height - 16) / 2);
        }

        public void GameOver_Paint(object sender, PaintEventArgs e)
        {
            string Winner;
            var menuBoard = (MenuBoard)sender;
            e.Graphics.DrawString($"Game Over", MenuFont, WhiteBrush, (menuBoard.Width / 2) - 100, menuBoard.Height / 2);
            if(P1Score > P2Score && P1Score > P3Score)
            {
                Winner = "Player 1";
            }
            else if(P2Score > P1Score && P2Score > P3Score)
            {
                Winner = "Player 2";
            }
            else if (P3Score > P1Score && P3Score > P2Score)
            {
                Winner = "Player 3";
            }
            else
            {
                Winner = "No One";
            }
            e.Graphics.DrawString($"{Winner} is the Winner!", MenuFont, WhiteBrush, (menuBoard.Width / 2) - 150, menuBoard.Height / 2 + 50);
        }

    }
}
