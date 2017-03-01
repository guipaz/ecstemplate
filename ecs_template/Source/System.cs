using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alchemist_mono
{
    public abstract class System
    {
        public virtual void Draw(float delta) { }
        public virtual void Update(float delta) { }
        public virtual void Tick() { }
    }

    public class RenderSystem : System
    {
        public override void Draw(float delta)
        {
            foreach (int entity in ECS.Entities)
            {
                CSpriteRenderer renderer = ECS.GetComponent<CSpriteRenderer>(entity);
                CPhysics physics = ECS.GetComponent<CPhysics>(entity);

                if (renderer == null || physics == null)
                    continue;

                renderer.sprite.position = physics.position;

                if (physics.isMoving)
                {
                    Point move = physics.queuedMovement;
                    move.X = (int)(move.X * physics.moveDelta * Pos.TileSize);
                    move.Y = (int)(move.Y * physics.moveDelta * Pos.TileSize);
                    renderer.sprite.position += move;
                }

                renderer.sprite.Draw();
            }
        }
    }

    public class PhysicsSystem : System
    {
        public override void Update(float delta)
        {
            foreach (int entity in ECS.GetEntitiesWithComponent<CPhysics>())
            {
                CPhysics physics = ECS.GetComponent<CPhysics>(entity);
                
                if (physics.isMoving)
                    HandleMovement(delta, physics);

                if (physics.isMoving)
                    continue;

                int x = 0, y = 0;
                if (physics.playerControlled)
                {
                    if (Input.IsKeyDown(Keys.Down))
                        y++;
                    if (Input.IsKeyDown(Keys.Up))
                        y--;
                    if (Input.IsKeyDown(Keys.Left))
                        x--;
                    if (Input.IsKeyDown(Keys.Right))
                        x++;
                }
                else
                {
                    x = physics.queuedMovement.X;
                    y = physics.queuedMovement.Y;
                }

                if (x != 0 || y != 0)
                {
                    physics.queuedMovement = new Point(x, y);
                    physics.isMoving = true;
                }

                if (physics.isMoving)
                    HandleMovement(delta, physics);
            }
        }

        void HandleMovement(float delta, CPhysics physics)
        {
            if (physics.moveDelta >= 1)
            {
                physics.moveDelta = 0;
                physics.isMoving = false;
                physics.position += new Pos(physics.queuedMovement.X, physics.queuedMovement.Y);
                return;
            }

            physics.moveDelta += delta * physics.tilePerSecond;
        }
    }

    public class SimpleAISystem : System
    {
        public override void Draw(float delta) { }

        public override void Update(float delta)
        {
            foreach (int entity in ECS.GetEntitiesWithComponent<CSimpleAI>())
            {
                CSimpleAI ai = ECS.GetComponent<CSimpleAI>(entity);
                CPhysics physics = ECS.GetComponent<CPhysics>(entity);

                if (physics == null)
                    continue;

                if (physics.isMoving)
                    continue;

                physics.isMoving = true;
                physics.queuedMovement = new Point(1, 0);
            }
        }
    }
}
