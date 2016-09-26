using UnityEngine;
using System.Collections;

namespace HexFight
{
    public class Card : MonoBehaviour
    {
        // Member-Attributes
        private string name;
        private int hp;
        private int level;
        private int damage;
        private int moveRange;
        private int minAttackRange;
        private int maxAttackRange;
        private Type type;
        //Hex-Coordinates
        public enum Partition { Offense,Defense};
        private Partition partition;
        

        public struct Type
        {
            private string name;
            private Type[] strongAgainst;
            private Type[] weakAgainst;

        }

        // Konstruktoren
        public Card(string pname, int php, int plevel, int pdamage, int pmoveRange, int pminAttackRange, int pmaxAttackRange, Type ptype)
        {
            name = pname;
            hp = php;
            level = plevel;
            damage = pdamage;
            moveRange = pmoveRange;
            minAttackRange = pminAttackRange;
            maxAttackRange = pmaxAttackRange;
            type = ptype;
        }


        // Member-Methoden
    }
}
