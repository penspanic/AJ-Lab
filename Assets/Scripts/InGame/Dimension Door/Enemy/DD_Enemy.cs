using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DD_Enemy : MonoBehaviour, IPointerDownHandler
{
    public int damage;
    public int hp;

    DD_Player player;

    float moveTime = 0;

    public bool isDestroyed
    {
        get;
        private set;
    }
    protected virtual void Awake()
    {
        moveTime = Random.Range(2, 3);
        player = GameObject.FindObjectOfType<DD_Player>();
    }

    protected virtual void Start()
    {
        StartCoroutine(Move());
    }

    protected virtual void Update()
    {
        if(!isDestroyed)
            Move();
    }

    public void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            OnDied();
    }

    void OnDied()
    {
        isDestroyed = true;
        Destroy(this.gameObject);
    }

    IEnumerator Move()
    {
        float elapsedTime = 0f;

        Vector2 startPos = transform.position;
        Vector2 endPos = player.transform.position;

        while(!isDestroyed && elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = EasingUtil.EaseVector2(EasingUtil.linear, startPos, endPos, elapsedTime / moveTime);
            
            yield return null;
        }
        player.Damaged(damage);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Damaged(player.damage);
    }
}