using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 50; // 기본 체력, MonsterData에 의해 덮어씌워질 예정
    private Animator animator;
    Enemy enemy;
    WaitForFixedUpdate wait;
    GameObject playerObject;

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        wait = new WaitForFixedUpdate();
        playerObject = GameObject.FindWithTag("Player");
    }

    // 체력 초기화 메서드
    public void InitializeHealth(int initialHealth)
    {
        health = initialHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("적의 현재 체력: " + health);

        // 피격 애니메이션 및 넉백 코루틴 실행
        if (enemy.isLive)
        {
            animator.SetTrigger("Hit");
            StartCoroutine(KnockBack());
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!enemy.isLive) return; // 이미 사망한 경우 중복 처리 방지

        enemy.isLive = false; // 사망 상태 설정
        Debug.Log("적이 사망했습니다.");

        // 사망 애니메이션 재생
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }

        // 사망 애니메이션이 끝난 후에 오브젝트 제거 (코루틴 사용)
        StartCoroutine(RemoveAfterDeath());
    }

    private System.Collections.IEnumerator RemoveAfterDeath()
    {
        // 사망 애니메이션 길이만큼 대기 후 오브젝트 제거
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator KnockBack()
    {
        yield return wait; //다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = playerObject.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        rigid.AddForce(dirVec.normalized * 1.5f, ForceMode2D.Impulse);
    }
}

