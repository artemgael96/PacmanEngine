using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

using ConsoleApp.Source.GameObjects.Ghosts;

namespace ConsoleApp.Source.GameObjects
{
    class Manager : IGameObject
    {
        public static Manager Instance { get; private set; }
        public void Initialize(IEnumerable<GameObject> gameObjects)
        {
            Instance = new Manager(gameObjects);
        }
        public string Name => ObjectNames.Manager;
        public bool IsEnabled { get { return true; } set { } }
        //public bool BigCoinEatenByPacman { get { return true; } set { } }
        public Animation Animation { get; set; }
        private readonly GameObject pacman;
        private readonly GameObject background;
        private readonly GameObject[] ghost;

        public void BigCoinEatenByPacman(bool BigCoinTimerOnOff) {            
            var mazeWhite = AnimationFactory.CreateAnimation(AnimationType.MazeWhite);
            var mazeBlue = AnimationFactory.CreateAnimation(AnimationType.MazeBlue);
            if (BigCoinTimerOnOff)
            {
                Instance.background.Animation = mazeWhite;
                foreach (var ghost in Instance.ghost)
                {
                    (ghost as GhostBace).SetBlueGhostState();
                }
            }
            else
            {
                Instance.background.Animation = mazeBlue;
                foreach (var ghost in Instance.ghost)
                {
                    (ghost as GhostBace).SetRegularGhostState();
                }
            }

            if (BigCoinTimerOnOff) {

            }
        }

        public void GhostEatenByPacman() {
            foreach (var ghost in Instance.ghost)
            {
                (ghost as GhostBace).SetEyesGhostState();
                ((GhostBace)ghost).SetEyesGhostState();

            }
        }



        public Coordinate PacmanLocation {
            get
            {
                return pacman != null ? pacman.Animation.Location : new Coordinate(0, 0);
            }
        }
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
        public Manager(IEnumerable<GameObject> gameObjects) 
        {
            pacman = gameObjects.Single(x => x.Name == ObjectNames.Pacman);
            background = gameObjects.Single(x => x.Name == ObjectNames.Background);
            ghost = gameObjects.Where(x => x.Name == ObjectNames.Ghost).ToArray();

            //if (ghost.Length != 4)
            //    throw new Exception("Wrong number of ghost");
        }
        public void Update() {}
    }
}
