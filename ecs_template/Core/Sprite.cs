using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alchemist_mono
{
    public class Sprite
    {
        public SpriteFrame frame;
        public Point position;
        public Vector2 pivot;

        Rectangle workingRect;

        public Sprite(SpriteFrame frame)
        {
            this.frame = frame;
            pivot = new Vector2(0.5f, 0.5f);
        }

        public void Draw()
        {
            workingRect.X = (int)(position.X - frame.source.Width * pivot.X);
            workingRect.Y = (int)(position.Y - frame.source.Height * pivot.Y);
            workingRect.Width = frame.source.Width;
            workingRect.Height = frame.source.Height;

            Global.SpriteBatch.Draw(frame.texture, workingRect, frame.source, Color.White);
        }
    }
}
