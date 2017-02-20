using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HexFight
{
    public class UIManager : MonoBehaviour
    {
        public Animator OptionBarAnimator;
        public Canvas MainMenu;
        public Canvas DeckSelection;
        public Canvas DeckManager;
        public Canvas CardShowcase;
        public Button[] DeckSelections;
        public GameObject CardHolder;
        public GameObject DeckHolder;
        public GameObject CardPrefab;
        public EventSystem eventSystem;
        public InputField DeckName;

        private bool movedFinger;
        private Deck selectedDeck;
        private Card cardInShowCase;
        private void Awake()
        {
            DeckSelection.enabled = false;
            DeckManager.enabled = false;
            CardShowcase.enabled = false;
        }

        private void Update()
        {
            if (DeckManager.enabled && !CardShowcase.enabled)
            {
                if (Input.touchCount == 1)
                {
                    switch (Input.GetTouch(0).phase)
                    {
                        case TouchPhase.Began:
                            movedFinger = false;
                            break;
                        case TouchPhase.Moved:
                            movedFinger = true;
                            break;
                        case TouchPhase.Ended:
                            if (movedFinger == false)
                            {
                                if (eventSystem.currentSelectedGameObject != null)
                                {
                                    Card selectedCard = eventSystem.currentSelectedGameObject.GetComponent<Card>();
                                    if (selectedCard != null)
                                    {
                                        if (selectedCard.transform.parent.name == "Card Content")
                                        {
                                            bool hasCard = false;

                                            if (selectedDeck.cards.Length > 0)
                                            {
                                                foreach (var item in selectedDeck.cards)
                                                {
                                                    if (item != null)
                                                    {
                                                        if (item.first == selectedCard.template.cardName)
                                                        {
                                                            hasCard = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }

                                            CardShowcase.transform.FindChild("ToDeck").gameObject.SetActive(!hasCard);

                                            CardShowcase.transform.FindChild("RemoveFromDeck").gameObject.SetActive(hasCard);
                                        }
                                        else if (selectedCard.transform.parent.name == "Deck Content")
                                        {
                                            CardShowcase.transform.FindChild("ToDeck").gameObject.SetActive(false);
                                            CardShowcase.transform.FindChild("RemoveFromDeck").gameObject.SetActive(true);
                                        }
                                        else
                                        {
                                            Debug.Log("oh oh ... fail!");
                                        }

                                        CardShowcase.transform.FindChild("Move Range").GetChild(0).GetComponent<Text>().text = selectedCard.template.moveRange.ToString();
                                        CardShowcase.transform.FindChild("Type").GetChild(0).GetComponent<Text>().text = selectedCard.template.typeName;
                                        CardShowcase.transform.FindChild("Damage").GetChild(0).GetComponent<Text>().text = selectedCard.template.damage.ToString();
                                        CardShowcase.transform.FindChild("Name").GetChild(0).GetComponent<Text>().text = selectedCard.template.cardName;
                                        CardShowcase.transform.FindChild("Attack Range").GetChild(0).GetComponent<Text>().text = selectedCard.template.minAttackRange + " - " + selectedCard.template.maxAttackRange;
                                        CardShowcase.transform.FindChild("HP").GetChild(0).GetComponent<Text>().text = selectedCard.template.hp.ToString();
                                        CardShowcase.transform.FindChild("Level").GetChild(0).GetComponent<Text>().text = selectedCard.template.level.ToString();

                                        cardInShowCase = selectedCard;

                                        CardShowcase.enabled = true;

                                    }
                                    else
                                    {
                                        Debug.Log(eventSystem.currentSelectedGameObject.name);
                                        if (eventSystem.currentSelectedGameObject.name == "DeckName")
                                        {
                                            TouchScreenKeyboard.Open(selectedDeck.name, TouchScreenKeyboardType.NamePhonePad);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }


        public void ChangeStateOfOptionBar()
        {
            OptionBarAnimator.SetTrigger("Change");
        }

        #region MainMenuFunctions
        public void ChangeMusicState()
        {
            //TODO: mute or unmute music here
        }
        public void ChangeSoundState()
        {
            //TODO: mute or unmute sounds here
        }
        public void Play()
        {
            SceneManager.LoadScene("LevelSelection");
        }
        public void LoadDeckSelection()
        {
            for (int i = 0; i < 3; i++)
            {
                string deckname = GameObject.FindObjectOfType<GameManager>().player.decks[i].name;
                DeckSelections[i].GetComponentInChildren<Text>().text = deckname;

            }
            DeckSelection.enabled = true;
            MainMenu.enabled = false;
        }
        #endregion
        #region DeckSelectionFunctions

        public void DeckSelectionBack()
        {
            DeckSelection.enabled = false;
            MainMenu.enabled = true;
        }
        public void EditDeck(int index)
        {
            selectedDeck = GameObject.FindObjectOfType<GameManager>().player.decks[index];

            Debug.Log("Edit Deck:"+selectedDeck.name);

            DeckName.text = selectedDeck.name;

            for (int i = 0; i < DeckHolder.transform.childCount; i++)
            {
                Destroy(DeckHolder.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < CardHolder.transform.childCount; i++)
            {
                Destroy(CardHolder.transform.GetChild(i).gameObject);
            }
            if (selectedDeck != null && selectedDeck.cards != null && selectedDeck.cards.Length > 0)
            {
                for (int i = 0; i < selectedDeck.cards.Length; i++)
                {
                    if (selectedDeck.cards[i] != null)
                    {
                        GameObject card = Instantiate(CardPrefab, DeckHolder.transform);
                        Card cardComponent = card.GetComponent<Card>();
                        cardComponent.setTemplate(selectedDeck.cards[i].first);
                        card.GetComponent<Image>().sprite = cardComponent.img;
                    }
                }
            }
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager.player.cards != null && gameManager.player.cards.Count > 0)
            {
                for (int i = 0; i < gameManager.player.cards.Count; i++)
                {
                    if (gameManager.player.cards[i] != null)
                    {
                        GameObject card = Instantiate(CardPrefab, CardHolder.transform);
                        Card cardComponent = card.GetComponent<Card>();
                        cardComponent.setTemplate(gameManager.player.cards[i].first);
                        card.GetComponent<Image>().sprite = cardComponent.img;
                    }
                }
            }

            DeckManager.enabled = true;
            DeckSelection.enabled = false;
        }
        #endregion

        #region DeckManager

        public void BackToDeckManager()
        {
            CardShowcase.enabled = false;
        }

        public void DeckManagerBack()
        {
            SaveDeck();
            for (int i = 0; i < 3; i++)
            {
                string deckname = GameObject.FindObjectOfType<GameManager>().player.decks[i].name;
                DeckSelections[i].GetComponentInChildren<Text>().text = deckname;

            }
            DeckManager.enabled = false;
            DeckSelection.enabled = true;
        }

        public void SaveDeck()
        {
            string source = "";
#if UNITY_EDITOR
            source = @"Assets/player.bin";
#else
            source = Application.persistentDataPath + "/player.bin";
#endif


            using (FileStream fs = new FileStream(source, FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, FindObjectOfType<GameManager>().player);
            }
        }

        public void EditDeckName()
        {
            Debug.Log(DeckName.text);
            selectedDeck.ChangeName(DeckName.text);
        }

        public void MoveCardToDeck()
        {
            if (selectedDeck.addCard(new Tuple<string, int>(cardInShowCase.template.cardName, cardInShowCase.template.level)) == false)
            {
                throw new Exception("Could not add Card to Deck");
            }
            else
            {
                reloadDeckView();
                CardShowcase.enabled = false;
            }
        }
        public void RemoveCardFromDeck()
        {
            if (selectedDeck.removeCard(new Tuple<string, int>(cardInShowCase.template.cardName, cardInShowCase.template.level)) == false)
            {
                throw new Exception("Could not remove Card from Deck");
            }
            else
            {
                reloadDeckView();
                CardShowcase.enabled = false;
            }
        }

        private void reloadDeckView()
        {
            for (int i = 0; i < DeckHolder.transform.childCount; i++)
            {
                Destroy(DeckHolder.transform.GetChild(i).gameObject);
            }
            if (selectedDeck != null && selectedDeck.cards != null && selectedDeck.cards.Length > 0)
            {

                for (int i = 0; i < selectedDeck.cards.Length; i++)
                {
                    if (selectedDeck.cards[i] != null)
                    {
                        GameObject card = Instantiate(CardPrefab, DeckHolder.transform);
                        Card cardComponent = card.GetComponent<Card>();
                        cardComponent.setTemplate(selectedDeck.cards[i].first);
                        card.GetComponent<Image>().sprite = cardComponent.img;
                    }

                }
            }
        }

        #endregion
    }
}
