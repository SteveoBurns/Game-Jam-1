using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField] private float raycastRadius = 3f;

    private RaycastHit2D circle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
	    circle = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 5f);
	    //circle = Physics2D.CircleCast(this.transform.position, raycastRadius,transform.right);

	    if(circle)
         {
	         if(circle.collider.tag == "Interactable")
	         {
		         Image image = circle.transform.GetComponentInChildren<Image>();
		         if(image != null)
		         {
			         image.enabled = true;
		         }
		         Debug.Log("hit interactable");
	         }
	         Debug.Log("hit" + circle.collider.name);
         }
    }

    
}
