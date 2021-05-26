using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCaveMusic : MonoBehaviour
{
    [SerializeField]
    private AudioClip music;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.PlayMusic(music);
        Destroy(this);
    }
}
