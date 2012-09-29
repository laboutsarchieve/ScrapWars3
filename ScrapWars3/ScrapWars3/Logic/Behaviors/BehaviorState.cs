using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars3.Screens;
using ScrapWars3.Data;

namespace ScrapWars3.Logic.Behaviors
{
    interface BehaviorState
    {
        void Update(MechAiStateMachine stateMachine, GameTime gameTime, Battle battle);
    }
}
