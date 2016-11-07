using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HexFight
{
    public class Player
    {
        string name;
        List<Card> cards = new List<Card>();
        Deck[] decks = new Deck[3];

        /// <summary>
        /// Gets called if a new Player gets createt.
        /// If a player gets reloaded, the instance gets createt from the FileStream
        /// </summary>
        public Player(string pName)
        {
            name = pName;
            //add default cards to list
            //add default deck to array
        }

    }
}