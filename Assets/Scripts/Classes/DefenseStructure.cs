using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

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
        private DateTime yay;

        void Awake()
        {
            if (structureName == "" || hp == 0 || type.name == "")
            {
                throw new Exception("Not all Parameters of this Structure are set.");
            }
        }

        void Start()
        {
            yay = DateTime.Now.AddSeconds(2);
            //Card enemy = EnemyInRange();
            //if (enemy != null)
            //{
            //    Console.WriteLine(enemy.name);
            //}
            //else
            //{
            //    Console.WriteLine("nothing in Range");
            //}
        }
        void Update()
        {
            if (yay <= DateTime.Now)
            {
                yay = yay.AddSeconds(4);
                Card enemy = EnemyInRange();
                if (enemy != null)
                {
                    Debug.Log("Sniping");
                    Vector3 targetDir = enemy.transform.position - transform.position;
                    Debug.Log(targetDir);
                    float step = 5 * Time.deltaTime;
                    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                    Debug.DrawRay(transform.position + Vector3.up, newDir, Color.red);
                    Debug.DrawLine(transform.position + Vector3.up, (transform.position + Vector3.up) + (newDir * 10), Color.red, 10);
                    transform.rotation = Quaternion.LookRotation(newDir);
                }
            }
        }
        private class Tubel<T1, T2>
        {
            public T1 Item1;
            public T2 Item2;
            public Tubel(T1 item1, T2 item2)
            {
                Item1 = item1;
                Item2 = item2;
            }
        }
        private Card EnemyInRange()
        {
            HexCoordinates pos = GetComponentInParent<HexCell>().position;
            List<Tubel<int, HexCell>> hexCells = new List<Tubel<int, HexCell>>();

            List<HexCell> baseToTake = new List<HexCell>();
            baseToTake.Add(GetComponentInParent<HexCell>());

            for (int i = 1; i <= maxAttackRange; i++)
            {
                List<HexCell> neighbours = new List<HexCell>();
                foreach (var item in baseToTake)
                {
                    neighbours.AddRange(item.GetNeighbours());
                }
                foreach (var item in neighbours)
                {
                    foreach (var cell in hexCells)
                    {
                        if (cell.Item2 == item)
                        {
                            break;
                        }
                    }
                    hexCells.Add(new Tubel<int, HexCell>(i, item));
                }
                baseToTake.Clear();
                foreach (var item in hexCells)
                {
                    if (item.Item1 == i)
                    {
                        baseToTake.Add(item.Item2);
                    }
                }
            }
            List<Tubel<int, HexCell>> toRemove = new List<Tubel<int, HexCell>>();
            foreach (var item in hexCells)
            {
                if (item.Item1 <= minAttackRange)
                {
                    toRemove.Add(item);
                }

            }
            foreach (var item in toRemove)
            {
                hexCells.Remove(item);
            }
            List<HexCell> closerWatch = new List<HexCell>();
            foreach (var item in hexCells)
            {

                if (item.Item1 <= maxAttackRange && item.Item1 >= minAttackRange + 1)
                {
                    closerWatch.Add(item.Item2);
                }
                else
                {
                    if (structureName == "Canon1")
                    {
                        Debug.Log("wauw");
                    }
                }
            }
            if (closerWatch.Count > 0)
            {
                foreach (var item in closerWatch)
                {
                    if (structureName == "Canon1")
                    {
                        item.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                    }
                    if (item.GetComponentInChildren<Card>() != null)
                    {
                        //Debug.Log(item.position.x +","+ item.position.y);
                        return item.GetComponentInChildren<Card>();//take first.. optiomization possible ^.^
                    }
                }
            }
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

