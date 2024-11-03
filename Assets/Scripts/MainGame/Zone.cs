using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] int _zoneIndex;

    ZoneController _zoneController;

    [SerializeField] Transform[] _randomSpawnPosTrs;

    [SerializeField] ZoneActiveEffect _zoneActiveEffect;

    public Vector3 GetRandomPosition()
    {
        int randIndex = Random.Range(0, _randomSpawnPosTrs.Length);
        return _randomSpawnPosTrs[randIndex].position;
    }
    public void Show()
    {
        _zoneActiveEffect.Show();
    }

    public void Hide()
    {
        _zoneActiveEffect.Hide();
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
