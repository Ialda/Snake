using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GameSnake
{
    class Engine
    {
        public Renderer Renderer;
        public readonly int Width;
        public readonly int Height;
        private bool GameOver = false;
        public List<Player> PlayerList = new List<Player>();
        private List<Food> FoodList = new List<Food>();

        Random Random = new Random();
        Pen Player1Pen = new Pen(Color.Red);
        Pen Player2Pen = new Pen(Color.Blue);
        Pen Player3Pen = new Pen (Color.Green);

        public Engine(int width, int height, Renderer renderer)
        {
            Width = width;
            Height = height;
            Renderer = renderer;
            Coordinate startingPosition = new Coordinate(Width / 4 * 3, Height / 2);
            PlayerList.Add(new Player(startingPosition, Width, Height, 1));
            startingPosition = new Coordinate(Width / 4, Height / 2);
            PlayerList.Add(new Player(startingPosition, Width, Height, 2));
            startingPosition = new Coordinate(Width / 4 + 4, Height / 2);
            //PlayerList.Add(new Player(Width, Height, Player1Pen, width, height, 1)); 
            PlayerList.Add(new Player(startingPosition, Width, Height, 3));  
        }

        public void Tick()
        {
            Renderer.Clear();
            PlaceFood();
            foreach (Player player in PlayerList)
            {
                if (player.Expired==false)
                {
                    player.Tick();
                    player.Draw(Renderer);
                }
            }
            foreach (Food food in FoodList)
            {
                food.Draw(Renderer);
            }
            Collide();
            SendScore();
        }

        private void PlaceFood()
        {
            if (FoodList.Count < PlayerList.Count + 1) // if (FoodList.Count < Player.Count + 1)
            {
                if (Random.Next(50) > 45)
                {
                    int number = Random.Next(5);
                    if (number == 1)
                    {
                        FoodList.Add(new FoodDiet(Random.Next(Width), Random.Next(Height)));
                    }
                    else if (number == 2)
                    {
                        FoodList.Add(new FoodValuable(Random.Next(Width), Random.Next(Height)));
                    }
                    else if (number == 3)
                    {
                        FoodList.Add(new FoodSpecial(Random.Next(Width), Random.Next(Height), this));
                    }
                    else
                    {
                        FoodList.Add(new FoodStandard(Random.Next(Width), Random.Next(Height)));
                    }
                }
            }
        }

        public bool IsGameOver()
        {
            int expiredAmmount = 0;
            foreach (Player player in PlayerList)
            {
                if(player.Expired == true)
                {
                    expiredAmmount++;
                }
            }
            
            if (expiredAmmount == PlayerList.Count)
            {
                return true;
            }
            return false;
        }

        /*public void Animate()
        {
            foreach(Player Player in PlayerList)
            {
            }
        }*/

        public Pen Draw(int x, int y)
        {
            Coordinate coordinate = new Coordinate(x, y);
            if(PlayerList[0].Coordinate.Equals(coordinate))
            {
                return Player1Pen;
               // PlayerList[0].Draw();
            }
            else if(PlayerList[1].Coordinate.Equals(coordinate))
            {
                return Player2Pen;
            }
            else
            {
                return Player3Pen;
               // PlayerList[1].Draw();
            }
                
           
        }

        public void Move(KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    PlayerList[0].Move(Player.Direction.Up);
                break;
                case Keys.Down:
                    PlayerList[0].Move(Player.Direction.Down);
                break;
                case Keys.Right:
                    PlayerList[0].Move(Player.Direction.Right);
                break;
                case Keys.Left:
                    PlayerList[0].Move(Player.Direction.Left);
                break;
                case Keys.W:
                    PlayerList[1].Move(Player.Direction.Up);
                break;
                case Keys.S:
                    PlayerList[1].Move(Player.Direction.Down);
                break;
                case Keys.D:
                    PlayerList[1].Move(Player.Direction.Right);
                break;
                case Keys.A:
                    PlayerList[1].Move(Player.Direction.Left);
                break;
                case Keys.I:
                    PlayerList[2].Move(Player.Direction.Up);
                break;
                case Keys.K:
                    PlayerList[2].Move(Player.Direction.Down);
                break;
                case Keys.L:
                PlayerList[2].Move(Player.Direction.Right);
                break;
                case Keys.J:
                PlayerList[2].Move(Player.Direction.Left);
                break;
            }
        }

        void Collide()
        {
            var playerMatrix = new Player[Width, Height];

            foreach (Player player in PlayerList)
            {
                if (player.Expired == false)
                    player.PlayerCollideDraw(playerMatrix);
            }
            foreach (Player player in PlayerList)
            {
                if (player.Expired == false)
                    player.Collide(playerMatrix);
            }
            foreach (Food food in FoodList)
            {
                food.Collide(playerMatrix);
            }
            FoodList.RemoveAll((food) => food.Expired);

            {
                // Not implemented, Beroende på implementation kan behöva flyttas under player colliden.
            }
            //PlayerList.RemoveAll((player) => player.Expired);
            //FoodList.RemoveAll((food) => food.Expired);
        }
         private void SendScore()
        {
            foreach (Player player in PlayerList)
            {
                if (player.PlayerNum == 1)
                    Renderer.P1Score = player.Score;
                else if (player.PlayerNum == 2)
                    Renderer.P2Score = player.Score;
                else
                    Renderer.P3Score = player.Score;
            }
        }
    }
}
