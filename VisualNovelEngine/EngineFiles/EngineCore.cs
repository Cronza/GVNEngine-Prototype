using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;

namespace VisualNovelEngine.EngineFiles

{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class EngineCore : Game
    {
        //Variables
        Texture2D bucket;
        Vector2 bucketPosition;
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Class References
        Engine_Updater updater;

        public EngineCore()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Create the Window Update Class / Initialize default window properties
            updater = new Engine_Updater(this);
            updater.UpdateWindowSettings(true, null);

            // TODO: Add your initialization logic here
            bucketPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bucket = Content.Load<Texture2D>("bucket");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Console.WriteLine("test");
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
                Console.WriteLine("Pressed");

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //spriteBatch.Begin();
            //spriteBatch.Draw(
            //    bucket, 
            //    bucketPosition, 
            //    null,
            //    Color.White,
            //    0f,
            //    new Vector2(bucket.Width / 2, bucket.Height / 2),
            //    Vector2.One,
            //    SpriteEffects.None,
            //    0f
            //    );
            //spriteBatch.End();

            base.Draw(gameTime);
        }

        ////Initialize the default settings of the engine window || Update the window settings
        //protected void UpdateWinSettings(bool setDefault, string[] settings)
        //{
        //    //Get the Path to the engine properties file
        //    string filePath = System.IO.Directory.GetCurrentDirectory();
        //    filePath = filePath.Replace(@"bin\DesktopGL\AnyCPU\Debug", @"properties\") + "engine_properties.xml";

        //    XmlReader reader;
        //    reader = XmlReader.Create(filePath);

        //    while(reader.Read())
        //    {
        //        if((reader.Name == "Resolution"))
        //        {
        //            //reader.ReadToFollowing
        //            reader.ReadToFollowing("width");
        //            Console.WriteLine(reader.ReadInnerXml());
        //            reader.ReadToFollowing("height");
        //            Console.WriteLine(reader.ReadInnerXml());
        //        }
        //    }
        //}
    }
}
