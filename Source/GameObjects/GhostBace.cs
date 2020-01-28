using System;
using System.Linq;

using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using ConsoleApp.Source.Helpers;

namespace ConsoleApp.Source.GameObjects
{
    public abstract class GhostBace : GameObject
    {
        protected enum Direction { Up, Down, Left, Right }
        public enum GhostState { Regular, BlueGhost, Eyes }

        private const int RegularGhostSpeed = Coordinate.Multiplier / 16;
        private const int BlueGhostSpeed = Coordinate.Multiplier / 10;
        private const int EyesSpeed = Coordinate.Multiplier / 8;

        private Coordinate step;

        protected Direction currentDirection = Direction.Down;
        public GhostState currentState = GhostState.Regular;

        public GhostBace(int x, int y, string name, AnimationType? animationType) : base(x, y, name, animationType) { }

        protected abstract Animation GetAnimation();
        protected abstract Coordinate GetTargetCoordinate(Coordinate PacmanLocation);

        public override void Update()
        {
            Animation.Location += step;
            if (Animation.Location.isRoundAll())
            {
                var target = GetTargetCoordinate(Manager.Instance.PacmanLocation);

                var path = PathFinder.GetPath(Animation.Location, target);

                if (path != null && path.Count() > 1)
                {
                    var moveVector = path[1] - Animation.Location;
                    Direction newDirection =
                        moveVector.X > 0 ? Direction.Right :
                        moveVector.X < 0 ? Direction.Left :
                        moveVector.Y > 0 ? Direction.Down : Direction.Up;

                    if (newDirection != currentDirection)
                    {
                        currentDirection = newDirection; ;
                        step = GetStep();
                        var currentPosition = Animation.Location;
                        Animation = GetAnimation();
                        Animation.Location = currentPosition;
                    }
                }
            }
        }
        private Coordinate GetStep()
        {
            int currentSpeed;
            switch (currentState)
            {
                case GhostState.BlueGhost:
                    currentSpeed = BlueGhostSpeed;
                    break;
                case GhostState.Eyes:
                    currentSpeed = EyesSpeed;
                    break;
                case GhostState.Regular:
                    currentSpeed = RegularGhostSpeed;
                    break;
                default: throw new Exception("Unknown ghost state");
            }

            switch (currentDirection)
            {
                case Direction.Up:
                    return new Coordinate(0, -currentSpeed);
                case Direction.Down:
                    return new Coordinate(0, currentSpeed);
                case Direction.Left:
                    return new Coordinate(-currentSpeed, 0);
                case Direction.Right:
                    return new Coordinate(currentSpeed, 0);
                default: throw new Exception("Unknown ghost currentDirectio");
            }
        }
    }
}
