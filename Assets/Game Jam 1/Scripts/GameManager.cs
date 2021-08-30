using GameJam.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Manager
{
    /// <summary>
    /// This is a singleton that goes on an empty object within the scene. It handles the UI elements to do with gameplay.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// This struct holds the data for the PopUp display.
        /// </summary>
        [Serializable]
        public struct PopUp
        {
            public string title;
            [TextArea]public string description;
            public bool endLevel;
        }

        public static GameManager theManager;

        /// <summary>The player object. Used to find position in scene.</summary>
        [Header("Player Character")]
        public GameObject player;

        private CharacterInteraction characterInteraction;

        [Header("World Canvas Elements")]
        public Image enterHiding;
        public Image exitHiding;
        public float imageOffset = 2;

        [Header("Pop Up Messages")] 
        public GameObject popUp;
        public TMP_Text mainMenu;
        public TMP_Text description;
        public TMP_Text title;
        public PopUp caught;
        public PopUp escaped;


        private void Awake()
        {
            if(theManager != null)
                Destroy(this);
            else
                theManager = this;

            characterInteraction = player.GetComponent<CharacterInteraction>();
            Time.timeScale = 1;
        }

        /// <summary>
        /// Sets the PopUp active and assigns the passed strings into text fields. Enables main menu button if finished the level.
        /// </summary>
        /// <param name="_popUp"></param>
        public void PopUpCanvas(PopUp _popUp)
        {
            popUp.SetActive(true);
            description.text = _popUp.description;
            title.text = _popUp.title;
            
            mainMenu.enabled = _popUp.endLevel;
            Time.timeScale = 0;
        }
        
        /// <summary>
        /// Sets bools for hiding and shows the world space UI.
        /// </summary>
        public void EnterHiding()
        {
            exitHiding.enabled = false;
            enterHiding.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + imageOffset, player.transform.position.z);
            enterHiding.enabled = true;
        }
        /// <summary>
        /// Sets the bools for exiting hiding and shows the world space UI.
        /// </summary>
        public void ExitHiding()
        {
            enterHiding.enabled = false;
            exitHiding.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + imageOffset, player.transform.position.z);
            exitHiding.enabled = true;
        }

        /// <summary>
        /// Disables the world space UI.
        /// </summary>
        public void DisableHidingUI()
        {
            enterHiding.enabled = false;
            exitHiding.enabled = false;
        }

        /// <summary>
        /// The Game Over function when either getting caught or finishing the level
        /// </summary>
        public void GameOver()
        {
            if(!characterInteraction.inHiding && !characterInteraction.madeIt)
            {
                PopUpCanvas(caught);
                Debug.Log("Game Over");
            }
            else if(characterInteraction.madeIt)
            {
                PopUpCanvas(escaped);
                Debug.Log("Finished");
            }
        }
        
        
    }
}