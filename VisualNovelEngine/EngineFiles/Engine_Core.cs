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
    /// This is the main type for your game.
    /// </summary>
    public class Engine_Core : Game
    {
        //Graphics Variables
        public GraphicsDeviceManager graphics;
        public List<Sprite> drawStack;
        public List<VideoRender> videoDrawStack;
        SpriteBatch spriteBatch;
        Video video;
        VideoPlayer player;

        //Class References
        public Engine_Updater updater;
        public Engine_State_Manager stateManager;

        public Engine_Core()
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

            //initialize the draw stack, allowing classes to pass content to be drawn to this list
            drawStack = new List<Sprite>() { };

            //initialize a special video draw stack allows video render updates
            videoDrawStack = new List<VideoRender>() { };

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
            //Console.WriteLine("test");
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();

            //Draw each item in the sprite draw stack (TO-DO: Add multi-threading capability)
            foreach(Sprite item in drawStack)
            {
                spriteBatch.Draw(item.Texture, item.Size, item.Color);
                //Console.WriteLine(item);
                //Texture2D test = new Texture2D(graphics, 0,0);
            }
            //Draw each item in the video draw stack (TO-DO: Research better performance maintainability)
            foreach (VideoRender videoItem in videoDrawStack)
            {
                spriteBatch.Draw(videoItem.Render.Texture, videoItem.Render.Size, videoItem.Render.Color);
                UpdateVideo(videoItem);
                
            }
            
            
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        //Begin rendering a video to the draw stack
        public void PlayVideo(String videoToLoad, Sprite spriteObject)
        {
            //Load the passed in video and trigger it to play
            video = Content.Load<Video>(videoToLoad);
            
            //Create a new video render object and have it begin playing
            VideoRender videoRender = new VideoRender() { Render = spriteObject, Player = new VideoPlayer() };
            videoRender.Player.Play(video);

            //Add the video object to the draw stack
            videoRender.Render.Texture = videoRender.Player.GetTexture();
            videoDrawStack.Add(videoRender);

            //TO-DO: Add delegate call when video stops to the object that called this
            
        }

        //Retrieve the next video frame to render
        protected void UpdateVideo(VideoRender itemToUpdate)
        {
            //Update item to have the next frame to be drawn
            itemToUpdate.Render.Texture = itemToUpdate.Player.GetTexture();
        }
    }
}
