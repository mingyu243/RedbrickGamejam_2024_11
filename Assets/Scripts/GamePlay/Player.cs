using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 inputVec;
    public float speed;
    public float attackRange = 1.5f; // 근접 공격 범위
    public int attackDamage = 20;    // 공격 데미지

    Rigidbody2D playerRigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        // 마우스 왼쪽 버튼 클릭 시 공격
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        //위치 이동
        playerRigid.MovePosition(playerRigid.position + nextVec);
    }

    void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0 ? true : false;
        }
    }

    void Attack()
    {
        // 마우스 위치 기준으로 공격 방향 계산
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDir = (mousePos - transform.position).normalized;

        // 공격 범위 내의 적 감지
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, attackDir, attackRange);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                // 적에게 데미지 주기
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                    Debug.Log("적에게 " + attackDamage + " 데미지를 주었습니다!");
                }
            }
        }
    }
}
