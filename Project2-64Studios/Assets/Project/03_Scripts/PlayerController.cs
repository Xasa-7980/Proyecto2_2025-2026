using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private LayerMask blockMask;
    public bool blockMovement;
    void Start()
    {
    }
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.U))
        {
            SkipLevel();
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            GoBackLevel();
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            ResetLevel();
        }
    }
    public void Move ( Vector3 direction = default )
    {
        if (direction != Vector3.zero)
        {
            TryMove(direction);
            return;
        }
        if (!blockMovement)
        {
            Vector3 dir = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.W)) dir = Vector3.up;
            else if (Input.GetKeyDown(KeyCode.S)) dir = Vector3.down;
            else if (Input.GetKeyDown(KeyCode.D)) dir = Vector3.right;
            else if (Input.GetKeyDown(KeyCode.A)) dir = Vector3.left;

            if (dir != Vector3.zero) TryMove(dir);
        }
    }

    void SkipLevel ( )
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void ResetLevel ( )
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    void GoBackLevel ( )
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
    private void TryMove ( Vector3 dir )
    {
        Vector3 targetPos = transform.position + dir;

        Collider2D hit = Physics2D.OverlapBox(targetPos, new Vector2(0.65f, 0.65f), 0f, blockMask);

        if (hit == null)
        {
            transform.position = targetPos;
        }
    }
    private void OnTriggerEnter2D ( UnityEngine.Collider2D collision )
    {
        if(collision.gameObject.layer == 3)
        {
            SceneSystem.instance.LoadNextScene();
            GetComponent<FileController>().ResetFiles();
        }
    }
}
