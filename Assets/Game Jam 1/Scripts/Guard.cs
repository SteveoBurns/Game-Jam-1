using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
