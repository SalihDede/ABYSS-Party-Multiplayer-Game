using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesh : MonoBehaviour
{
    public AudioClip musicClip;  // S�r�kle b�rak i�in mp3 dosyan�z� buraya ekleyin.
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();  // AudioSource bile�enini ekle
        audioSource.clip = musicClip;  // mp3 dosyas�n� ayarla
        audioSource.loop = true;  // D�ng�de �almas�n� ayarla
        audioSource.spatialBlend = 1.0f;  // 3D ses i�in
        audioSource.minDistance = 1.0f;  // Sesin tam duyuldu�u minimum mesafe
        audioSource.maxDistance = 10.0f;  // Sesin tamamen kesildi�i maksimum mesafe
        audioSource.Play();  // M�zi�i �almaya ba�la
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
