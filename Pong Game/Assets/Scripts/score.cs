using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{
    [SerializeField] private int id;
    private int value;

    void Start()
    {
        this.value = 0;

        ball.Score += (int id) => 
        {
            if(id == this.id)
            {
                this.value += 1;
                this.GetComponent<TMPro.TextMeshPro>().SetText(value.ToString());
            }
        };
    }
}
