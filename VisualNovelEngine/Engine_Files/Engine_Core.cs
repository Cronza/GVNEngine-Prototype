/*
Author: Garrett Fredley
Purpose: The core engine script that manages and executes core engine functionality. The engine passes in information to this script to be executed such
         as content to be drawn, while passing out information to the relevant classes such as input information to the input manager

 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml;
using VisualNovelEngine.EngineFiles.Collections;


namespace VisualNovelEngine.EngineFiles

{
    /// <summary>
    /// This is the main class for the game.
    /// </summary>
    public class Engine_Core : Game
    {
        //Graphics Variables
        public GraphicsDeviceManager graphics;
        public Dictionary<string, Sprite> drawStack;
        public Dictionary<string, TextSprite> textDrawStack;
        public Dictionary<string, VideoRender> videoDrawStack;
        private SpriteBatch spriteBatch;
        private Video video;
        private VideoPlayer player;

        //Class References
        public Engine_Updater updater;
        public Engine_State_Manager stateManager;

        //Events
        //public event coreEventHandler OnVideoStop;

        //Main Function
        public Engine_Core()
        {
            //Initialize the graphics manager
            graphics = new GraphicsDeviceManager(this);

            //Point to the proper location for game content
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

            //initialize the draw stack, allowing classes to pass sprites to be drawn to this list
            drawStack = new Dictionary<string, Sprite>();

            //initialize the text draw stack, allowing text to be drawn
            textDrawStack = new Dictionary<string, TextSprite>();

            //initialize a special video draw stack allows video render updates
            videoDrawStack = new Dictionary<string, VideoRender>();

            //Initialize a video player to render videos
            player = new VideoPlayer();

            //Create the Engine State Manager
            stateManager = new Engine_State_Manager(this);

            //Start the Engine with the Splash Screen State
            stateManager.UpdateGameState(Engine_State_Manager.GameState.Splash);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            base.Update(gameTime);
            stateManager.activeGameState.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Clear the graphics
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //Begin the batch for filling
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            
            //Draw each item in the sprite draw stack (TO-DO: Add multi-threading capability)
            foreach(KeyValuePair<string, Sprite> item in drawStack)
            {
                spriteBatch.Draw(item.Value.Texture, item.Value.Size, null, item.Value.Color, 0.0f, new Vector2(0,0), SpriteEffects.None, item.Value.RenderLayer);
            }
            //Draw each item in the text draw stack (TO-DO: Add multi-threading capability)
            foreach (KeyValuePair<string, TextSprite> item in textDrawStack)
            {
                spriteBatch.DrawString(item.Value.Font, item.Value.Text, item.Value.Pos, item.Value.Color, 0.0f, item.Value.Origin, 1.0f, SpriteEffects.None, 1.0f);
            }
            //Draw each item in the video draw stack (TO-DO: Research better performance maintainability)
            foreach (KeyValuePair<string, VideoRender> videoItem in videoDrawStack)
            {
                spriteBatch.Draw(videoItem.Value.Render.Texture, videoItem.Value.Render.Size, videoItem.Value.Render.Color);
                UpdateVideo(videoItem.Value);
            }
            
            
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        //Begin rendering a video to the draw stack
        public VideoPlayer PlayVideo(string videoToLoad, string key, Sprite spriteObject)
        {
            //Load the passed in video and trigger it to play
            video = Content.Load<Video>(videoToLoad);
            
            //Create a new video render object and have it begin playing
            VideoRender videoRender = new VideoRender() { Render = spriteObject, Player = player };
            videoRender.Player.Play(video);

            //Add the video object to the draw stack
            videoRender.Render.Texture = videoRender.Player.GetTexture();
            videoDrawStack.Add(key, videoRender);

            //Return the video player object
            return videoRender.Player;

        }
        /// <summary>
        /// Taking in whether the designated item is a video or sprite, it removes the item from the draw list via its key name
        /// </summary>
        /// <param name="key"></param>
        /// <param name="drawType"></param>
        public void RemoveDrawItem(string key, string drawType)
        {
            switch(drawType.ToLower())
            {
                case "video":
                    videoDrawStack.Remove(key);
                    break;
                case "sprite":
                    drawStack.Remove(key);
                    break;
                case "text":
                    textDrawStack.Remove(key);
                    break;
            }
        }

        /// <summary>
        /// Retrieve the next video frame to render
        /// </summary>
        /// <param name="itemToUpdate"></param>
        protected void UpdateVideo(VideoRender itemToUpdate)
        {
            //Update item to have the next frame to be drawn
            itemToUpdate.Render.Texture = itemToUpdate.Player.GetTexture();
        }
    }
}
