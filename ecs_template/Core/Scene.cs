using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alchemist_mono
{
    public abstract class Scene
    {
        List<System> systems;

        float tickDelta = 0;
        const int ticksPerSecond = 20;
        float ticksCooldown = 1f / ticksPerSecond;

        public Scene()
        {
            systems = new List<System>();
        }

        public void AddSystem(System system)
        {
            systems.Add(system);
        }

        public abstract void Load();

        public virtual void Update(float delta)
        {
            foreach (System system in systems)
                system.Update(delta);

            tickDelta += delta;
            while (tickDelta > ticksCooldown)
            {
                tickDelta -= ticksCooldown;

                foreach (System system in systems)
                    system.Tick();
            }
        }

        public virtual void Draw(float delta)
        {
            Global.SpriteBatch.Begin();

            foreach (System system in systems)
                system.Draw(delta);
            
            Global.SpriteBatch.End();
        }
    }
}
