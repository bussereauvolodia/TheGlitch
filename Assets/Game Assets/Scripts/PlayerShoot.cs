using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject BulletPrefab;
    private float BulletSpeed = 10f;
    void Update()
    
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
        {
            var bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = BulletSpawnPoint.forward*BulletSpeed;
        }
    }
}



