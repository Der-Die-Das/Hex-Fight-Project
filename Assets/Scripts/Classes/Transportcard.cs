using UnityEngine;
using System.Collections;

namespace HexFight
{
    public class Transportcard : Card
    {
        // Member-Attribute
        private int capacity;

        // Konstruktoren
        public Transportcard(string pname, int php, int plevel, int pdamage, int pmoveRange, int pminAttackRange, int pmaxAttackRange, Type ptype, int pcapacity)
            : base(pname, php, plevel, pdamage, pmoveRange, pminAttackRange, pmaxAttackRange, ptype)
        {
            capacity = pcapacity;
        }
    }
}
