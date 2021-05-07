using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCameraShake : MonoBehaviour
{
    [SerializeField]
    private float shakeTime = 1;

    [SerializeField]
    private float minDist = 0.1f;
    [SerializeField]
    private float maxDist = 0.1f;

    FollowingCamera followingCamera;

    Vector3 iniPos;

    private void OnEnable()
    {
        Invoke("DeactivateScript", shakeTime);

        iniPos = transform.position;
    }

    private void Start()
    {
        followingCamera = GetComponent<FollowingCamera>();
    }

    private void Update()
    {
        if (followingCamera != null) followingCamera.DeactivateMovement(shakeTime);

        float x = Random.Range(minDist, maxDist);
        float y = Random.Range(minDist, maxDist);

        transform.Translate(iniPos + new Vector3(x, y, 0));
    }

    private void DeactivateScript() 
    {
        this.enabled = false;
    }
}
