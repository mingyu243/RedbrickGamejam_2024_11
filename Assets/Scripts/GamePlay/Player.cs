using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 inputVec;
    public PlayerData playerData;
    public bool isLive = true;
    
    Rigidbody2D playerRigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    PlayerHealth playerHealth;
    PlayerMental playerMental;
    [SerializeField] WeaponManager weaponManager;
    public WeaponManager Weapon => weaponManager;
    public PlayerHealth PlayerHealth => playerHealth;
    public PlayerMental PlayerMental => playerMental;

    private float moveSoundCooldown = 0.7f; // 사운드 재생 간격 (초)
    private float lastMoveSoundTime; // 마지막으로 사운드가 재생된 시간

    void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        playerMental = GetComponent<PlayerMental>();
    }

    public void InitState()
    {
        if(!isLive)
        {
            isLive = true;
            animator.SetTrigger("Retry");
        }
        playerData = Managers.Data.PlayerDatas[0];

        //attack = playeraData.Attack;
        //attackSpeed = playeraData.AttackSpeed;
        //hp = playeraData.Hp;
        //moveSpeed = playeraData.MoveSpeed;

        Weapon.SetWeaponRange(playerData.WeaponRange);
        PlayerHealth.InitializeHealth(playerData.Hp);
        PlayerMental.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLive) return;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        //// 마우스 왼쪽 버튼 클릭 시 공격
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Attack();
        //}
    }

    private void FixedUpdate()
    {
        if (!isLive) return;
        Vector2 nextVec = inputVec.normalized * playerData.MoveSpeed * Time.fixedDeltaTime;
        //위치 이동
        playerRigid.MovePosition(playerRigid.position + nextVec);


        // 현재 시간이 마지막 재생 시간 + 쿨다운보다 큰지 확인
        if (Time.time >= lastMoveSoundTime + moveSoundCooldown && animator.GetFloat("Speed") > 0)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Move);
            lastMoveSoundTime = Time.time; // 마지막 재생 시간을 갱신
        }
    }

    void LateUpdate()
    {
        if (!isLive) return;
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
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, attackDir, playerData.WeaponRange);
        animator.SetTrigger("Attack");

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                // 적에게 데미지 주기
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(playerData.Attack);
                    Debug.Log("적에게 " + playerData.Attack + " 데미지를 주었습니다!");
                }
            }
        }
    }
}
