using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] int _zoneIndex;

    ZoneController _zoneController;

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
