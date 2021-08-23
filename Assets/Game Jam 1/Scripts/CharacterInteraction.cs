using GameJam.Manager;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Character
{
	public class CharacterInteraction : MonoBehaviour
	{
		[SerializeField] private float raycastDist = 3f;
		private bool inRange = false;
		private bool inHiding = false;
		private Image currentImage;
		private SpriteRenderer rend;

		private CharacterMotor motor;
		

		private RaycastHit2D hitRight;
		private RaycastHit2D hitLeft;
		[SerializeField] private Transform rayOriginRight;
		[SerializeField] private Transform rayOriginLeft;

		private void Start()
		{
			motor = GetComponent<CharacterMotor>();
			rend = GetComponentInParent<SpriteRenderer>();
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
			
			if(inHiding && inRange && Input.GetKeyDown(KeyCode.Space))
			{
				inHiding = false;
				rend.enabled = true;
				motor.enabled = true;
				GameManager.theManager.EnterHiding();

			}
			
		}

		/// <summary>
		/// Cast a ray and detect any Interactable objects, then enable the UI on those objects.
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

		private void Update()
		{
			Hiding();
		}

		private void FixedUpdate()
		{
			InteractablesCast();
			
		}

    
	}
}