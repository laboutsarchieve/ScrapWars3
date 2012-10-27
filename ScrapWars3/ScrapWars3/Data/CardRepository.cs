using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Logic.Cards;

namespace ScrapWars3.Data
{
    static class CardRepository
    {
        static Random rng = new Random();
        static List<Card> cards = new List<Card>( );

        static CardRepository( )
        {
            cards.Add(new AttackWeakest());
            cards.Add(new LargeShells());
            cards.Add(new BoostMove());
            cards.Add(new OverDrive());
            cards.Add(new StayStill());
        }

        public static Card GetRandomCard()
        {
            return cards[rng.Next(cards.Count)];
        }
    }
}
