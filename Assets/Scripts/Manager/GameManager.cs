using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HexFight
{
    public class GameManager : MonoBehaviour
    {

        public Player player { get; private set; }

        void Awake()
        {
            DontDestroyOnLoad(this);
            //player = createPlayerWithCards();
            LoadPlayer();
            LoadAllTypes();
            LoadAllCards();

            //SerializeCards(new string[] { "card1", "card2" });
            //Tuple<string[], string[]> meh = new Tuple<string[], string[]>(new string[] { "a1", "a2" }, new string[] { "b1", "b2" });

            //Tuple<string, Tuple<string[], string[]>>[] blah = new Tuple<string, Tuple<string[], string[]>>[] { new Tuple<string, Tuple<string[], string[]>>("asd", meh), new Tuple<string, Tuple<string[], string[]>>("asd", meh) };
            //SerializeTypes(blah);
        }
        private void LoadAllTypes()
        {
            string source = "";
#if UNITY_EDITOR
            source = @"Assets/Resources/types.xml";
#else
            source = Application.persistentDataPath + "/cards.xml";
#endif
            if (!File.Exists(source))
            {
                TextAsset ta = Resources.Load<TextAsset>("cards");
                if (ta != null)
                {
                    MemoryStream assetStream = new MemoryStream(ta.bytes);
                    XmlSerializer x = new XmlSerializer(typeof(CardTemplate[]));
                    CardTemplate[] cards = (CardTemplate[])x.Deserialize(assetStream);
                    CardTemplate.allCardTemplates.AddRange(cards);
                    using (FileStream fs = new FileStream(source, FileMode.Create))
                    {
                        x.Serialize(fs, cards);
                    }
                }
                else
                {
                    throw new System.Exception("No Cards to load");
                }
            }
            else
            {
                using (FileStream fs = new FileStream(source, FileMode.Open))
                {
                    XmlSerializer x = new XmlSerializer(typeof(CardTemplate[]));
                    CardTemplate[] cards = (CardTemplate[])x.Deserialize(fs);
                    CardTemplate.allCardTemplates.AddRange(cards);
                }
            }

        }
        private void LoadAllCards()
        {
            string source = "";
#if UNITY_EDITOR
            source = @"Assets/Resources/cards.xml";
#else
            source = Application.persistentDataPath + "/cards.xml";
#endif
            if (!File.Exists(source))
            {
                TextAsset ta = Resources.Load<TextAsset>("cards");
                if (ta != null)
                {
                    MemoryStream assetStream = new MemoryStream(ta.bytes);
                    XmlSerializer x = new XmlSerializer(typeof(CardTemplate[]));
                    CardTemplate[] cards = (CardTemplate[])x.Deserialize(assetStream);
                    CardTemplate.allCardTemplates.AddRange(cards);
                    using (FileStream fs = new FileStream(source, FileMode.Create))
                    {
                        x.Serialize(fs, cards);
                    }
                }
                else
                {
                    throw new System.Exception("No Cards to load");
                }
            }
            else
            {
                using (FileStream fs = new FileStream(source, FileMode.Open))
                {
                    XmlSerializer x = new XmlSerializer(typeof(CardTemplate[]));
                    CardTemplate[] cards = (CardTemplate[])x.Deserialize(fs);
                    CardTemplate.allCardTemplates.AddRange(cards);
                }
            }
        }
        private void LoadPlayer()
        {
            string source = "";
#if UNITY_EDITOR
            source = @"Assets/player.bin";
#else
            source = Application.persistentDataPath + "/player.bin";
#endif


            if (!File.Exists(source))
            {
                using (FileStream fs = new FileStream(source, FileMode.Create, FileAccess.Write))
                {
                    player = new Player("Name");
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, player);
                }
            }
            else
            {
                player = createPlayerWithCards();
                //using (FileStream fs = new FileStream(source, FileMode.Open, FileAccess.Read))
                //{
                //    BinaryFormatter bf = new BinaryFormatter();
                //    player = (Player)bf.Deserialize(fs);
                //}
            }

        }

        private void SerializeCards(string[] card)
        {
            CardTemplate[] cards = new CardTemplate[card.Length];
            for (int i = 0; i < card.Length; i++)
            {
                cards[i] = new CardTemplate(card[i], 1, 2, 3, 4, 5, "Infantry");
            }

            string source = @"Assets\cards.xml";
            using (FileStream fs = new FileStream(source, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer x = new XmlSerializer(typeof(CardTemplate[]));
                x.Serialize(fs, cards);
            }
        }

        private void SerializeTypes(Tuple<string, Tuple<string[], string[]>>[] type)
        {
            Card.Type[] types = new Card.Type[type.Length];
            for (int i = 0; i < type.Length; i++)
            {
                types[i] = new Card.Type(type[i].first);
                types[i].strongAgainst.AddRange(type[i].second.first);
                types[i].weakAgainst.AddRange(type[i].second.second);
            }

            string source = @"Assets\types.xml";
            using (FileStream fs = new FileStream(source, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer x = new XmlSerializer(typeof(Card.Type[]));
                x.Serialize(fs, types);
            }

        }
        private Player createPlayerWithCards()
        {
            Player newPlayer = new Player("Name");


            List<Tuple<string, int>> cards = new List<Tuple<string, int>>();

            cards.Add(new Tuple<string, int>("Footman", 1));
            cards.Add(new Tuple<string, int>("Soldier", 1));
            cards.Add(new Tuple<string, int>("Catapult", 1));
            cards.Add(new Tuple<string, int>("Bowman", 1));
            cards.Add(new Tuple<string, int>("Horseman", 1));
            cards.Add(new Tuple<string, int>("Engineer", 1));
            cards.Add(new Tuple<string, int>("Gangster", 1));

            newPlayer.cards = cards;


            newPlayer.decks[0] = new Deck("Infantry");
            newPlayer.decks[0].addCard(new Tuple<string, int>("Footman", 1));
            newPlayer.decks[0].addCard(new Tuple<string, int>("Soldier", 1));
            newPlayer.decks[0].addCard(new Tuple<string, int>("Engineer", 1));

            newPlayer.decks[1] = new Deck("Others");
            newPlayer.decks[1].addCard(new Tuple<string, int>("Catapult", 1));
            newPlayer.decks[1].addCard(new Tuple<string, int>("Bowman", 1));
            newPlayer.decks[1].addCard(new Tuple<string, int>("Horseman", 1));
            newPlayer.decks[1].addCard(new Tuple<string, int>("Gangster", 1));

            newPlayer.decks[2] = new Deck("All");
            newPlayer.decks[2].addCard(new Tuple<string, int>("Footman", 1));
            newPlayer.decks[2].addCard(new Tuple<string, int>("Soldier", 1));
            newPlayer.decks[2].addCard(new Tuple<string, int>("Engineer", 1));
            newPlayer.decks[2].addCard(new Tuple<string, int>("Gangster", 1));
            newPlayer.decks[2].addCard(new Tuple<string, int>("Horseman", 1));
            newPlayer.decks[2].addCard(new Tuple<string, int>("Bowman", 1));
            newPlayer.decks[2].addCard(new Tuple<string, int>("Catapult", 1));

            return newPlayer;
        }
    }
}