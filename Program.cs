using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.Run(new IGameObject[] {
                new Pacman(),
                new Background()
            }) ;
        }
    }

    class Background : BaseGameObject {
        public Background() {
            Animation = AnimationFactory.CreateAnimation(AnimationType.MazeBlue);
            Animation.Location = new Coordinate(0, 0);
        }
        public override void Update() { }
    }

    abstract class BaseGameObject : IGameObject {
        public string Name { get; set; }
        public bool IsEnabled { get; set; } = true;
        public Animation Animation { get; set; }
        public abstract void Update();
    }
    class Pacman : BaseGameObject, IProtagonist {
        public Pacman() {
            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
            //Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanLeft);
            //Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp);
            //Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanDown);
        }
        public DirectionKeys PressedKeys { get; set; }
        public void Collide(IEnumerable<IGameObject> collisions) { }
        public override void Update()
        {
            bool RightKeyPressed = (PressedKeys & DirectionKeys.Right) == DirectionKeys.Right,
                 LeftKeyPressed = (PressedKeys & DirectionKeys.Left) == DirectionKeys.Left,
                 UpKeyPressed = (PressedKeys & DirectionKeys.Up) == DirectionKeys.Up,
                 DownKeyPressed = (PressedKeys & DirectionKeys.Down) == DirectionKeys.Down;

            var StepRight = new Coordinate(0.1f, 0f);
            var StepLeft = new Coordinate(0.1f, 0f);
            var StepUp = new Coordinate(0f, 0.1f);
            var StepDown = new Coordinate(0f, 0.1f);

            var animationPacmanRight = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
            var animationPacmanLeft = AnimationFactory.CreateAnimation(AnimationType.PacmanLeft);
            var animationPacmanUp = AnimationFactory.CreateAnimation(AnimationType.PacmanUp);
            var animationPacmanDown = AnimationFactory.CreateAnimation(AnimationType.PacmanDown);

            if (RightKeyPressed) {
                var currentLocation = Animation.Location += StepRight;
                Animation = animationPacmanRight;
                Animation.Location = currentLocation;
            }

            if (LeftKeyPressed) {
                var currentLocation = Animation.Location -= StepLeft;
                Animation = animationPacmanLeft;
                Animation.Location = currentLocation;
            }

            if (UpKeyPressed) {
                var currentLocation = Animation.Location -= StepUp;
                Animation = animationPacmanUp;
                Animation.Location = currentLocation;
            }

            if (DownKeyPressed) {
                var currentLocation= Animation.Location += StepDown;
                Animation = animationPacmanDown;
                Animation.Location = currentLocation;
            }
        }
    }
}