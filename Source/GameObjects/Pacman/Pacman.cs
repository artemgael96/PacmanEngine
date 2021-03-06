﻿using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using System.Collections.Generic;

namespace ConsoleApp.Source.GameObjects
{
    public class Pacman : GameObject, IProtagonist
    {
        private readonly int speed = Coordinate.Multiplier / 8;
        private DirectionKeys currentDirection = DirectionKeys.Up;
        private bool alive = true;
        private Coordinate step;
        private int BigCoinTimer;
        private bool BigCoinTimerOnOff;
        public Pacman(int x, int y) : base(x, y, ObjectNames.Pacman, AnimationType.PacmanUp) { }
        public DirectionKeys PressedKeys
        {
            set
            {
                if (alive && value != DirectionKeys.None && (value & currentDirection) != currentDirection)
                {
                    Coordinate newStep, position = Animation.Location;
                    
                    if ((value & DirectionKeys.Up) == DirectionKeys.Up)
                    {
                        newStep = new Coordinate(0, -speed);
                        if (PathFinder.CanMove(position, newStep))
                        {
                            step = newStep;
                            currentDirection = DirectionKeys.Up;
                            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanUp);
                            Animation.Location = position;
                        }
                    }
                    else if ((value & DirectionKeys.Down) == DirectionKeys.Down)
                    {
                        newStep = new Coordinate(0, speed);
                        if (PathFinder.CanMove(position, newStep))
                        {
                            step = newStep;
                            currentDirection = DirectionKeys.Down;
                            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanDown);
                            Animation.Location = position;
                        }
                    }
                    else if ((value & DirectionKeys.Left) == DirectionKeys.Left)
                    {
                        newStep = new Coordinate(-speed, 0);
                        if (PathFinder.CanMove(position, newStep))
                        {
                            step = newStep;
                            currentDirection = DirectionKeys.Left;
                            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanLeft);
                            Animation.Location = position;
                        }
                    }
                    else if ((value & DirectionKeys.Right) == DirectionKeys.Right)
                    {
                        newStep = new Coordinate(speed, 0);
                        if (PathFinder.CanMove(position, newStep))
                        {
                            step = newStep;
                            currentDirection = DirectionKeys.Right;
                            Animation = AnimationFactory.CreateAnimation(AnimationType.PacmanRight);
                            Animation.Location = position;
                        }
                    }
                }
            }
        }

        public override void Update()
        {
            ExistTime();
            if (PathFinder.CanMove(Animation.Location, step))
            {
                Animation.Location += step;

                if (Animation.Location.X >= Coordinate.WorldWidth)
                    Animation.Location = new Coordinate(0, Animation.Location.Y);
                else if (Animation.Location.X < 0)
                    Animation.Location = new Coordinate(Coordinate.WorldWidth, Animation.Location.Y);
            }
        }
        public void Collide(IEnumerable<IGameObject> collisions)
        {
            foreach (var objCoin in collisions) { 
                if (objCoin.Name == ObjectNames.Coin)
                { objCoin.IsEnabled = false; }
            }
            foreach (var objBigCoin in collisions)
                if (objBigCoin.Name == ObjectNames.BigCoin)
                {
                    BigCoinTimerOnOff = true;
                    objBigCoin.IsEnabled = false;
                    Manager.Instance.BigCoinEatenByPacman(BigCoinTimerOnOff);
                }
            foreach (var objGhost in collisions) { 
                if (objGhost.Name == ObjectNames.Ghost) { 
                    Manager.Instance.GhostEatenByPacman();
                }
            }
        }

        public void ExistTime() {
            if (BigCoinTimerOnOff)
                BigCoinTimer += 1;
            if (BigCoinTimer == 300) {
                BigCoinTimerOnOff = false;
                BigCoinTimer = 0;
                Manager.Instance.BigCoinEatenByPacman(BigCoinTimerOnOff);
            }
        }
    }
}
