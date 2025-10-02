using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left;
        }
    }
}
