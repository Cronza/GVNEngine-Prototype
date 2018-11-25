using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelEngine.EngineFiles.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using VisualNovelEngine.EngineFiles.UI.InGame;
using VisualNovelEngine.EngineFiles.Utilities;

namespace VisualNovelEngine.EngineFiles.GameStates
{
    class Dialogue : IGame_State_Base
    {
        //Variables 
        private Engine_Core engCoreRef;
        IGame_State_Base myInterface;

        //U.I Variables
        private Dialogue_UI dialogueBox;
        
        /// <summary>
        /// Initialization Method
        /// </summary>
        public Dialogue(Engine_Core coreRef)
        {
            //Cast to the interface in order to call interface functions
            myInterface = this;

            //Retain a reference to the Engine_Core
            engCoreRef = coreRef;
            
            //Run the core state function
            Console.WriteLine("Activating Dialogue State!");
            myInterface.Main();
        }

        /// <summary>
        /// The main function for the state
        /// </summary>
        void IGame_State_Base.Main()
        {
            //Create the Dialogue Box
            dialogueBox = new Dialogue_UI(engCoreRef);
            //dialogueBox.AddCharacter("Jessica");
            StoryReader reader = new StoryReader();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        void IGame_State_Base.Update(GameTime gameTime)
        {
            
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
            
        }
    }
}
