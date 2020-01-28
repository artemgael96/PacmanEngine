using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace ConsoleApp.Source.GameObjects.Ghosts
{
    class Inky : GhostBace
    {
        public Inky(int x, int y) : base(x, y, ObjectNames.Ghost, AnimationType.BlinkyDown) { }

        protected override Animation GetAnimation()
        {
            AnimationType animationType = AnimationType.BlueGhost;

            if (currentState == GhostState.Regular)
            {
                switch (currentDirection)
                {
                    case Direction.Up:
                        animationType = AnimationType.InkyUp;
                        break;
                    case Direction.Down:
                        animationType = AnimationType.InkyDown;
                        break;
                    case Direction.Left:
                        animationType = AnimationType.InkyLeft;
                        break;
                    case Direction.Right:
                        animationType = AnimationType.InkyRight;
                        break;
                }
            }
            else if (currentState == GhostState.Eyes)
            {
                switch (currentDirection)
                {
                    case Direction.Up:
                        animationType = AnimationType.EyesUp;
                        break;
                    case Direction.Down:
                        animationType = AnimationType.EyesDown;
                        break;
                    case Direction.Left:
                        animationType = AnimationType.EyesLeft;
                        break;
                    case Direction.Right:
                        animationType = AnimationType.EyesRight;
                        break;
                }
            }
            return AnimationFactory.CreateAnimation(animationType);
        }

        protected override Coordinate GetTargetCoordinate(Coordinate PacmanLocation)
        {
            var pacmanDirection = Manager.Instance.PacmanDirection;
            var target = new Coordinate
              ((Coordinate.WorldWidth + (PacmanLocation.X + pacmanDirection.X % 4)) % Coordinate.WorldWidth,
              (Coordinate.WorldHeight + (PacmanLocation.Y + pacmanDirection.Y % 4)) % Coordinate.WorldHeight);

            if (PathFinder.isSquareEmpty(target))
                return target;
            else
                return PacmanLocation;
        }
    }
}
