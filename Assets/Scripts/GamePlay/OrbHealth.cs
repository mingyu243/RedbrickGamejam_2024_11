using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbHealth : MonoBehaviour
{
    public int health = 100; // 오브의 초기 체력

    // 데미지를 받는 메서드
    public void TakeDamage(int damage)
    {
        health -= damage; // 데미지만큼 체력 감소
        Debug.Log("오브의 현재 체력: " + health);

        // 체력이 0 이하가 되면 파괴 처리
        if (health <= 0)
        {
            Die();
        }
    }

    // 오브가 파괴되었을 때의 처리
    void Die()
    {
        Debug.Log("오브가 파괴되었습니다!");
        Destroy(gameObject); // 오브 오브젝트 삭제
    }
}

