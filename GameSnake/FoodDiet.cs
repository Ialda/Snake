using System;
using System.Collections.Generic;
using System.Text;

namespace GameSnake
{
    class FoodDiet : Food
    {
        public FoodDiet(int x, int y)
        {
            Coordinate = new Coordinate(x, y);
            Points = 1;
        }
        
        public override void Hit(Player player)
        {
            player.RemoveBody();
            player.AddScore(Points);
            Expired = true; 
        }
        public override void Draw(Renderer renderer)
        {
            renderer.Draw(Coordinate.X, Coordinate.Y, Renderer.Entity.FoodDiet);
        }

    }
}
