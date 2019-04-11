using System;
using Microsoft.Xna.Framework;
using GVNEngine.EngineFiles.UI.InGame;
using GVNEngine.EngineFiles.Collections;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GVNEngine.EngineFiles.GameStates
{
    class Front_End : IGame_State_Base
    {

        //Variables 
        private Engine_Core engCoreRef;
        private IGame_State_Base myInterface;

        //U.I Variables
        private Front_End_UI frontEndUI;

        //Audio Variables
        public Song song;

        /// <summary>
        /// Initialization Method
        /// </summary>
        public Front_End(Engine_Core coreRef)
        {
            //Cast to the interface in order to call interface functions
            myInterface = this;

            //Retain a reference to the Engine_Core
            engCoreRef = coreRef;

            //Generate the U.I Class
            frontEndUI = new Front_End_UI(this, engCoreRef, myInterface);

            //Run the core state function
            Console.WriteLine("Activating Front-End!");
            myInterface.Main();

        }

        /// <summary>
        /// The main function for the state
        /// </summary>
        void IGame_State_Base.Main()
        {
            //Reveal mouse cursor
            engCoreRef.IsMouseVisible = true;

            //------------------------AUDIO TEST----------------------------
            song = engCoreRef.Content.Load<Song>("Music/Front_End_Test");
            MediaPlayer.Play(song);

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        void IGame_State_Base.Update(GameTime gametime)
        {
            //Update the U.I
            frontEndUI.UpdateUI();
        }

        /// <summary>
        /// Change the state by passing a state ID back to the state manager
        /// </summary>
        void IGame_State_Base.ChangeState(Engine_State_Manager.GameState newState)
        {
            //Clean up the created objects before transitioning
            myInterface.CleanUp();

            //Called the state managers update method, giving it the state code
            engCoreRef.stateManager.UpdateGameState(newState);
        }

        /// <summary>
        /// Update the items to be drawn that were passed in by this class
        /// </summary>
        void IGame_State_Base.UpdateDrawCalls()
        {

        }

        /// <summary>
        /// Destroys all objects created during the execution of this state
        /// </summary>
        void IGame_State_Base.CleanUp()
        {
            //Destroy any objects created by the front end
            frontEndUI.CleanUp();

            //Unload all assets that have been loaded through the content pipeline
            engCoreRef.Content.Unload();
        }
    }

    /// <summary>
    /// The class that owns the U.I elements for the Front End state
    /// </summary>
    class Front_End_UI
    {
        //Variables
        private Front_End state;
        private Engine_Core engCoreRef;
        private IGame_State_Base stateFunctions;

        //Buttons
        public Base_Button startGameButton;
        public Base_Button exitGameButton;

        //Initialization method
        public Front_End_UI(Front_End curState, Engine_Core coreRef, IGame_State_Base stateInterface)
        {
            //Store references
            stateFunctions = stateInterface;
            engCoreRef = coreRef;
            state = curState;

            //---------------GENERATE BUTTONS---------------

            //Create the Start Game Button
            Base_Button.ClickEvent clickEvent = GoToGame;
            startGameButton = new Base_Button(
            new Sprite()
            {
                Texture = engCoreRef.Content.Load<Texture2D>("UI/Front_End/Test_Button"),
                Color = Color.White,
                Size = new Rectangle(engCoreRef.GraphicsDevice.Viewport.Width / 2 - 128, (engCoreRef.GraphicsDevice.Viewport.Height / 2) - 32, 256, 64)
            },
            new TextSprite()
            {
                Font = engCoreRef.Content.Load<SpriteFont>("UI/Fonts/Menu_Button_01"),
                Text = "Start Game",
                Color = Color.White
            },
            clickEvent);

            //Create the Exit Game Button
            clickEvent = ExitGame;
            exitGameButton = new Base_Button(
            new Sprite()
            {
                Texture = engCoreRef.Content.Load<Texture2D>("UI/Front_End/Test_Button"),
                Color = Color.White,
                Size = new Rectangle(engCoreRef.GraphicsDevice.Viewport.Width / 2 - 128, (engCoreRef.GraphicsDevice.Viewport.Height / 2) + (engCoreRef.GraphicsDevice.Viewport.Height / 6) - 32, 256, 64)
            },
            new TextSprite()
            {
                Font = engCoreRef.Content.Load<SpriteFont>("UI/Fonts/Menu_Button_01"),
                Text = "Exit Game",
                Color = Color.White
            },
            clickEvent);
            
            //Add the buttons to the draw stack
            engCoreRef.drawStack.Add("Start_Game_Button", startGameButton.myArt);
            engCoreRef.textDrawStack.Add("Start_Game_Button_Text", startGameButton.myText);
            engCoreRef.drawStack.Add("Exit_Game_Button", exitGameButton.myArt);
            engCoreRef.textDrawStack.Add("Exit_Game_Button_Text", exitGameButton.myText);

        }
        
        /// <summary>
        /// Updates each U.I element and checks inputs for buttons
        /// </summary>
        public void UpdateUI()
        {
            startGameButton.CheckState();
            exitGameButton.CheckState();
        }

        /// <summary>
        /// Used to transition into the "Game" state
        /// </summary>
        public void GoToGame()
        {
            stateFunctions.ChangeState(Engine_State_Manager.GameState.Game);    
        }

        /// <summary>
        /// Used to transition into the "Game" state
        /// </summary>
        public void ExitGame()
        {
            engCoreRef.Exit();
        }

        /// <summary>
        /// Cleans up the objects created by the U.I
        /// </summary>
        public void CleanUp()
        {
            engCoreRef.RemoveDrawItem("Start_Game_Button", "sprite");
            engCoreRef.RemoveDrawItem("Exit_Game_Button", "sprite");
            engCoreRef.RemoveDrawItem("Start_Game_Button_Text", "text");
            engCoreRef.RemoveDrawItem("Exit_Game_Button_Text", "text");
            MediaPlayer.Stop();
            state.song = null;
        }
    }
}
