using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public AudioSource shootaudio;
    // Start is called before the first frame update

    public GameObject bulletPref;
    public Transform shootPoint;
    public GameObject laser;
    public Camera Camera;
    public float reloadTime = 1f;
    public float cooldown = 0.7f;

    Vitals playerVitals;

    [Space]
    public float shootingSpeed;
    public float gravityForce;
    public float bulletLifeTime;


    float t=0;

    private void Start()
    {
        playerVitals = GetComponent<Vitals>();
    }

    private void Update()
    {
        laser.transform.forward = (shootPoint.position - Camera.transform.position);
        t -= Time.deltaTime;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnShoot()
    {
        if (t <= 0 && playerVitals.Vaccines > 0)
        {
            shootaudio.Play();
            t = cooldown;
            GameObject bullet = Instantiate(bulletPref, shootPoint.position, shootPoint.rotation);
            playerVitals.Vaccines--;
            ParabolicBullet bulletScript = bullet.GetComponent<ParabolicBullet>();
            if (bulletScript)
            {
                bulletScript.Initialize(shootPoint, shootingSpeed, (shootPoint.position - Camera.transform.position).normalized, gravityForce);
            }
            Destroy(bullet, bulletLifeTime);
        }
    }
}
