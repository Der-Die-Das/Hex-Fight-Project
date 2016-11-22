using UnityEngine;
using System.Collections;
using System;

namespace HexFight
{
    public class UIManager : MonoBehaviour
    {

        public GameObject container;
        public float OptionBarMovingSpeed;
        private bool OptionBarExpanded = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // if the settings button is clicked, move the optionbar to the left until it has reached its desired position, if its then clicked again move it to the right
            float xPos = container.GetComponent<RectTransform>().localPosition.x;
            if (!OptionBarExpanded)
            {
                if (xPos < 252f)
                {
                    container.GetComponent<RectTransform>().localPosition = new Vector3(container.GetComponent<RectTransform>().localPosition.x + OptionBarMovingSpeed, container.GetComponent<RectTransform>().localPosition.y);
                }
            }
            else
            {
                if (xPos > 167f)
                {
                    container.GetComponent<RectTransform>().localPosition = new Vector3(container.GetComponent<RectTransform>().localPosition.x - OptionBarMovingSpeed, container.GetComponent<RectTransform>().localPosition.y);
                }
            }
        }

        // void to move Sound- & Music buttons when option button is clicked
        public void OnOptionClick()
        {
            if (!OptionBarExpanded)
            {
                OptionBarExpanded = true;
            }
            else
            {
                OptionBarExpanded = false;
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
            GameManager.GetInstance().LoadLevelSelection();
        }

        public void OnDeckManagerClick()
        {
            GameManager.GetInstance().LoadDeckManager();
        }
    }
}
