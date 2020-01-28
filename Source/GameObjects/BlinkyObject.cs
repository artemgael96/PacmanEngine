using PacmanEngine.Components.Graphics;
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
    class BlinkyObject : GhostBace
    {
        public BlinkyObject(int x, int y) : base(x, y, ObjectNames.Ghost, AnimationType.BlinkyDown) { }

        protected override Animation GetAnimation()
        {
            AnimationType animationType = AnimationType.BlueGhost;

            if(currentState == GhostState.Regular)
            {
                switch (currentDirection)
                {
                    case Direction.Up:
                        animationType = AnimationType.BlinkyUp;
                        break;
                    case Direction.Down:
                        animationType = AnimationType.BlinkyDown;
                        break;
                    case Direction.Left:
                        animationType = AnimationType.BlinkyLeft;
                        break;
                    case Direction.Right:
                        animationType = AnimationType.BlinkyRight;
                        break;
                }
            } else if (currentState == GhostState.Eyes)
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

        protected override Coordinate GetTargetCoordinate(Coordinate PacmanLocation) {
            var x = PacmanLocation.X / Coordinate.Multiplier;
            var y = PacmanLocation.Y / Coordinate.Multiplier;

            return new Coordinate(x * Coordinate.Multiplier, y * Coordinate.Multiplier);
        }

        //protected override Coordinate GetTargetCoordinate(Coordinate PacmanLocation)
        //{
        //    var pacmanDirection = ManagerObject.Instance.PacmanDirection;
        //    var target = new Coordinate
        //      ((Coordinate.WorldWidth + (PacmanLocation.X + pacmanDirection.X * 4)) % Coordinate.WorldWidth,
        //      (Coordinate.WorldHeight + (PacmanLocation.Y + pacmanDirection.Y * 4)) % Coordinate.WorldHeight);

        //    if (PathFinder.isSquareEmpty(target))
        //        return target;
        //    else
        //        return PacmanLocation;
        //}

        public void Collide(IEnumerable<IGameObject> collisions)
        {
            foreach (var obj in collisions)
                if (obj.Name == ObjectNames.Coin)
                    obj.IsEnabled = false;
        }
    }
}
