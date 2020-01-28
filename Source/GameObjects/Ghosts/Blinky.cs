using System.Collections.Generic;

using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;


namespace ConsoleApp.Source.GameObjects
{
    class Blinky : GhostBace
    {
        public Blinky(int x, int y) : base(x, y, ObjectNames.Ghost, AnimationType.BlinkyDown) { }

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

        protected override Coordinate GetTargetCoordinate(Coordinate PacmanLocation) {
            var x = PacmanLocation.X / Coordinate.Multiplier;
            var y = PacmanLocation.Y / Coordinate.Multiplier;

            return new Coordinate(x * Coordinate.Multiplier, y * Coordinate.Multiplier);
        }
    }
}
