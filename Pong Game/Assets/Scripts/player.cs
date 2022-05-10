using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    private float pace;
    private Vector3 velocity;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.startPosition = this.transform.position;
        this.pace = 10f;
        this.velocity = new Vector3(0, 0, 0);
        ball.Score += (int id) =>
        {
            this.transform.position = this.startPosition;
        };
    }

    // Update is called once per frame
    void Update()
    {
        this.velocity = new Vector3(0, 0, 0);

        if (Input.GetKey(this.upKey))
        {
            this.velocity = new Vector3(0, this.pace, 0);
        }
        if (Input.GetKey(this.downKey))
        {
            this.velocity = new Vector3(0, -this.pace, 0);
        }

        this.transform.position += this.velocity * Time.deltaTime;

        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        float heightOverTwo = this.transform.lossyScale.y / 2;
        Vector3 thisPosition = this.transform.position;
        if(thisPosition.y - heightOverTwo < -screenBounds.y)
        {
            thisPosition.y = heightOverTwo - screenBounds.y;
        }
        else if(thisPosition.y + heightOverTwo > screenBounds.y)
        {
            thisPosition.y = screenBounds.y - heightOverTwo;
        }

        this.transform.position = thisPosition;
    }
}
