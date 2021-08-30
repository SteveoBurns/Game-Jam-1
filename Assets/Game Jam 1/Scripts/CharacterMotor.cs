using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameJam.Character
{
    /// <summary>
    /// This class goes on the player object and controls the movement of the character.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMotor : MonoBehaviour
    {
        /// <summary>The run speed of the player character, can be set in the inspector.</summary>
        [Header("Movement Variables")]
        [SerializeField] private float runSpeed = 20;
        /// <summary>The move speed of the player character, can be set in the inspector.</summary>
        [SerializeField] private float moveSpeed = 5;
        
        private bool isMovingRight;
        private bool isMovingLeft;
        private bool isRunning;
        private Animator anim;

        private Rigidbody2D rb;
        
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            isMovingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            isMovingLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            //isRunning = Input.GetKey(KeyCode.LeftShift) && isMovingLeft || Input.GetKey(KeyCode.LeftShift) && isMovingRight;

        }

        private void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// Handles physics based movement
        /// </summary>
        private void Move()
        {
            if(isMovingRight)
            {
                rb.AddForce(this.transform.right * moveSpeed);
                anim.SetTrigger("isMoving");
                if(isRunning)
                    rb.AddForce(this.transform.right * runSpeed);
            }
            else if(isMovingLeft)
            {
                rb.AddForce(-this.transform.right * moveSpeed);
                anim.SetTrigger("isMovingLeft");

                if(isRunning)
                    rb.AddForce(-this.transform.right * runSpeed);
            }
            else
            {
                anim.SetTrigger("isIdle");
            }
        }
    }
}