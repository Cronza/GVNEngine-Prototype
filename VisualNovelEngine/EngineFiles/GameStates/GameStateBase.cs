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

namespace VisualNovelEngine.EngineFiles.GameStates
{
    interface IGameStateBase
    {
        void Main();
        void EndState();
        void ChangeState();
        void UpdateDrawCalls();
    }
}
