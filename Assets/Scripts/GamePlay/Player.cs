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
    [SerializeField] WeaponManager weaponManager;
    public WeaponManager Weapon => weaponManager;

    void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void InitState()
    {
        playerData = Managers.Data.PlayerDatas[0];

        //attack = playeraData.Attack;
        //attackSpeed = playeraData.AttackSpeed;
        //hp = playeraData.Hp;
        //moveSpeed = playeraData.MoveSpeed;

        Weapon.SetWeaponRange(playerData.WeaponRange);

        if (playerHealth != null)
        {
            playerHealth.InitializeHealth(playerData.Hp);
        }
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
    }

    void LateUpdate()
    {
        if (!isLive) return;
        //animator.SetFloat("Speed", inputVec.magnitude);

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
