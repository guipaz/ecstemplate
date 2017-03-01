using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alchemist_mono
{
    public class ShackScene : Scene
    {
        public override void Load()
        {
            SpriteSheet sheet = SpriteSheet.Get("textures1");
            SpriteFrame frame = sheet.Frames["TheAlchemist_Idle_1"];
            Sprite sprite = new Sprite(frame);

            int player = ECS.CreateEntity();
            ECS.AddComponent(player, new CSpriteRenderer());
            ECS.AddComponent(player, new CPhysics());
            ECS.GetComponent<CSpriteRenderer>(player).sprite = sprite;
            ECS.GetComponent<CPhysics>(player).position = new Pos(1, 1);
            ECS.GetComponent<CPhysics>(player).playerControlled = true;

            int npc = ECS.CreateEntity();
            ECS.AddComponent(npc, new CSpriteRenderer());
            ECS.AddComponent(npc, new CPhysics());
            ECS.AddComponent(npc, new CSimpleAI());
            ECS.GetComponent<CSpriteRenderer>(npc).sprite = sprite;
            ECS.GetComponent<CPhysics>(npc).position = new Pos(5, 1);

            AddSystem(new SimpleAISystem());
            AddSystem(new PhysicsSystem());
            AddSystem(new RenderSystem());
        }
    }
}
