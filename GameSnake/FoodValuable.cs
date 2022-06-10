using System;
using System.Collections.Generic;
using System.Text;

namespace GameSnake
{
    class FoodValuable : Food
    {
        public FoodValuable(int x, int y)
        {
            Coordinate = new Coordinate(x, y);
            Points = 5;
        }

        public override void Hit(Player player)
        {
            player.AddBody();
            player.AddBody();
            player.AddScore(Points);
            Expired = true;
        }
        public override void Draw(Renderer renderer)
        {
            renderer.Draw(Coordinate.X, Coordinate.Y, Renderer.Entity.FoodValuable);
        }
    }
}
