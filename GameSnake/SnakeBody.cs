using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameSnake
{
    class SnakeBody : ICollidable
    {
        public Coordinate Coordinate { private set; get; }
        public Coordinate LastCoordinate { private set; get; }

        public SnakeBody(Coordinate coordinate)
        {
            Coordinate = new Coordinate(coordinate.X, coordinate.Y);
            LastCoordinate = Coordinate;
        }

        public Coordinate Follow(Coordinate coordinate)
        {
            LastCoordinate = new Coordinate(Coordinate.X, Coordinate.Y);
            Coordinate.X = coordinate.X;
            Coordinate.Y = coordinate.Y;
            return LastCoordinate;
        }

        public void Draw(Renderer renderer, int num)
        {
            if (num == 1)
                renderer.Draw(Coordinate.X, Coordinate.Y, Renderer.Entity.P1Bodypart);
            else if (num == 2)
                renderer.Draw(Coordinate.X, Coordinate.Y, Renderer.Entity.P2Bodypart);
            else
                renderer.Draw(Coordinate.X, Coordinate.Y, Renderer.Entity.P3Bodypart);
        }
        /*public void Draw(Renderer renderer)
        {
            renderer.Draw(Coordinate.X, Coordinate.Y, Renderer.Entity.Bodypart);
        }*/
        public bool Collide(Player[,] collisionMatrix)
        {
            if (collisionMatrix[Coordinate.X, Coordinate.Y] != null && !collisionMatrix[Coordinate.X, Coordinate.Y].First.Equals(this))
            {
                collisionMatrix[Coordinate.X, Coordinate.Y].Hit();
                return true;
            }
            return false;
        }
        public void Hit()
        {
            
        }
        public void Eat()
        {

        }
    }
}
