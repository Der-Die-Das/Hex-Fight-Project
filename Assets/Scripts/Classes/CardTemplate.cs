using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace HexFight
{
    [Serializable]
    public class CardTemplate
    {
        // Member-Attributes
        public static List<CardTemplate> allCardTemplates = new List<CardTemplate>();
        public string cardName;
        public int hp;
        public int level;
        public int damage;
        public int moveRange;
        public int minAttackRange;
        public int maxAttackRange;
        public string typeName;
        public string imageSource;

        /// <summary>
        /// For xml Serialization only
        /// </summary>
        public CardTemplate(){}

        public CardTemplate(string pCardName, int pHp, int pDamage, int pMoveRange, int pMinAttackRange, int pMaxAttackRange, string pTypeName)
        {
            cardName = pCardName;
            hp = pHp;
            damage = pDamage;
            moveRange = pMoveRange;
            minAttackRange = pMinAttackRange;
            maxAttackRange = pMaxAttackRange;
            typeName = pTypeName;

            allCardTemplates.Add(this);
        }
    }
}
