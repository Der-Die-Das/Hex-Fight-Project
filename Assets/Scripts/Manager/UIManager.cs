using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace HexFight {
    public class UIManager : MonoBehaviour
    {

        public List<GameObject> OptionBar;
        public GameObject[] canvas;
        public GameObject[] DeckManagerButtons;
        public GameObject DeckManagerCanvasGroup;
        public GameObject NameEditorCanvasGroup;
        public GameObject DeckEditor_Deck;
        private bool OptionBarExpanded = false;
        private List<Animator> animator = new List<Animator>();
        private Deck selectedDeck;
        private GameObject selectedCardinDeck;
        public GameObject[] cardsDeckEditor;

        private enum CardState { ZoomedIn, ZommedOut};
        private CardState cardstate;
        private enum State { MainToLevel, MainToDeckMngr, DeckMngrToMain, LevelToMain, DeckMngrToDeck, DeckToDeckMngr }
        private State state;


        void Awake() {
            
        }

        // Use this for initialization
        void Start()
        {
            //subscribe to inputfield onEndEdit-Event
            NameEditorCanvasGroup.GetComponentInChildren<InputField>().onEndEdit.AddListener(ChangeDeckName);

            //add all optionbaranimators to a list
            foreach (GameObject item in OptionBar)
            {
                animator.Add(item.GetComponent<Animator>());
            }


            for (int i = 0; i < canvas.Length; i++)
            {
                canvas[i].GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
                if (i >= 1)
                {
                    canvas[i].GetComponent<RectTransform>().position = new Vector2(canvas[i - 1].GetComponent<RectTransform>().position.x + Screen.width, 0);
                }
            }

            LoadDeckNames();

        }

        private void LoadDeckNames() {
            //Set ButtonTexts in DeckManager the value of the decknames
            for (int i = 0; i < 3; i++)
            {
                string deckname = GameManager.instance.player.decks[i].name;
                DeckManagerButtons[i].GetComponentInChildren<Text>().text = deckname;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //Do Camera-Movement to active Canvas
            Transform cameratransform = Camera.main.GetComponent<Transform>();
            float cameraxpos = cameratransform.position.x;
            float desiredxpos;
            switch (state)
            {
                case State.MainToLevel:
                    //Move Camera to Levelselection, departure at MainMenu
                    break;
                case State.MainToDeckMngr:
                    //Move Camera To DeckManager, departure at MeinMenu
                    desiredxpos = canvas[1].transform.position.x;
                    if(cameraxpos < desiredxpos)
                    {
                        cameratransform.Translate(new Vector3((desiredxpos-cameraxpos)/5,0));
                    }
                    break;
                case State.DeckMngrToMain:
                    //Move Camera To MainMenu, departure at DeckManager
                    desiredxpos = canvas[0].transform.position.x;
                    if(cameraxpos > desiredxpos)
                    {
                        cameratransform.Translate(new Vector3((desiredxpos - cameraxpos) / 5, 0));
                    }
                    break;
                case State.LevelToMain: 
                    //Move Camera To MainMenu, departure at LevelSelection
                    break;
                case State.DeckMngrToDeck:
                    //Move Camera to selected Deck, departure at DeckManager
                    desiredxpos = canvas[2].transform.position.x;
                    if(cameraxpos < desiredxpos) 
                    {
                        cameratransform.Translate(new Vector3((desiredxpos - cameraxpos) / 5, 0));
                    }
                    break;
                case State.DeckToDeckMngr:
                    //Move Camera to DeckManager, departure at selected Desk
                    if(cameraxpos > canvas[1].transform.position.x)
                    {
                        cameratransform.Translate(new Vector3(-20, 0));
                    }
                    break;
                default:
                    break;
            }

            if (selectedCardinDeck != null)
            {
                Vector3 position = selectedCardinDeck.GetComponent<RectTransform>().position;
                Vector2 size = selectedCardinDeck.GetComponent<RectTransform>().sizeDelta;
                Vector2 shadow = selectedCardinDeck.GetComponent<Shadow>().effectDistance;
                switch (cardstate)
                {
                    case CardState.ZoomedIn:
                        if(size.x < 75 && size.y < 150) 
                        {
                            selectedCardinDeck.GetComponent<RectTransform>().sizeDelta += new Vector2(2, 2); 
                        }
                        if(position.z > -20) {
                            selectedCardinDeck.GetComponent<RectTransform>().position += new Vector3(0, 0, -4);
                        }
                        if(shadow.x < 5 && shadow.y > -5) 
                        {
                            selectedCardinDeck.GetComponent<Shadow>().effectDistance += new Vector2(1, -1);
                        }
                        selectedCardinDeck.GetComponent<RectTransform>().SetAsLastSibling();
                        break;
                    case CardState.ZommedOut:
                        if(size.x > 65 && size.y > 140) 
                        {
                            selectedCardinDeck.GetComponent<RectTransform>().sizeDelta -= new Vector2(2, 2);
                        }
                        if(position.z < 0) 
                        {
                            selectedCardinDeck.GetComponent<RectTransform>().position += new Vector3(0, 0, 4);
                        }
                        if(shadow.x > 0 && shadow.y < 0) 
                        {
                            selectedCardinDeck.GetComponent<Shadow>().effectDistance += new Vector2(-1, 1);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        // void to move Sound- & Music buttons when option button is clicked
        public void OnOptionClick()
        {
            if (!OptionBarExpanded)
            {
                //play expand-animation of every optionbar
                foreach (Animator item in animator)
                {
                    item.Play("SettingsBarIn");
                    OptionBarExpanded = true;
                }
            }
            else
            {
                //play close-animation of every optionbar
                foreach (Animator item in animator)
                {
                    item.Play("SettingsBarOut");
                    OptionBarExpanded = false;
                }
            }
        }

        public void OnMusicSoundClick()
        {
            //if the music button is clicked, mute or demute music

            //change buttonIcon
        }

        public void OnSoundClick()
        {
            //if the sound button is clicked, mute or demute sound

            //change ButtonIcon
        }

        public void OnPlayClick()
        {
            //GameManager.GetInstance().LoadLevelSelection();
        }

        public void OnDeckManagerClick() {
            state = State.MainToDeckMngr;
            //Camera.main.GetComponent<Animator>().Play("MoveToDeckManager");
        }

        public void BackToMainFromDeck() {
            state = State.DeckMngrToMain;
            //Camera.main.GetComponent<Animator>().Play("MoveDeckToMainMenu");
        }

        public void LoadDeck() {
            //set Title in Header to Deckname
            canvas[2].GetComponentInChildren<Text>().text = selectedDeck.name;

            //Check every Card-Slot in Deck and check if there is any card in this slot, if so, load its image
            for (int i = 0; i < selectedDeck.cards.Length; i++)
            {
                //tag gameobjects
                if(selectedDeck.cards[i] != null) 
                {
                    cardsDeckEditor[i].tag = selectedDeck.cards[i].first;
                }
                else
                {
                    cardsDeckEditor[i].tag = "Empty";
                }
            }

            cardstate = CardState.ZommedOut;

            /*******************Bullshit********************/
            ////Instantiate every Card in this Deck
            //float CanvasWidth = DeckEditor_Deck.GetComponent<RectTransform>().sizeDelta.x;
            //float CanvasHeight = DeckEditor_Deck.GetComponent<RectTransform>().sizeDelta.y;
            //float desiredcardwidth = (CanvasWidth / 5);
            //float desiredcardheight = (CanvasHeight / 2);
            //float[] desiredxpos = new float[5] { -(desiredcardwidth * 2), -desiredcardwidth, 0, desiredcardwidth, desiredcardwidth*2};
            //float[] desiredypos = new float[2] { -(desiredcardheight / 2), desiredcardheight / 2 };
            //Vector3 desiredpos;

            //for (int i = 0; i < selectedDeck.cards.Length; i++)
            //{   
            //    //get desired position
            //    if(i < 5) 
            //    {
            //        desiredpos = new Vector3(desiredxpos[i], desiredypos[0]);
            //    }
            //    else
            //    {
            //        desiredpos = new Vector3(desiredxpos[i-5], desiredypos[1]);
            //    }
                
            //    if (selectedDeck.cards[i] != null) 
            //    {

            //    }
            //    else
            //    {
            //        GameObject instance = (GameObject)Instantiate(EmptyDeckslotPlaceholder, DeckEditor_Deck.GetComponent<RectTransform>());
            //        instance.GetComponent<RectTransform>().sizeDelta = new Vector2(desiredcardwidth-10, desiredcardheight-20);
            //        instance.GetComponent<RectTransform>().Translate(desiredpos + new Vector3(846.625f,0));
            //        Debug.Log(instance.GetComponent<RectTransform>().position);
            //    }

            //}

            state = State.DeckMngrToDeck;
        }

        public void OnCardClick(GameObject clickedcard)
        {
            if (clickedcard != selectedCardinDeck && selectedCardinDeck != null && cardstate == CardState.ZoomedIn)
            {
                cardstate = CardState.ZommedOut;
            }

            else if (clickedcard.tag != "Empty")
            {
                selectedCardinDeck = clickedcard;
                switch (cardstate)
                {
                    case CardState.ZoomedIn:
                        cardstate = CardState.ZommedOut;
                        break;
                    case CardState.ZommedOut:
                        cardstate = CardState.ZoomedIn;
                        break;
                    default:
                        break;
                }
            }
        }

        public void ClearDeck() {
            selectedDeck.Clear();
        }

        public void SelectDeck(GameObject pressedButton) {
            string Deckname = pressedButton.GetComponent<Text>().text;
            foreach (Deck deck in GameManager.instance.player.decks)
            {
                if (deck.name == Deckname)
                {
                    selectedDeck = deck;
                    EnableDeckEditButtons();
                }
            }

        }

        private void EnableDeckEditButtons() {
            DeckManagerCanvasGroup.GetComponent<CanvasGroup>().alpha = 1f;
            DeckManagerCanvasGroup.GetComponent<CanvasGroup>().interactable = true;
        }

        public void EnableDeckNameEditor() {
            NameEditorCanvasGroup.GetComponent<CanvasGroup>().alpha = 1;
            NameEditorCanvasGroup.GetComponent<CanvasGroup>().interactable = true;
            NameEditorCanvasGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        private void DisableDeckNameEditor() {
            NameEditorCanvasGroup.GetComponent<CanvasGroup>().alpha = 0;
            NameEditorCanvasGroup.GetComponent<CanvasGroup>().interactable = false;
            NameEditorCanvasGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }


        private void ChangeDeckName(string arg0) {
            //change Name
            selectedDeck.ChangeName(arg0);

            //set the Canvas Groups alpha to 0 again and make it ininteractable
            DisableDeckNameEditor();

            //Reload the Deck
            LoadDeck();

            //Relaod DeckButtons so their text changes
            LoadDeckNames();
        }
        public void BackToDeckManagerFromDeck() {
            state = State.DeckToDeckMngr;

        }
    }
}
