using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreEffectZone : MonoBehaviour
{
    [SerializeField] int _index;
    [Space]
    [SerializeField] SpriteRenderer _circle;
    Material _cachedCircleMaterial;

    const string ALPHA = "_Alpha";
    const string MIN_RADIUS = "_MinRadius";
    const string MAX_RADIUS = "_MaxRadius";

    // 모든 Zone의 Scale은 제일 큰 거 기준으로 맞춤. 그 안에서 비율로 두께가 정해짐.
    public void SetUp(int index, float totalMaxRadius, float minRadius, float maxRadius, float alpha)
    {
        _index = index;

        _circle.transform.localScale = Vector3.one * (totalMaxRadius * 2); // size의 2배를 해줘야 유니티에서 distance 1과 크기가 같길래 이렇게 처리함.

        if (_cachedCircleMaterial == null)
        {
            _cachedCircleMaterial = _circle.material;
        }
        _cachedCircleMaterial.SetFloat(ALPHA, alpha);
        _cachedCircleMaterial.SetFloat(MIN_RADIUS, minRadius / totalMaxRadius);
        _cachedCircleMaterial.SetFloat(MAX_RADIUS, maxRadius / totalMaxRadius);
    }

    public void Effect(Player player, Core core)
    {
        CoreEffectData data = Managers.Data.CoreEffectDatas[_index];

        player.Weapon.SetWeaponProperties(data.WeaponCount, data.WeaponSize, data.WeaponRotationSpeed, data.WeaponRange);
        player.PlayerMental.ChangeRate = data.PlayerMentalChangeRate;
        player.MoveSpeed = data.PlayerMoveSpeed;

        // 부스트
        player.Boost();

        core.SetLinkPower(_index);
    }
}
