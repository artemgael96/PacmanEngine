using ConsoleApp.Source.GameObjects;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Source
{
    static class ObjectsBuilder
    {
        private const string initializer = "000000000000000000000 022222222202222222220 020002000202000200020 030002000202000200030 020002000202000200020 022222222222222222220 020002020000020200020 020002020000020200020 022222022202220222220 000002000101000200000 000002011111110200000 000002010010010200000 000002010151010200000 111112110678011211111 000002010000010200000 000002011111110200000 000002010000010200000 000002010000010200000 022222222202222222220 020002000202000200020 032202222242222202230 000202020000020202000 000202020000020202000 022222022202220222220 020000000202000000020 022222222222222222220 000000000000000000000";

        static void Main(string[] args)
        {
            var objects = new List<GameObject>() { new GameObject(0, 0, ObjectNames.Background, AnimationType.MazeBlue) };

            foreach (var square in initializer.Split(' ').SelectMany((row, y) => row.Select((ch, x) => new { ch, x, y })))
                InitSquare(square.ch, square.x, square.y, objects);

            var path = PathFinder.GetPath(new Coordinate(5000000, 13000000), new Coordinate(15000000, 13000000));

            Engine.Run(objects);
        }

        private static void InitSquare(char squareType, int x, int y, List<GameObject> gameObjects)
        {
            // Wall = 0, Empty = 1, SmallCoin = 2, BigCoin = 3, Pacman = 4, Blinky = 5, Pinky = 6, Inky = 7, Clyde = 8.
            switch (squareType)
            {
                case '2':
                    gameObjects.Add(new GameObject(x, y, ObjectNames.Coin, AnimationType.SmallCoin));
                    break;
                case '3':
                    gameObjects.Add(new GameObject(x, y, ObjectNames.BigCoin, AnimationType.BigCoin));
                    break;
                case '4':
                    gameObjects.Add(new Pacman(x, y));
                    break;
            }

            if (squareType != '0')
                PathFinder.Grid[x, y] = true;
        }
    }
}
