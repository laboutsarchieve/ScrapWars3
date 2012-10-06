using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars3.Logic.Behaviors
{
    class DummyBehavior : BehaviorState
    {
        public void Update(MechAiStateMachine stateMachine, Microsoft.Xna.Framework.GameTime gameTime, Screens.Battle battle)
        {
            // Do nothing
        }

        public void EnterState(MechAiStateMachine stateMachine, Screens.Battle battle)
        {
            
        }

        public void ExitState(MechAiStateMachine stateMachine, Screens.Battle battle)
        {
            
        }
    }
}
