using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private LayerMask wallMask;
    void Start()
    {
    }
    void Update()
    {
        Move();
    }
    public void Move(Vector3 direction = default)
    {
        if(direction != Vector3.zero)
        {
            transform.position += direction;
            return;
        }
        Vector3 dir = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W))
        {
            dir  = transform.position + Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dir = transform.position + Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dir = transform.position + Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dir = transform.position + Vector3.left;
        }
        if (Physics2D.BoxCast(transform.position, transform.localScale, 0, transform.position + dir * 2,0, wallMask))
        {
            return;
        }
        if(dir != Vector3.zero)
        {
            transform.position = dir;
        }
    }
    private void OnCollisionEnter2D ( Collision2D collision )
    {
        if(collision.gameObject.layer == 3)
        {
            print("He tocado la salida");
            SceneSystem.instance.LoadNextScene();
        }
    }
}
