using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

namespace HexFight
{
    [Serializable]
    public class Card : MonoBehaviour
    {
        public Type type;
        public Sprite img;
        public CardTemplate template;
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

            public static Type? getTypeByName(string name)
            {
                if (allTypes.Count > 0)
                {
                    foreach (var item in allTypes)
                    {
                        if (item.name == name)
                        {
                            return item;
                        }
                    }
                }
                return null;
            }
        }
        void Awake()
        {
            //if (template.cardName == "" || template.hp == 0 || template.damage == 0 || template.moveRange == 0 || template.minAttackRange == 0 || template.maxAttackRange == 0 || template.typeName == "")
            //{
            //    throw new Exception("Not all Parameters of this Card are set.");
            //}
        }

        public void setTemplate(string name)
        {
            foreach (var item in CardTemplate.allCardTemplates)
            {
                if (item.cardName == name)
                {
                    template = item;
                    Type? type = Type.getTypeByName(template.typeName);
                    if (type != null)
                    {
                        type = (Type)type;
                    }

                    Texture2D tex = Resources.Load(@"CardImages\" + template.imageSource, typeof(Texture2D)) as Texture2D;
                    Rect rect = new Rect(Vector2.zero, new Vector2(300, 450));
                    if (tex != null)
                    {
                        img = Sprite.Create(tex, rect, Vector2.zero);
                    }
                    else
                    {
                        Debug.Log("Image: " + template.imageSource + " not found");
                    }
                    return;
                }
            }
            Debug.Log("Template: " + name + " not found");

        }

        public bool MoveInDirection(int direction)
        {
            throw new NotImplementedException();
        }
        public bool Attack(HexCoordinates position)
        {
            throw new NotImplementedException();
        }
        public void Die()
        {
            throw new NotImplementedException();
        }
        private bool EnterTransportCard(Transportcard card)
        {
            throw new NotImplementedException();
        }
        private bool LeaveTransportCard(Transportcard card)
        {
            throw new NotImplementedException();
        }
    }
}
