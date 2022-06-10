using System;
using System.Collections.Generic;
using System.Text;

namespace GameSnake
{
    class FoodSpecial : Food 
    {
        Engine Engine;
        private int TimeCounter;
        public FoodSpecial(int x, int y, Engine engine) 
        {
            Coordinate = new Coordinate(x, y);
            Points = 1;
            TimeCounter = 10;
            Engine = engine;
        }

        public override void Hit (Player player)
        {
            player.AddEffectTime(TimeCounter);
            player.AddScore(Points);
            Expired = true;
        }

        public override void Draw(Renderer renderer)
        {
            renderer.Draw(Coordinate.X, Coordinate.Y, Renderer.Entity.FoodSpecial);
        }

        public override void Collide(Player[,] playerMatrix)
        {
            Random random = new Random();
            int randomPlayer = random.Next(2);
            if(Engine.PlayerList[randomPlayer].Expired == true)
            {
                while(Engine.PlayerList[randomPlayer].Expired == true)
                {
                    if(Engine.PlayerList[0].Expired == true && Engine.PlayerList[1].Expired == true && Engine.PlayerList[2].Expired == true)
                    {
                        return;
                    }
                    randomPlayer = random.Next(2);
                }
            }
            if(playerMatrix[Coordinate.X, Coordinate.Y] != null)
            {   
                Hit(Engine.PlayerList[randomPlayer]);
            }
        }

    }
}