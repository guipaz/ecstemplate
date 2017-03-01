using Microsoft.Xna.Framework;
using System;

namespace alchemist_mono
{
    public abstract class Component
    {
    }

    public class CProfileRenderer : Component
    {
        public Profiler profiler;
        public int currentAnimation;
        public int currentFrame;
    }

    public class CSpriteRenderer : Component
    {
        public Sprite sprite;
    }

    public class CPhysics : Component
    {
        public Pos position;
        public bool playerControlled;

        public Point queuedMovement;
        public bool isMoving;
        public float moveDelta;
        public int tilePerSecond = 3;
    }

    public class CSimpleAI : Component
    {

    }
}
