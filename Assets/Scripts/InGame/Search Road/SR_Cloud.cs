using UnityEngine;
using System.Collections;

public class SR_Cloud : MonoBehaviour
{

    public float speed;
    public bool isMoveLeft;


    void Awake()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * (isMoveLeft == true ? 1 : -1) * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 newPos;
        newPos.x = 13f;
        newPos.y = Random.Range(-3f, 3f);
        transform.localPosition = newPos;
    }
}