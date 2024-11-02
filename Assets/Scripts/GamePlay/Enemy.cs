using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D EnemyRigid;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        EnemyRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        if(!isLive)
        {
            return;
        }

        Vector2 dirVec = target.position - EnemyRigid.position; //방향
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        EnemyRigid.MovePosition(EnemyRigid.position + nextVec);
        EnemyRigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }

        spriteRenderer.flipX = target.position.x > EnemyRigid.position.x ? false : true;
    }
}
