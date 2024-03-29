﻿/*
Author: Garrett Fredley
Purpose: State manager for the engine. This is where the engine states are recorded and managed. State ID's are passed in and parsed for the relevant 
         state. Once found, this generates the associated state class to perform its functionality. At any given time, another ID can be passed in to update
         the engine state.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GVNEngine.EngineFiles.GameStates;

namespace GVNEngine.EngineFiles
{
    public class Engine_State_Manager
    {
        //Variables
        public IGame_State_Base activeGameState;
        private Engine_Core engCoreRef;

        /// <summary>
        /// Store a passed-in reference to the engine core
        /// </summary>
        public Engine_State_Manager(Engine_Core coreRef)
        {
            //Retain a reference to the engine core
            engCoreRef = coreRef;
        }

        /// <summary>
        /// Simplified ID system for indentifying game state. 
        /// </summary>
        public enum GameState
        {
            Splash,
            FrontEnd,
            Loading,
            Game,
            Credits,
        }

        //Using a given state, update the engine to the new state (I.E switch to game view mode | front-end mode)
        public void UpdateGameState(GameState newState)
        {
            //Destroy the previous state by clearing its reference (Garbage collector will pick it up)
            activeGameState = null;
            
            //Update state record to point to the new state object
            switch (newState)
            {
                case GameState.Splash:
                { 
                    activeGameState = new Splash_Screen(engCoreRef);
                    break;
                }
                case GameState.FrontEnd:
                { 
                    activeGameState = new Front_End(engCoreRef);
                    break;
                }
                case GameState.Game:
                {
                    activeGameState = new Dialogue(engCoreRef);
                    break;
                }
            }
        }
    }
}
