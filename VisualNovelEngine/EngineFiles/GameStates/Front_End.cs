using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using VisualNovelEngine.EngineFiles.UI.InGame;
using VisualNovelEngine.EngineFiles.Collections;
using Microsoft.Xna.Framework.Graphics;

namespace VisualNovelEngine.EngineFiles.GameStates
{
    class Front_End : IGame_State_Base
    {

        //Variables 
        private Engine_Core engCoreRef;
        private IGame_State_Base myInterface;

        //U.I Variables
        private Front_End_UI frontEndUI;

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
            Console.WriteLine("Running Front End code");

            //Reveal mouse cursor
            engCoreRef.IsMouseVisible = true;

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
        }
    }

    /// <summary>
    /// The class that owns the U.I elements for the Front End state
    /// </summary>
    class Front_End_UI
    {
        //Variables
        //private Front_End state;
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

            //---------------GENERATE BUTTONS------------
            
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
        }
    }
}
