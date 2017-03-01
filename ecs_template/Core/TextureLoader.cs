using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alchemist_mono
{
    public class TextureLoader
    {
        static Dictionary<string, Texture2D> loaded = new Dictionary<string, Texture2D>();

        public static GraphicsDevice GraphicsDevice { get; internal set; }

        public static Texture2D FromStream(Stream stream)
        {
            Texture2D texture = Texture2D.FromStream(GraphicsDevice, stream);
            Color[] data = new Color[texture.Width * texture.Height];
            texture.GetData(data);
            for (int i = 0; i != data.Length; ++i)
                data[i] = Color.FromNonPremultiplied(data[i].ToVector4());
            texture.SetData(data);
            return texture;
        }

        public static Texture2D Load(string name)
        {
            if (loaded.ContainsKey(name))
                return loaded[name];

            Texture2D texture = null;
            using (FileStream stream = File.OpenRead("Content/" + name + ".png"))
            {
                texture = FromStream(stream);
            }

            if (texture != null)
                loaded[name] = texture;

            return texture;
        }
    }
}
