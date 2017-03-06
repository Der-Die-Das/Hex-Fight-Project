using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexFight
{
    public class AI {
        #region fields
        private List<DefenseStructure> objectsToControl;
        private static AI Instance;
        #endregion

        #region constructor
        private AI(List<DefenseStructure> pobjectsToControl)
        {
            objectsToControl = pobjectsToControl;
        }
        #endregion

        #region methods
        public AI CreateGet(List<DefenseStructure> pobjectsToControl)
        {
            if (Instance == null)
                Instance = new HexFight.AI(pobjectsToControl);
            return Instance;
        }

        public void DoTurn()
        {
            foreach (var item in objectsToControl)
            {
                Card iteminrange = item.EnemyInRange();
                if(iteminrange != null)
                {
                    item.Attack(iteminrange);
                }
            }
        }
        #endregion
    }
}
