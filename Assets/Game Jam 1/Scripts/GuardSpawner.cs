using GameJam.Manager;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Guard
{
    public class GuardSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject guardPrefab;
        [SerializeField] private float guardLifetime = 20;
        [SerializeField] private float dist = 10;
        
        private Vector2 spawn = Vector2.zero;

        private void Start()
        {
            // Setting the spawn point
            spawn = new Vector2(transform.position.x - dist, transform.position.y);
        }


        /// <summary>
        /// Spawn a guard and then destroy after the lifetime
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                //Spawn an amount of guards at the set spawn point
                GameObject guard = Instantiate(guardPrefab, spawn, Quaternion.identity);
                // Destroy spawned guards after x time
                Destroy(guard, guardLifetime);
            }
        }
    }
}