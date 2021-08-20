using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
	public class CharacterInteraction : MonoBehaviour
	{
		[SerializeField] private float raycastDist = 3f;
		private bool inRange = false;
		private bool inHiding = false;
		private Image currentImage;
		private SpriteRenderer rend;

		private RaycastHit2D hit;
		[SerializeField] private Transform rayOriginRight;

		private void Start()
		{
			rend = GetComponentInParent<SpriteRenderer>();
		}

		/// <summary>
		/// Hiding the character when near an interactable object.
		/// </summary>
		private void Hiding()
		{
			if(inRange && !inHiding)
			{
				currentImage.enabled = true;
			}
			if(inRange && Input.GetKeyDown(KeyCode.E))
			{
				Debug.Log("pressed E");
				inHiding = true;
				rend.enabled = false;
				currentImage.enabled = false;
			}
			
			if(inHiding && inRange && Input.GetKeyDown(KeyCode.Space))
			{
				inHiding = false;
				rend.enabled = true;
				currentImage.enabled = true;
			}
			
		}

		/// <summary>
		/// Cast a ray and detect any Interactable objects, then enable the UI on those objects.
		/// </summary>
		private void InteractablesCast()
		{
			hit = Physics2D.Raycast(rayOriginRight.position, transform.TransformDirection(Vector2.right), raycastDist);

			if(hit)
			{
				if(hit.collider.tag == "Interactable")
				{
					Image image = hit.transform.GetComponentInChildren<Image>();
					if(image != null)
					{
						currentImage = image;
						inRange = true;
					}
			    
					Debug.Log("hit interactable");
				}
		    
				Debug.Log("hit" + hit.collider.name);
			}
			else if(!hit)
			{
				if(currentImage != null)
					currentImage.enabled = false;
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