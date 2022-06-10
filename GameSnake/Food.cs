using System;
using System.Collections.Generic;
using System.Text;

namespace GameSnake
{
    abstract class Food
    {
        public Coordinate Coordinate { protected set; get; }
        public int Points { protected set; get; }
        public bool Expired = false;
        


        public abstract void Hit(Player player);
        
        public virtual void Collide(Player[,] playerMatrix)
        {
            if(playerMatrix[Coordinate.X, Coordinate.Y] != null)
            {
                Hit(playerMatrix[Coordinate.X, Coordinate.Y]);
            }
        }

        public abstract void Draw(Renderer renderer);
    }
}
