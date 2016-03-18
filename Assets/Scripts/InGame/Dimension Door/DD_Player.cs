using UnityEngine;
using System.Collections;

public class DD_Player : MonoBehaviour
{
    public int maxHp;
    public int damage;

    DD_Game game;

    bool hurtEffecting = false;
    public int hp
    {
        get;
        private set;
    }

    void Awake()
    {
        hp = maxHp;

        game = GameObject.FindObjectOfType<DD_Game>();
    }

    public void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            OnDied();
            return;
        }
        if (!hurtEffecting)
            StartCoroutine(HurtEffect());

    }

    IEnumerator HurtEffect()
    {
        hurtEffecting = true;
        yield return StartCoroutine(SceneEffector.instance.CameraShake(0.5f, 0.05f, 0.5f));
        hurtEffecting = false;
    }

    void OnDied()
    {
        game.GameEnd();
    }
}