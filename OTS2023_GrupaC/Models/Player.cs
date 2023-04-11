
namespace OTS2023_GrupaC.Models
{
    public class Player
    {
        public Location Location { get; set; }
        public int AmountOfFlowers { get; set; }
        public int AmountOfHoneyJars { get; set; }
        public bool BeeCollected { get; set; }

        public Player()
        {
        }

        public Player(Location location)
        {
            Location = location;
        }


        public void MakeMove(Direction move)
        {
            switch (move)
            {
                case Direction.Up:
                    MoveUp();
                    break;
                case Direction.Down:
                    MoveDown();
                    break;
                case Direction.Left:
                    MoveLeft();
                    break;
                case Direction.Right:
                    MoveRight();
                    break;
                default:
                    break;
            }
        }

        public void MoveUp()
        {
            Location.Y--;
        }

        public void MoveDown()
        {
            Location.Y++;
        }

        public void MoveLeft()
        {
            Location.X--;
        }

        public void MoveRight()
        {
            Location.X++;
        }

        public Location GetLocationAfterMove(Direction move)
        {
            int x = Location.X;
            int y = Location.Y;
            switch (move)
            {
                case Direction.Up:
                    return new Location(x, y - 1);
                case Direction.Down:
                    return new Location(x, y + 1);
                case Direction.Left:
                    return new Location(x - 1, y);
                case Direction.Right:
                    return new Location(x + 1, y);
                default:
                    return null;
            }
        }
    }
}
