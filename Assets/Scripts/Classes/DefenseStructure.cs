using UnityEngine;
using System.Collections;
using System;

namespace HexFight
{
    public class DefenseStructure : MonoBehaviour
    {

        // Member-Attributes
        //maybe add custom editor to handle properties
        public string structureName;
        public int hp;
        private int level;
        public int damage;
        public int minAttackRange;
        public int maxAttackRange;
        public Card.Type type;
        private HexCoordinates position;

        void Awake()
        {
            if (structureName == "" || hp == 0 || damage == 0 || minAttackRange == 0 || maxAttackRange == 0 || type.name == "")
            {
                throw new Exception("Not all Parameters of this Structure are set.");
            }
        }
        private Card EnemyInRange()
        {
            return null;
        }
        public bool Attack(Card pCard)
        {
            //pCard.hp -= this.damage * this.type.GetDamageMulitplicator(pCard.type);
            return false;
        }
        public void Die()
        {

        }

    }

}
