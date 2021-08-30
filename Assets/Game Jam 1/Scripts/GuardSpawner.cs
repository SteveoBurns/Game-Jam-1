using GameJam.Manager;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Guard
{
    /// <summary>
    /// This class goes on the triggers in the level and spawns guards.
    /// </summary>
    public class GuardSpawner : MonoBehaviour
    {
        /// <summary>The prefab object of the guard. Can be set in the inspector.</summary>
        [Header("Guard variables")]
        [SerializeField] private GameObject guardPrefab;
        /// <summary>Lifetime in seconds before the guard is destroyed. Can be set in the inspector.</summary>
        [SerializeField] private float guardLifetime = 20;
        
        /// <summary>The distance from the object that the guard spawns. Can be set in the inspector.</summary>
        [Header("Distance from the trigger that the guard spawns")]
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