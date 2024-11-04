using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] MonsterType monsterType;
    public Rigidbody2D target; // 타겟이 되는 오브젝트 (오브)

    [SerializeField] private int attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int hp; // MonsterData에서 가져오는 체력
    [SerializeField] private float moveSpeed;
    [SerializeField] private float avoidDistance = 1.0f; // 회피 거리
    [SerializeField] private float avoidStrength = 1.5f; // 회피 강도

    private float attackRange = 3.2f; // 오브 크기에 따라 바꿔줘야 함
    private float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    
    public bool isLive = true;
    private bool isAttacking = false;

    Rigidbody2D enemyRigid;
    SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;
    Animator animator;

    void Awake()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    public void InitState()
    {
        MonsterData monsterData = Managers.Data.MonsterDatas[(int)monsterType];

        attack = monsterData.Attack;
        attackSpeed = monsterData.AttackSpeed;
        hp = monsterData.Hp;
        moveSpeed = monsterData.MoveSpeed;

        // EnemyHealth의 체력을 MonsterData의 Hp로 설정
        if (enemyHealth != null)
        {
            enemyHealth.InitializeHealth(hp);
        }

        target = Managers.GamePlay.MainGame.Orb.Rb;
    }

    void FixedUpdate()
    {
        if (!isLive || target == null || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }

        // 공격 중이 아니라면 이동
        if (!isAttacking)
        {
            Vector2 dirVec = target.position - enemyRigid.position; // 목표 방향 계산
            Vector2 avoidVec = Vector2.zero;

            // 회피 벡터 계산
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, avoidDistance);
            foreach (var collider in nearbyEnemies)
            {
                if (collider != GetComponent<Collider2D>() && collider.CompareTag("Enemy"))
                {
                    Vector2 directionToOther = (Vector2)(transform.position - collider.transform.position);
                    float distance = directionToOther.magnitude;
                    avoidVec += directionToOther.normalized / distance; // 가까울수록 강하게 회피
                }
            }

            // 목표 방향과 회피 벡터를 결합하여 최종 이동 방향 결정
            Vector2 finalDirection = (dirVec.normalized + avoidVec * avoidStrength).normalized;
            Vector2 nextVec = finalDirection * moveSpeed * Time.fixedDeltaTime;

            enemyRigid.MovePosition(enemyRigid.position + nextVec);
            enemyRigid.velocity = Vector2.zero;
        }

        // 몬스터가 오브와 가까운지 확인하여 공격
        float distanceToTarget = Vector2.Distance(target.position, enemyRigid.position);
        if (distanceToTarget <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
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
                orbHealth.TakeDamage(attack); // 오브에 10의 데미지
                AudioManager.instance.PlaySfx(AudioManager.Sfx.EnemyAttack);
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
