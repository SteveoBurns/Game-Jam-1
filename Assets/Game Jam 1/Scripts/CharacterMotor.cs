using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMotor : MonoBehaviour
    {
        [Header("Movement Variables")]
        [SerializeField] private float runSpeed = 20;
        [SerializeField] private float moveSpeed = 5;
        
        private bool isMovingRight;
        private bool isMovingLeft;
        private bool isRunning;

        private Rigidbody2D rb;
        
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            isMovingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            isMovingLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            isRunning = Input.GetKey(KeyCode.LeftShift) && isMovingLeft || Input.GetKey(KeyCode.LeftShift) && isMovingRight;

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
                if(isRunning)
                    rb.AddForce(this.transform.right * runSpeed);
            }
            if(isMovingLeft)
            {
                rb.AddForce(-this.transform.right * moveSpeed);
                if(isRunning)
                    rb.AddForce(-this.transform.right * runSpeed);
            }
        }
    }
}