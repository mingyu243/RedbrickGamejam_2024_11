using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("카메라 바운더리")]
    [SerializeField] PolygonCollider2D _cameraBoundary;

    [Header("맵 바운더리")]
    [SerializeField] BoxCollider2D[] _mapBoundarys;


    IEnumerator Start()
    {
        yield return new WaitUntil(() => Managers.IsInit);

        SetMapSize(Managers.Data.ConfigDatas[0].MapSize);
    }

    void SetMapSize(float size)
    {
        Managers.Camera.MinimapCam.orthographicSize = size;

        Vector2[] points = new Vector2[]
        {
            new Vector2 (size, size),
            new Vector2 (-size, size),
            new Vector2 (-size, -size),
            new Vector2 (size, -size),
        };
        _cameraBoundary.points = points;

        _mapBoundarys[0].transform.position = new Vector2(0, -size - 1.5f);
        _mapBoundarys[1].transform.position = new Vector2(0, size + 1.5f);
        _mapBoundarys[2].transform.position = new Vector2(size + 1.5f, 0);
        _mapBoundarys[3].transform.position = new Vector2(-size - 1.5f, 0f);
    }
}
