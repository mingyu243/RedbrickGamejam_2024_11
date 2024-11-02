using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Managers.IsInit);

        this.damage = Managers.Data.PlayerDatas[0].Attack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Enemy" 태그를 가진 경우 데미지를 입힘
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // 데미지를 전달
                AudioManager.instance.PlaySfx(AudioManager.Sfx.PlayerAttack);
                Debug.Log("적에게 " + damage + " 데미지를 입혔습니다!");
            }
        }
    }
}
