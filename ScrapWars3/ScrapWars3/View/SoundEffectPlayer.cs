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
            SoundEffectInstance sound = null;

            switch(theEvent.EventType)
            { 
                case "BulletHitMech":
                    sound = SoundRepo.basicBulletHit.CreateInstance( );
                    sound.Volume = (float)Math.Min(1.0,0.01 * Math.Pow(2,((BulletHitMechEvent)theEvent).Bullet.BulletScale));
                    break;
                default:
                    sound = SoundRepo.basicBulletHit.CreateInstance();
                    sound.Volume = 0.0f;
                    break;
            }

            sound.Play();

            return false;
        }
    }
}
