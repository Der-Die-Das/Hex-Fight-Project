using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HexFight {
    public class GameManager : MonoBehaviour {

        public static GameManager instance { get; private set; }
        public Player player { get; private set; }

        public GameManager()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        void Awake()
        {
            DontDestroyOnLoad(instance);
            LoadPlayer();
        }

        // Use this for initialization
        void Start() {
            
        }

        // Update is called once per frame
        void Update() {

        }

        public void LoadLevelSelection() {
            //Load LevelSelection
            //Application.LoadLevel();
        }

        public void LoadDeckManager() {
            //Load DeckManager
            //Application.LoadLevel();
        }

        private void LoadPlayer() 
        {
            //Search for existing Player, if there is none, create a new Player
            string source = @"Assets\player.bin";
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
                using(FileStream fs = new FileStream(source, FileMode.Open, FileAccess.Read)) 
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    player = (Player)bf.Deserialize(fs);
                }
            }

        }
    }
}