/*
Author: Garrett Fredley
Purpose: This script acts as a base template for all game states. It contains common functions that will be universally used. Additionally,
         given the nature of the state manager needing to retain a reference to the active state, this script is set up as an interface, allowing
         semi-ambiguous usage by the state manager
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GVNEngine.EngineFiles.GameStates
{
    public interface IGame_State_Base
    {
        void Main();
        void Update(GameTime gametime);
        void ChangeState(Engine_State_Manager.GameState newState);
        void UpdateDrawCalls();
        void CleanUp();
    }
}
