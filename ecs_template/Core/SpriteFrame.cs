using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alchemist_mono
{
    public class SpriteFrame
    {
        public string name;
        public Texture2D texture;
        public Rectangle source;

        public SpriteFrame(string name)
        {
            this.name = name;
        }
    }
}
