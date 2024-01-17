using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject WallR,WallL;
    public GameObject Camera;
    public Transform Background;
    public GameObject Boss;
    public GameObject cameraPlayer;
    public Transform Player;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !Boss.activeSelf)
        {
            WallR.SetActive(true);
            WallL.SetActive(true);
            Boss.SetActive(true);
            cameraPlayer.GetComponent<CinemachineVirtualCamera>().Follow = Camera.transform;
            for (int i = 0; i < Background.childCount; i++)
            {
                Background.GetChild(i).GetComponent<Parallax>().enabled = false;
            }
            Boss.GetComponent<BossFight>().audioManager.volume = 0.1f;
        }
    }
    public void BossReset()
    {
        WallR.SetActive(false);
        WallL.SetActive(false);
        cameraPlayer.GetComponent<CinemachineVirtualCamera>().Follow = Player;
        for (int i = 0; i < Background.childCount; i++)
        {
            Background.GetChild(i).GetComponent<Parallax>().enabled = true;
        }
    }
    public void BossDeath()
    {
        WallR.SetActive(false);
        WallL.SetActive(false);
        cameraPlayer.GetComponent<CinemachineVirtualCamera>().Follow = Player;
        for (int i = 0; i < Background.childCount; i++)
        {
            Background.GetChild(i).GetComponent<Parallax>().enabled = true;
        }
        Destroy(gameObject);
    }
}
