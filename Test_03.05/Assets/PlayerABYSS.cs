using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerABYSS : MonoBehaviour
{
    public Animator Anim;

    public float shotForce = 500f; // Şut kuvveti
    private bool canShoot = true; // Tekrar tekrar şut yapmayı engellemek için kullanılacak flag

    void Start()
    {
        
    }

    void Update()
    {
        // Koşma animasyonunu kontrol et
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            Anim.SetBool("IsRun", true);
        }
        else
        {
            Anim.SetBool("IsRun", false);
        }

        // Zıplama animasyonunu kontrol et
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Anim.SetBool("IsJump", true);
        }
        else
        {
            Anim.SetBool("IsJump", false);
        }

        // Topa vurma işlevselliği
        if (Input.GetKeyDown(KeyCode.F) && canShoot)
        {
            ShootBall();
        }
    }

    // Topa vurma fonksiyonu
    private void ShootBall()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                Rigidbody ballRb = hit.collider.GetComponent<Rigidbody>();
                if (ballRb != null)
                {
                    ballRb.AddForce(transform.forward * shotForce);
                    canShoot = false; // Tekrar tekrar şut yapmayı engelle
                    StartCoroutine(ResetShootFlag());
                }
            }
        }
    }

    // Tekrar tekrar şut yapmayı engelleme için flag resetleme fonksiyonu
    IEnumerator ResetShootFlag()
    {
        yield return new WaitForSeconds(1f); // 1 saniye sonra flag'i tekrar true yap
        canShoot = true;
    }
}
