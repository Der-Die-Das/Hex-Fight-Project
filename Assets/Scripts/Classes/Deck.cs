using UnityEngine;
using System.Collections;

namespace HexFight
{
    public class Deck
    {
        string name;
        Card[] cards = new Card[10];

        public Deck(string pName)
        {
            name = pName;
        }

        /// <summary>
        /// Adds a Card to the Deck.
        /// </summary>
        /// <param name="card">The Card you want to add.</param>
        /// <returns>Returns if the Card got added.</returns>
        public bool addCard(Card card)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == null)
                {
                    cards[i] = card;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Removes a card from the Deck.
        /// </summary>
        /// <param name="card">Thge Card you want to remove.</param>
        /// <returns>Returns if the Card got removed.</returns>
        public bool removeCard(Card card)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == card)
                {
                    cards[i] = null;
                    return true;
                }
            }
            return false;
        }
    }
}
