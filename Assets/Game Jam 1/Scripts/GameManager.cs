using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager theManager;

        public GameObject player;

        public Image enterHiding;
        public Image exitHiding;
        public float imageOffset = 2;


        private void Awake()
        {
            if(theManager != null)
                Destroy(this);
            else
                theManager = this;
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