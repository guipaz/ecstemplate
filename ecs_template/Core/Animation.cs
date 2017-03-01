using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alchemist_mono
{
    public class Animation
    {
        public List<Sprite> sprites;
        public int frames;

        public Animation(int frames)
        {
            this.frames = frames;
            sprites = new List<Sprite>(frames);
        }
    }
}
