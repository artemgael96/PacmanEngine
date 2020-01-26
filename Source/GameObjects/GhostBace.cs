using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using ConsoleApp.Source.Helpers;

namespace ConsoleApp.Source.GameObjects
{
    public abstract class GhostBace : GameObject
    {
        private enum Direction { Up, Down, Left, Right }
        private enum GhostState { Regular, BlueGhost, Eyes }

        private const int RegularGhostSpeed = Coordinate.Multiplier / 8;
        private const int BlueGhostSpeed = Coordinate.Multiplier / 10;
        private const int EyesSpeed = Coordinate.Multiplier / 16;


        protected Coordinate step;
        public GhostBace(int x, int y, string name, AnimationType? animationType) : base(x, y, name, animationType)
        {
           var loc = ManagerObject.Instance.PacmanLocation;
        }
        protected abstract Animation GetAnimation();
        protected abstract Coordinate GetTargetCoordinate(Coordinate PacmanLocation);
        public override void Update()
        {
            Animation.Location += step;

            if (Animation.Location.isRoundAll())
            {
                var target = GetTargetCoordinate(ManagerObject.Instance.PacmanLocation);
                var path = PathFinder.GetPath(Animation.Location, target);

                if (path != null && path.Count() > 1)
                {
                    var moveVector = path[1] - Animation.Location;

                    Direction newDirection =
                        moveVector.X > 0 ? Direction.Right :
                        moveVector.X < 0 ? Direction.Left :
                        moveVector.Y > 0 ? Direction.Down : Direction.Up;
                }
            }
        }
    }
}
