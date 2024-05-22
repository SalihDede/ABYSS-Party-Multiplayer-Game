using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesh : MonoBehaviour
{
    public AudioClip musicClip;  // Sürükle býrak için mp3 dosyanýzý buraya ekleyin.
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();  // AudioSource bileþenini ekle
        audioSource.clip = musicClip;  // mp3 dosyasýný ayarla
        audioSource.loop = true;  // Döngüde çalmasýný ayarla
        audioSource.spatialBlend = 1.0f;  // 3D ses için
        audioSource.minDistance = 1.0f;  // Sesin tam duyulduðu minimum mesafe
        audioSource.maxDistance = 10.0f;  // Sesin tamamen kesildiði maksimum mesafe
        audioSource.Play();  // Müziði çalmaya baþla
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
