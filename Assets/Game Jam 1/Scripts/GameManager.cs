using GameJam.Character;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Manager
{
    public class GameManager : MonoBehaviour
    {
        [Serializable]
        public struct PopUp
        {
            public string title;
            [TextArea]public string description;
            public bool endLevel;
        }

        
        public static GameManager theManager;

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
        public TMP_Text retry;
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

        public void PopUpCanvas(PopUp _popUp)
        {
            popUp.SetActive(true);
            description.text = _popUp.description;
            title.text = _popUp.title;
            
            mainMenu.enabled = _popUp.endLevel;
            Time.timeScale = 0;
        }
        
        public void EnterHiding()
        {
            exitHiding.enabled = false;
            enterHiding.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + imageOffset, player.transform.position.z);
            enterHiding.enabled = true;
        }
        public void ExitHiding()
        {
            enterHiding.enabled = false;
            exitHiding.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + imageOffset, player.transform.position.z);
            exitHiding.enabled = true;
        }

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
        
        

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}