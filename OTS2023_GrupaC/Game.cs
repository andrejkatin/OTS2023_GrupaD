using OTS2023_GrupaC.Exceptions;
using OTS2023_GrupaC.Models;

namespace OTS2023_GrupaC
{

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum Score
    {
        Poor,
        Average,
        Good
    }

    public class Game
    {
        public Map Map { get; set; }
        public Player Player { get; set; }

        public Game(Location playerLocation, Location beeLocation)
        {
            Map = new Map();
            Map.InitializeMap();

            if (!ValidateLocationInsideMap(playerLocation) || !ValidateLocationInsideMap(beeLocation))
            {
                throw new LocationOutsideOfMapException("Locations must be valid!");
            }

            int itemX = beeLocation.X;
            int itemY = beeLocation.Y;

            Map.Tiles[itemX, itemY].Content = TileContent.Bee;
            Player = new Player(playerLocation);
        }

        public void MovePlayer(Direction move)
        {
            Location playerPositionAfterMove = Player.GetLocationAfterMove(move);
            bool positionIsValid = ValidateLocation(playerPositionAfterMove);
            if (positionIsValid)
            {
                Player.MakeMove(move);
            }
        }

        public bool ValidateLocation(Location location)
        {
            int x = location.X;
            int y = location.Y;

            if (!ValidateLocationInsideMap(location))
            {
                return false;
            }
            if (Map.Tiles[x, y].Type.Equals(TileType.Hive))
            {
                return Player.BeeCollected;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateLocationInsideMap(Location location)
        {
            int x = location.X;
            int y = location.Y;

            if (x < 0 || x >= Map.MapSize || y < 0 || y >= Map.MapSize)
            {
                return false;
            }
            if (Map.Tiles[x, y].Type.Equals(TileType.MapBarrier))
            {
                return false;
            }
            return true;
        }

        public void UpdatePlayer()
        {
            int x = Player.Location.X;
            int y = Player.Location.Y;

            if (Map.Tiles[x, y].Content.Equals(TileContent.Flower))
            {
                Player.AmountOfFlowers++;
            }
            else if (Map.Tiles[x, y].Content.Equals(TileContent.Bee))
            {
                Player.BeeCollected = true;
            }
            else if (Map.Tiles[x, y].Type.Equals(TileType.Hive))
            {
                Player.AmountOfHoneyJars += Player.AmountOfFlowers;
                Player.AmountOfFlowers = 0;
            }

            Map.EmptyTileOnLocation(Player.Location);
        }


        public Score CalculateScore()
        {
            if (Player.AmountOfHoneyJars > 15)
            {
                return Score.Good;
            }
            if (Player.AmountOfFlowers >= 12 && Player.BeeCollected)
            {
                if (Player.AmountOfHoneyJars > 6)
                {
                    return Score.Good;
                }
                else
                {
                    return Score.Average;
                }
            }
            return Score.Poor;
        }
    }
}
