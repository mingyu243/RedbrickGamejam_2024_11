using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; // 초기 체력
    public Transform orb; // 오브의 Transform을 할당
    public float damageRange = 2f; // 체력이 감소되는 거리
    public int damageAmount = 5; // 감소할 체력 양
    public float damageInterval = 1.0f; // 체력 감소 간격 (초)

    private float lastDamageTime = 0f;

    private WeaponManager weaponManager;
    Animator animator;
    Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        weaponManager = FindObjectOfType<WeaponManager>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        // 오브와의 거리 계산
        float distanceToOrb = Vector2.Distance(transform.position, orb.position);

        // 오브가 일정 거리 내에 있으면 체력 감소
        if (distanceToOrb <= damageRange && Time.time >= lastDamageTime + damageInterval && health > 0)
        {
            TakeDamage(damageAmount);
            lastDamageTime = Time.time; // 마지막 피해 시간을 갱신
        }
    }

    public void InitializeHealth(int Hp)
    {
        health = Hp;
        Managers.Ui.Battle.SetPlayerHp(health);
    }

    // 데미지를 받는 메서드
    public void TakeDamage(int damage)
    {
        health -= damage; // 데미지만큼 체력 감소
        Managers.Ui.Battle.SetPlayerHp(health);
        Debug.Log("플레이어의 현재 체력: " + health);

        // 체력이 0 이하가 되면 죽음 처리
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("플레이어가 사망했습니다.");
        // 사망 처리 로직 (예: 게임 오버 화면 호출, 애니메이션 재생 등)
        Player player = GetComponent<Player>();
        weaponManager.RemoveAllWeapons();
        player.isLive = false;
        animator.SetTrigger("Dead");

        Managers.GamePlay.MainGame.GameResult = GameResult.PlayerDeath;
    }
}
