using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D target; // 타겟이 되는 오브젝트 (오브)

    [SerializeField] private float attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int hp; // MonsterData에서 가져오는 체력
    [SerializeField] private float moveSpeed;

    private float attackRange = 1.5f;
    private float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    public bool isLive = true;
    private bool isAttacking = false;

    Rigidbody2D enemyRigid;
    SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;

    void Awake()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InitState()
    {
        MonsterData monsterData = Managers.Data.MonsterDatas[0];

        attack = monsterData.Attack;
        attackSpeed = monsterData.AttackSpeed;
        hp = monsterData.Hp;
        moveSpeed = monsterData.MoveSpeed;

        // EnemyHealth의 체력을 MonsterData의 Hp로 설정
        if (enemyHealth != null)
        {
            enemyHealth.InitializeHealth(hp);
        }
    }

    void FixedUpdate()
    {
        if (!isLive || target == null) // target이 null인지 확인
        {
            return;
        }

        // 공격 중이 아니라면 이동
        if (!isAttacking || isLive)
        {
            MoveTowardsTarget();
        }

        // 몬스터가 오브와 가까운지 확인하여 공격
        float distanceToTarget = Vector2.Distance(target.position, enemyRigid.position);
        if (distanceToTarget <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time; // 공격 시간 갱신
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 dirVec = target.position - enemyRigid.position; // 방향 계산
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        enemyRigid.MovePosition(enemyRigid.position + nextVec);
        enemyRigid.velocity = Vector2.zero;
    }

    void Attack()
    {
        // 공격을 시작하고 이동을 멈추게 설정
        isAttacking = true;

        // 오브가 존재하는지 확인하고 공격 수행
        if (target != null)
        {
            Debug.Log("몬스터가 오브를 공격합니다!");
            OrbHealth orbHealth = target.GetComponent<OrbHealth>();
            if (orbHealth != null)
            {
                orbHealth.TakeDamage(10); // 오브에 10의 데미지
            }
        }

        // 일정 시간이 지나면 다시 이동할 수 있도록 코루틴으로 처리
        StartCoroutine(EndAttack());
    }

    System.Collections.IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(attackCooldown); // 쿨타임만큼 대기
        isAttacking = false; // 공격 중 상태 해제
    }

    void LateUpdate()
    {
        if (!isLive || target == null) // target이 null인지 확인
        {
            return;
        }

        // 방향에 따라 스프라이트 플립
        spriteRenderer.flipX = target.position.x > enemyRigid.position.x ? false : true;
    }
}
