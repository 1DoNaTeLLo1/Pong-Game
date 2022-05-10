using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ball : MonoBehaviour
{
    [SerializeField] private float pace;
    private Vector3 velocity;
    public static event Action <int> Score;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.startPosition = this.transform.position;
        this.InitVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.velocity * Time.deltaTime;
        this.BounceFromWall();
        this.CheckIfScore();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.transform.position.y > collision.collider.bounds.min.y && this.transform.position.y < collision.collider.bounds.max.y)
        {
            Vector3 thisPosition = this.transform.position;
            float thisRadius = this.transform.lossyScale.x / 2;
            float colliderWidthOverTwo = collision.collider.bounds.size.x / 2;
            if(thisPosition.x < collision.collider.bounds.center.x)
            {
                thisPosition.x = collision.collider.bounds.center.x - colliderWidthOverTwo - thisRadius;
            }
            else
            {
                thisPosition.x = collision.collider.bounds.center.x + colliderWidthOverTwo + thisRadius;
            }
            this.transform.position = thisPosition;

            Vector3 direction = this.transform.position - collision.collider.bounds.center;
            direction = direction.normalized;
            this.velocity = direction * this.pace;
        }
        else
        {
            Vector3 thisPosition = this.transform.position;
            float thisRadius = this.transform.lossyScale.y / 2;
            float colliderHeightOverTwo = collision.collider.bounds.size.y / 2;
            if (thisPosition.y < collision.collider.bounds.center.y)
            {
                thisPosition.y = collision.collider.bounds.center.y - colliderHeightOverTwo - thisRadius;
            }
            else
            {
                thisPosition.y = collision.collider.bounds.center.y + colliderHeightOverTwo + thisRadius;
            }
            this.transform.position = thisPosition;

            this.velocity.y = -this.velocity.y;
        }
    }

    private void InitVelocity()
    {
        Vector3 direction = new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), 0);
        direction = direction.normalized;
        this.velocity = direction * this.pace;
    }

    private void Init()
    {
        this.transform.position = this.startPosition;
        this.InitVelocity();
    }

    private void BounceFromWall()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 thisPosition = this.transform.position;
        float radius = this.transform.lossyScale.x / 2;

        if (thisPosition.y - radius < -screenBounds.y)
        {
            thisPosition.y = radius - screenBounds.y;
            this.velocity.y *= -1;
        }
        else if (thisPosition.y + radius > screenBounds.y)
        {
            thisPosition.y = screenBounds.y - radius;
            this.velocity.y *= -1;
        }

        this.transform.position = thisPosition;
    }

    private void CheckIfScore()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 thisPosition = this.transform.position;
        float radius = this.transform.lossyScale.x / 2;

        if (thisPosition.x - radius < -screenBounds.x)
        {
            Score?.Invoke(0);
            this.Init();
        }
        else if (thisPosition.x + radius > screenBounds.x)
        {
            Score?.Invoke(1);
            this.Init();
        }
    }
}
