using System.Collections;
using UnityEngine;

public class Slug : MonoBehaviour
{

    public float moveSpeed = 15;

    public Vector3 _velocity;

    private BoxController2D _controller;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(other.gameObject.GetComponent<Player>().GameOver());
        }
    }

    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<BoxController2D>();

    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > .01f)
        {
            UpdateMovement();
        }
    }

    private void UpdateMovement()
    {

        if (_controller.collisions.above || _controller.collisions.below)
        {
            _velocity.y = 0;
        }

        if (_controller.collisions.left || _controller.collisions.right)
        {
            if (!_justTurnedAround)
            {
                moveSpeed = -moveSpeed;
                GetComponent<SpriteRenderer>().flipX = ! GetComponent<SpriteRenderer>().flipX;
                StartCoroutine(TurnAround());
            }

        }

        _velocity.x =  moveSpeed;
        
        _velocity.y += Physics2D.gravity.x * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private bool _justTurnedAround;
    
    private IEnumerator TurnAround()
    {
        var timer = 3.0f;
        while (timer > .0f)
        {
            _justTurnedAround = true;
            timer -= Time.deltaTime;
            yield return null;
        }


        _justTurnedAround = false;

    }
}