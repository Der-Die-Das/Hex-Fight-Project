using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace HexFight
{
    [Serializable]
    public class Card : MonoBehaviour
    {
        // Member-Attributes
        //maybe add custom editor to handle properties
        public string cardName;
        public int hp;
        private int level;
        public int damage;
        public int moveRange;
        public int minAttackRange;
        public int maxAttackRange;
        public Type type;
        private HexCoordinates position;        

        [Serializable]
        public struct Type
        {
            public static List<Type> allTypes = new List<Type>();
            public string name;
            public List<string> strongAgainst; //string because of upcoming intialization issues
            public List<string> weakAgainst;

            public Type(string pName)
            {
                name = pName;
                strongAgainst = new List<string>();
                weakAgainst = new List<string>();
            }
        }
        void Awake()
        {
            if (cardName == "" || hp == 0|| damage == 0 || moveRange == 0 || minAttackRange == 0 || maxAttackRange == 0 || type.name == "")
            {
                throw new Exception("Not all Parameters of this Card are set.");
            }
        }

        public bool MoveInDirection(int direction){
            return false;
        }
        public bool Attack(HexCoordinates position)
        {
            return false;
        }
        public void Die()
        {

        }
        private bool EnterTransportCard(Transportcard card)
        {
            return false;
        }
        private bool LeaveTransportCard(Transportcard card)
        {
            return false;
        }

    }
}
