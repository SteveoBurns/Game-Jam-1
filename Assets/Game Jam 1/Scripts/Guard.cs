using GameJam.Character;
using GameJam.Manager;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.Experimental.GraphView;

using UnityEngine;

namespace GameJam.Guard
{
    public class Guard : MonoBehaviour
    {
        [SerializeField] private float speed = 5;

        [SerializeField] private Transform rayOriginRight;
        [SerializeField] private float raycastDist = 5.22f;
    
        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void FixedUpdate()
        {
            Casting();
        }

        /// <summary>
        /// Raycast from origin, if contact with player, call Game Over.
        /// </summary>
        private void Casting()
        {
            RaycastHit2D hitRight1 = Physics2D.Raycast(rayOriginRight.position, transform.TransformDirection(Vector2.right),raycastDist);

            if(hitRight1)
            {
                // Not sure why this isnt working, but its triggering all the time, even if the object isnt tagged with player.
                if(hitRight1.collider.gameObject.CompareTag("Player"));
                {
                    CharacterInteraction player = hitRight1.collider.gameObject.GetComponent<CharacterInteraction>();
                    if(player != null && !player.inHiding)
                    {
                        GameManager.theManager.GameOver();
                        
                    }
                    Debug.Log($"hit {hitRight1.collider.name}");
                }
            }
        }

        /// <summary>
        /// Moves the transform to the right by set speed variable
        /// </summary>
        private void Move()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}