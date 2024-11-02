using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject weaponPrefab; // 무기 프리팹
    public Transform player;        // 플레이어의 Transform
    private List<GameObject> weapons = new List<GameObject>();

    public float rotationSpeed;
    public int weaponCount;
    public float weaponRange; // 반지름

    private int previousWeaponCount = -1; // 이전 무기 개수 기록

    void Start()
    {
        InitializeWeapons();
    }

    void Update()
    {
        RotateWeaponsAroundPlayer();
    }

    // ZoneData에 따른 무기 초기화
    private void InitializeWeapons()
    {
        // ZoneData에서 회전 속도와 무기 개수 설정
        //rotationSpeed = Managers.Data.ZoneDatas[0].RotationSpeed;
        //weaponCount = Managers.Data.ZoneDatas[0].WeaponCount;

        // 이전 무기 위치 저장용 배열 생성
        Transform[] lastTrans = new Transform[weapons.Count];
        int idx = 0;
        foreach (var weapon in weapons)
        {
            lastTrans[idx++] = weapon.transform;
            Destroy(weapon);
        }
        weapons.Clear();

        // 무기 생성
        for (int i = 0; i < weaponCount; i++)
        {
            GameObject weapon = Instantiate(weaponPrefab, transform);
            float angle = i * Mathf.PI * weaponRange / weaponCount; // 각도 계산
            Vector3 weaponPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * weaponRange;
            // 이전 위치 적용, 존재하지 않으면 기본 위치 적용
            if (previousWeaponCount != -1 && previousWeaponCount == weaponCount && i < lastTrans.Length)
            {
                weapon.transform.position = lastTrans[i].position;
            }
            else
            {
                weapon.transform.position = player.position + weaponPosition;
            }
            weapon.transform.SetParent(player); // 무기를 플레이어의 자식으로 설정
            weapons.Add(weapon);
        }
    }

    // 무기를 플레이어 주위로 회전
    private void RotateWeaponsAroundPlayer()
    {
        float angle = rotationSpeed * Time.deltaTime;
        foreach (var weapon in weapons)
        {
            weapon.transform.RotateAround(player.position, Vector3.forward, angle);
        }
    }

    // 모든 무기를 제거하는 메서드
    public void RemoveAllWeapons()
    {
        foreach (var weapon in weapons)
        {
            Destroy(weapon);
        }
        weapons.Clear();
    }

    public void SetWeaponRange(float range)
    {
        weaponRange = range;
    }

    // 무기의 개수와 회전 속도를 설정하는 함수
    public void SetWeaponProperties(int newWeaponCount, float newRotationSpeed)
    {
        previousWeaponCount = weaponCount;
        weaponCount = newWeaponCount;
        rotationSpeed = newRotationSpeed;
        InitializeWeapons();
    }
}
