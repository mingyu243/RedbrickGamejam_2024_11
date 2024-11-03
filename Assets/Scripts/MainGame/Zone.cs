using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] int _zoneIndex;

    ZoneController _zoneController;

    [SerializeField] Transform[] _randomSpawnPosTrs;

    [SerializeField] TMP_Text _numberText;

    public Vector3 GetRandomPosition()
    {
        int randIndex = Random.Range(0, _randomSpawnPosTrs.Length);
        return _randomSpawnPosTrs[randIndex].position;
    }
    public void Show()
    {
        Debug.Log($"{_zoneIndex} 켜짐");
        _numberText.color = new Color(1, 1, 1, 1);
    }

    public void Hide()
    {
        Debug.Log($"{_zoneIndex} 꺼짐");
        _numberText.color = new Color(1, 1, 1, 67 / 255f);
    }

    private void Start()
    {
        _zoneController = Managers.GamePlay.MainGame.ZoneController;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_zoneController != null)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                _zoneController.OnTriggerStayPlayer(_zoneIndex);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_zoneController != null)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                _zoneController.OnTriggerExitPlayer(_zoneIndex);
            }
        }
    }
}
