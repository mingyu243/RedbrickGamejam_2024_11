using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 inputVec;
    public float speed;

    Rigidbody2D playerRigid;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        //위치 이동
        playerRigid.MovePosition(playerRigid.position + nextVec);
    }

    void LateUpdate()
    {
        if(inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0 ? true : false;
        }
    }
}