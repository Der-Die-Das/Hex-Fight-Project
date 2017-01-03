using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;

namespace HexFight
{
    [Serializable]
    public class Deck
    {
        public string name { get; private set; }
        public Tuple<string, int>[] cards { get; private set; }

        public Deck(string pName)
        {
            name = pName;
            cards = new Tuple<string, int>[10];
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
                if(cards[i] == null) 
                {
                    //cards[i] = new Tuple<string, int>(card.name, card.level);
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
        public bool removeCard(Tuple<string, int> card)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if(cards[i] == card) 
                {
                    cards[i] = null;
                    return true;
                }
            }
            return false;
        }

        public void Clear() 
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = null;
            }
        }

        public void ChangeName(string newName) 
        {
            name = newName;
        }
    }
}
