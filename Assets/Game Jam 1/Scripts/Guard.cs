using GameJam.Character;
using GameJam.Manager;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.Experimental.GraphView;

using UnityEngine;

namespace GameJam.Guard
{
    /// <summary>
    /// This class goes on the guard object and handles the guard once it has been instantiated by the GuardSpawner.
    /// </summary>
    public class Guard : MonoBehaviour
    {
        /// <summary>The move speed of the guard object.</summary>
        [Header("Guard Speed")]
        [SerializeField] private float speed = 5;

        /// <summary>Origin point of the right raycast.</summary>
        [Header("Raycast Variables")]
        [SerializeField] private Transform rayOriginRight;
        /// <summary>The distance of the raycast.</summary>
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