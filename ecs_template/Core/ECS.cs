using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alchemist_mono
{
    public static class ECS
    {
        static int lastEid = 0;
        static int maxEids = 50000;
        
        static List<int> eids;
        static Dictionary<int, List<Component>> componentsByEid;
        static Dictionary<string, List<int>> eidsByComponent;

        public static List<int> Entities
        {
            get
            {
                return eids;
            }
        }

        static ECS()
        {
            eids = new List<int>();
            componentsByEid = new Dictionary<int, List<Component>>();
            eidsByComponent = new Dictionary<string, List<int>>();
        }

        public static void RegisterComponent<T>() where T : Component
        {
            eidsByComponent[typeof(T).Name] = new List<int>();
        }

        public static int CreateEntity()
        {
            if (eids.Count >= maxEids)
                throw new Exception("Too much entities, something's wrong");

            int eid = ++lastEid;
            eids.Add(eid);
            componentsByEid[eid] = new List<Component>();
            return eid;
        }

        public static void AddComponent(int eid, Component component)
        {
            if (!componentsByEid[eid].Contains(component))
            {
                componentsByEid[eid].Add(component);
                eidsByComponent[component.GetType().Name].Add(eid);
            }
        }

        public static List<int> GetEntitiesWithComponent<T>() where T : Component
        {
            string name = typeof(T).Name;
            if (eidsByComponent.ContainsKey(name))
                return eidsByComponent[name];
            return null;
        }

        public static T GetComponent<T>(int eid) where T : Component
        {
            List<Component> comps = componentsByEid[eid];
            foreach (Component c in comps)
                if (typeof(T) == c.GetType())
                    return (T)c;
            return default(T);
        }
    }
}
