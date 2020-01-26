using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;

namespace ConsoleApp.Source.GameObjects
{
    public class GameObject : IGameObject
    {
        protected readonly Coordinate initialCoordinate;
        private readonly AnimationType? initialAnimationType;
        public string Name { get; set; }
        public bool IsEnabled { get; set; } = true; //by default  = false
        public Animation Animation { get; set; }
        public GameObject (int x, int y, string name, AnimationType? animationType) 
            {
                initialCoordinate = new Coordinate(x * Coordinate.Multiplier, y * Coordinate.Multiplier);
                initialAnimationType = animationType;
                Name = name;
                Reset();
            }
        public virtual void Update() { }
        public virtual void Reset() {
            if (initialAnimationType.HasValue)
            {
                if (Animation == null || Animation.AnimationType != initialAnimationType.Value)
                    Animation = AnimationFactory.CreateAnimation(initialAnimationType.Value);
                Animation.Location = initialCoordinate;
            }
            else
                Animation = null;
            IsEnabled = true;
        }
    }
}
