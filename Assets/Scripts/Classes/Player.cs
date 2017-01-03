using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace HexFight
{
    [Serializable]
    public class Player
    {
        public string name { get; private set; }
        public List<Tuple<string, int>> cards { get; private set; }
        public Deck[] decks { get; private set; }
        public int level { get; set; }
        public List<Card> playedCards { get; private set; }

        /// <summary>
        /// Gets called if a new Player gets createt.
        /// If a player gets reloaded, the instance gets createt from the FileStream
        /// </summary>
        public Player(string pName)
        {
            name = pName;
            decks = new Deck[3] { new Deck("Slot 1"), new Deck("Slot 2"), new Deck("Slot 3") };
            level = 1;
            playedCards = new List<Card>();
        }

        //public bool PlayCard(Tuple<string, int> card/*, HexCoordinates position*/) 
        //{
        //     var cell = Gameobjectblabla.Map.GetCellbyPosition(position);

        //     if(cell.placedObject != null) {
        //        return false;
        //     }

        //     instantiate Card and place on Cell
        //     return true;
        //}

    }
}