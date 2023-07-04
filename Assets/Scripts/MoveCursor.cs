using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCursor : MonoBehaviour
{
    [SerializeField] TargetSystem _targetSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey (KeyCode.RightArrow))
        {
            this.transform.position = this.transform.position + new Vector3 (0.025f,0,0);
        }
        if(Input.GetKey (KeyCode.LeftArrow))
        {
            this.transform.position = this.transform.position + new Vector3 (-0.025f,0,0);
        }
        if(Input.GetKey (KeyCode.UpArrow))
        {
            this.transform.position = this.transform.position + new Vector3 (0,0.025f,0);
        }
        if(Input.GetKey (KeyCode.DownArrow))
        {
            this.transform.position = this.transform.position + new Vector3 (0,-0.025f,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = this.transform.position + new Vector3(0.025f, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = this.transform.position + new Vector3(-0.025f, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = this.transform.position + new Vector3(0, 0.025f, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position = this.transform.position + new Vector3(0, -0.025f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.transform.position = new Vector3(0, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            _targetSystem.CollisionNumber += 1;
            Destroy(collision.gameObject);
        }
    }

}
