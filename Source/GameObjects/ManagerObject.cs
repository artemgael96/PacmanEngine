using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace ConsoleApp.Source.GameObjects
{
    class ManagerObject : IGameObject
    {
        public static ManagerObject Instance { get; private set; }
        public void Initialize(IEnumerable<IGameObject> gameObjects) 
        {
            Instance = new ManagerObject(gameObjects);
        }
        public string Name => ObjectNames.Manager; 
        public bool IsEnabled { get { return true; } set { } }
        public Animation Animation { get; set; }

        private readonly IGameObject pacman;
        private readonly IGameObject background;
        private readonly IGameObject[] ghost;

        public Coordinate PacmanLocation { get { return pacman != null ? pacman.Animation.Location : new Coordinate(0, 0); } }
        public Coordinate PacmanDirection { 
            get
            {
                if (pacman != null) 
                {
                    switch (pacman.Animation.AnimationType) 
                    {
                    case AnimationType.PacmanDown: return Coordinate.UnitY;
                    case AnimationType.PacmanUp: return -Coordinate.UnitY;
                    case AnimationType.PacmanLeft: return -Coordinate.UnitX;
                    default: return Coordinate.UnitX;
                    }
                } 
                else 
                { 
                    return -Coordinate.UnitY; 
                }
            }
        }
        private ManagerObject() { }
        public ManagerObject(IEnumerable<IGameObject> gameObjects) 
        {
            pacman = gameObjects.Single(x => x.Name == ObjectNames.Pacman);
            background = gameObjects.Single(x => x.Name == ObjectNames.Background);
            ghost = gameObjects.Where(x => x.Name == ObjectNames.Ghost).ToArray();

            //if (ghost.Length != 4)
            //    throw new Exception("Wrong number of ghost");
        }
        public void Update() { }
    }
}
