using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTools.Events;
using ScrapWars3.Data.Event;
using Microsoft.Xna.Framework.Audio;
using ScrapWars3.Resources;

namespace ScrapWars3.View
{
    class SoundEffectPlayer
    {
        public SoundEffectPlayer( )
        {
            ScrapWarsEventManager.GetManager( ).Subscribe(this, MakeSound, "BulletHitMech");
        }
        public bool MakeSound( BaseGameEvent theEvent )
        {
            if(theEvent is BulletHitMechEvent)
            { 
                SoundEffectInstance sound = SoundRepo.basicBulletHit.CreateInstance( );
                sound.Volume = 0.025f;
                sound.Play( );
            }

            return false;
        }
    }
}
