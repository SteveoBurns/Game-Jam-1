using GameJam.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Character
{
	/// <summary>
	/// This class goes on the player character and handles interactions with the world.
	/// </summary>
	public class CharacterInteraction : MonoBehaviour
	{
		/// <summary>The origin of the rightside raycast.</summary>
		[Header("Raycast Origin Points")]
		[SerializeField] private Transform rayOriginRight;
		/// <summary>The origin of the leftside raycast.</summary>
		[SerializeField] private Transform rayOriginLeft;
		/// <summary>The distance of the raycasts.</summary>
		[SerializeField] private float raycastDist = 3f;
		
		private RaycastHit2D hitRight;
		private RaycastHit2D hitLeft;
		
		/// <summary>The Sprite Renderer of the player object.</summary>
		[Header("Character Sprite Renderer")]
		public SpriteRenderer rend; 
		
		private bool inRange = false;
		[Header("Character Bools")]
		public bool inHiding = false;
		public bool madeIt = false;

		private CharacterMotor motor;


		private void Start()
		{
			motor = GetComponent<CharacterMotor>();
		}
		
		private void Update()
		{
			Hiding();
		}

		private void FixedUpdate()
		{
			InteractablesCast();
		}

		/// <summary>
		/// Hiding the character when near an interactable object.
		/// </summary>
		private void Hiding()
		{
			if(inRange && !inHiding)
			{
				GameManager.theManager.EnterHiding();
			}
			if(inRange && Input.GetKeyDown(KeyCode.E))
			{
				Debug.Log("pressed E");
				inHiding = true;
				rend.enabled = false;
				motor.enabled = false;
				GameManager.theManager.ExitHiding();
			}

			if(inHiding)
			{
				GameManager.theManager.ExitHiding();

			}
			
			if(inHiding && Input.GetKeyDown(KeyCode.Space))
			{
				inHiding = false;
				rend.enabled = true;
				motor.enabled = true;
				GameManager.theManager.EnterHiding();

			}
			
		}

		/// <summary>
		/// Cast a ray Left and Right and detect any Interactable objects, then enable the UI on those objects.
		/// </summary>
		private void InteractablesCast()
		{
			hitRight = Physics2D.Raycast(rayOriginRight.position, transform.TransformDirection(Vector2.right), raycastDist);
			hitLeft = Physics2D.Raycast(rayOriginLeft.position, transform.TransformDirection(Vector2.left), raycastDist);

			if(hitRight)
			{
				if(hitRight.collider.tag == "Interactable")
				{
					inRange = true;
					Debug.Log("hit interactable");
				}
				else if(hitRight.collider.CompareTag("Finish"))
				{
					madeIt = true;
					GameManager.theManager.GameOver();
				}
				Debug.Log("hit" + hitRight.collider.name);
			}
			else if(hitLeft)
			{
				if(hitLeft.collider.CompareTag("Interactable"))
				{
					inRange = true;
					Debug.Log("hit interactable");
				}
			}
			else if(!hitRight || ! hitLeft)
			{
				GameManager.theManager.DisableHidingUI();
				inRange = false;
			}
		}

		

    
	}
}