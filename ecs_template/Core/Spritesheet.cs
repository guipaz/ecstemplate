using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace alchemist_mono
{
    public class SpriteSheet
    {
        static Dictionary<string, SpriteSheet> sheets = new Dictionary<string, SpriteSheet>();

        public string Name { get; set; }
        public Dictionary<string, SpriteFrame> Frames { get; set; }

        public SpriteSheet(string name)
        {
            this.Name = name;
            this.Frames = new Dictionary<string, SpriteFrame>();
        }

        public static SpriteSheet Load(string xmlFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Content/" + xmlFile);
            
            XmlNode atlas = doc["TextureAtlas"];
            string imagePath = atlas.Attributes["imagePath"].Value;

            SpriteSheet sheet = new SpriteSheet(GetNameWithoutExtension(imagePath));
            foreach (XmlNode spriteNode in atlas)
            {
                string name = GetNameWithoutExtension(spriteNode.Attributes["n"].Value);
                SpriteFrame frame = new SpriteFrame(name);

                int x = Convert.ToInt32(spriteNode.Attributes["x"].Value);
                int y = Convert.ToInt32(spriteNode.Attributes["y"].Value);
                int w = Convert.ToInt32(spriteNode.Attributes["w"].Value);
                int h = Convert.ToInt32(spriteNode.Attributes["h"].Value);

                frame.source = new Rectangle(x, y, w, h);
                frame.texture = TextureLoader.Load(sheet.Name);
                sheet.Frames[frame.name] = frame;
            }

            sheets[sheet.Name] = sheet;

            return sheet;
        }

        public static SpriteSheet Get(string name)
        {
            if (sheets.ContainsKey(name))
                return sheets[name];
            return null;
        }

        public static string GetNameWithoutExtension(string nameWithExtension)
        {
            return nameWithExtension.Substring(0, nameWithExtension.LastIndexOf('.'));
        }
    }
}
