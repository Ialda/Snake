using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GameSnake 
{
    class Player : ICollidable
    {
        private LinkedList<SnakeBody> Body;
        public int Score { get; private set; }

        private int BoardWidth, BoardHeight;
        Vector Vector = new Vector(1);
        public enum Direction { Up, Down, Left, Right }
        private Direction CurrentDirection = Direction.Up;
        public bool Expired = false;
        public Coordinate Coordinate;
        public int PlayerNum { get; private set; }
        private bool Moved = false;
        public int EffectTime;
        Random Random = new Random();
        int RandomDirection;
        public SnakeBody First => Body.First.Value;

        public Player(Coordinate StartingPosition, int width, int height, int playernum)
        {
            PlayerNum = playernum;
            Body = new LinkedList<SnakeBody>();
            //Coordinate = new Coordinate(width / 2, height / 2);
            //Body.AddLast(new SnakeBody(Coordinate));
            BoardWidth = width - 1; BoardHeight = height - 1;
            Body.AddLast(new SnakeBody(StartingPosition));
            while(Body.Count < 4)
            {
                AddBody();
            }
            //Pen = pen;
            Score = 0;
            EffectTime = 0;
        }

        public void Tick()
        {

            Coordinate toFollow = default;

           switch (CurrentDirection)
            {
                case Direction.Up:
                    if (Body.First.Value.Coordinate.Y == 0)
                    {
                        toFollow = new Coordinate(Body.First.Value.Coordinate.X, Body.First.Value.Coordinate.Y + BoardHeight);
                    }
                    else
                    {
                        toFollow = new Coordinate(Body.First.Value.Coordinate.X, Body.First.Value.Coordinate.Y - 1);
                    }
                    break;
                case Direction.Down:
                    if (Body.First.Value.Coordinate.Y == BoardHeight)
                    {
                        toFollow = new Coordinate(Body.First.Value.Coordinate.X, Body.First.Value.Coordinate.Y - BoardHeight);
                    }
                    else
                    {
                        toFollow = new Coordinate(Body.First.Value.Coordinate.X, Body.First.Value.Coordinate.Y + 1);
                    }
                    break;
                case Direction.Left:
                    if (Body.First.Value.Coordinate.X == 0)
                    {
                        toFollow = new Coordinate(Body.First.Value.Coordinate.X + BoardWidth, Body.First.Value.Coordinate.Y);
                    }
                    else
                    {
                        toFollow = new Coordinate(Body.First.Value.Coordinate.X - 1, Body.First.Value.Coordinate.Y);
                    }
                    break;
                case Direction.Right:
                    if (Body.First.Value.Coordinate.X == BoardWidth)
                    {
                        toFollow = new Coordinate(Body.First.Value.Coordinate.X - BoardWidth, Body.First.Value.Coordinate.Y);
                    }
                    else
                    {
                        toFollow = new Coordinate(Body.First.Value.Coordinate.X + 1, Body.First.Value.Coordinate.Y);
                    }
                    break;
            }
            
            if (toFollow != default)
            {
                foreach(SnakeBody node in Body)
                {
                    toFollow = node.Follow(toFollow);
                }
            }
            Coordinate = Body.First.Value.Coordinate;
            Moved = false;
        }
        public void AddBody()
        {
            Body.AddLast(new SnakeBody(Body.Last.Value.LastCoordinate));
        }
        public void RemoveBody()
        {
            if(Body.Count > 4)
            {
                Body.RemoveLast();
            }
        }
        public void AddScore(int boost)
        {
            Score = Score + boost;
        }
        public void AddEffectTime(int time)
        {
           EffectTime = time;
        }
        public void Move(Direction direction)
        {
            if (Moved == false && EffectTime > 0)
            {

                RandomDirection = Random.Next(4);
                
                if(RandomDirection == 1)
                {
                    if (CurrentDirection != Direction.Down)
                        CurrentDirection = Direction.Up;
                    if (CurrentDirection == Direction.Down)
                    {
                        CurrentDirection = Direction.Left;
                    }
                }
                else if(RandomDirection == 2)
                {
                    if (CurrentDirection != Direction.Up)
                        CurrentDirection = Direction.Down;
                    if (CurrentDirection == Direction.Up)
                    {
                        CurrentDirection = Direction.Right;
                    }
                }
                else if(RandomDirection == 3)
                {
                    if (CurrentDirection != Direction.Right)
                        CurrentDirection = Direction.Left;
                    if (CurrentDirection == Direction.Right)
                    {
                        CurrentDirection = Direction.Up;
                    }
                }
                else
                {
                    if (CurrentDirection != Direction.Left)
                     CurrentDirection = Direction.Right;
                    if (CurrentDirection == Direction.Left)
                    {
                        CurrentDirection = Direction.Down;
                    }
                }
            }
            else if (Moved == false)
            {
                switch (direction)
                {
                    case Direction.Up:
                        if (CurrentDirection != Direction.Down)
                            CurrentDirection = Direction.Up;
                        break;
                    case Direction.Down:
                        if (CurrentDirection != Direction.Up)
                           CurrentDirection = Direction.Down;
                        break;
                    case Direction.Left:
                        if (CurrentDirection != Direction.Right)
                            CurrentDirection = Direction.Left;
                        break;
                    case Direction.Right:
                        if (CurrentDirection != Direction.Left)
                        CurrentDirection = Direction.Right;
                        break;
                }
            }
            EffectTime--;
            Moved = true;

        }

        public void Draw(Renderer renderer)
        {
           foreach(SnakeBody node in Body)
            {
                node.Draw(renderer, PlayerNum);
            }
            
        }

        public void PlayerCollideDraw(Player[,] playerMatrix)
        {  
            if (playerMatrix[First.Coordinate.X, First.Coordinate.Y] != null)
            {
                playerMatrix[First.Coordinate.X, First.Coordinate.Y].Hit();
                Hit();
            }
            playerMatrix[First.Coordinate.X, First.Coordinate.Y] = this;
        }
        public void Collide(Player[,] playerMatrix)
        {
             bool kill;
            foreach (SnakeBody body in Body)
            {
                kill = body.Collide(playerMatrix);
                
                if (kill == true && Expired == false)
                {
                    AddScore(5);
                }
            }
        }
    
        public void Hit()
        {
            Expired = true;
        }
    }
}