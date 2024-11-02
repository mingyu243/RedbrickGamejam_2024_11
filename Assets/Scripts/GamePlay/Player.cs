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

    void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        // DataManager에서 PlayerData를 불러오는 코루틴 시작
        StartCoroutine(InitializePlayerData());
    }

    IEnumerator InitializePlayerData()
    {
        // DataManager 인스턴스를 찾아 Init 호출 후 PlayerData 설정
        GameObject managers = GameObject.Find("Managers"); // 부모 오브젝트 찾기
        DataManager dataManager = managers.GetComponentInChildren<DataManager>(); // 자식에서 DataManager 찾기
       
        // DataManager에서 데이터를 초기화할 때까지 대기
        yield return dataManager.Init();

        // 초기화된 PlayerData의 첫 번째 데이터를 가져오기 (필요에 따라 인덱스를 변경 가능)
        playerData = dataManager.PlayerDatas[0];

        playerHealth.InitializeHealth(playerData.Hp);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLive) return;

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
        if (!isLive) return;
        Vector2 nextVec = inputVec.normalized * playerData.MoveSpeed * Time.fixedDeltaTime;
        //위치 이동
        playerRigid.MovePosition(playerRigid.position + nextVec);
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
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, attackDir, playerData.AttackRange);

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
