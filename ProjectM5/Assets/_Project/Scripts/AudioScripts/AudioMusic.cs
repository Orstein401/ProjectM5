using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMusic : MonoBehaviour
{
    [SerializeField] private SoundID typeMusic;

    private void Start()
    {
        AudioManager.Instance.PlaySound(typeMusic);
    }
}
