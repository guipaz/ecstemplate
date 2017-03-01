using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace alchemist_mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Scene scene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureLoader.GraphicsDevice = GraphicsDevice;
            Global.SpriteBatch = spriteBatch;

            // load sprites
            SpriteSheet.Load("textures1.xml");

            // initialize systems
            ECS.RegisterComponent<CSpriteRenderer>();
            ECS.RegisterComponent<CPhysics>();
            ECS.RegisterComponent<CSimpleAI>();

            scene = new ShackScene();
            scene.Load();
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float delta = gameTime.ElapsedGameTime.Milliseconds / 1000f;

            Input.Update(gameTime);
            scene.Update(delta);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            float delta = gameTime.ElapsedGameTime.Milliseconds / 1000f;

            scene.Draw(delta);

            base.Draw(gameTime);
        }
    }
}
