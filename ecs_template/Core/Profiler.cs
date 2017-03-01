using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alchemist_mono
{
    public class Profiler
    {
        public string name;
        Dictionary<string, Animation> animations;

        public Profiler()
        {
            animations = new Dictionary<string, Animation>();
        }
    }
}
