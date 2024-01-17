using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoneCode : MonoBehaviour
{
    public GameObject CodedZonePrefab;
    private GameObject Player;
    private Vector3 Playerpos;
    public int PlatformCount;
    public int PlatformCountLimit;
    public GameObject PlatformTemplate;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player" && PlatformCount<PlatformCountLimit)
        {
            Player = collision.gameObject;
            Playerpos = Player.transform.position;
            if (Input.GetKey(KeyCode.A))
            {
                var platform = Instantiate(CodedZonePrefab, new Vector3(Playerpos.x, Playerpos.y - 1.5f, Playerpos.z), new Quaternion(0, 0, 0, 0));
                platform.GetComponent<CodedPlatform>().CodeZoneParent = gameObject;
                PlatformCount++;
            }
            PlatformTemplate.transform.position = new Vector3(Playerpos.x, Playerpos.y - 1.5f, Playerpos.z);
            PlatformTemplate.GetComponent<CodedPlatformTemplate>().playerInZone = true;
        }
        else if (collision.gameObject.tag == "Player")
        {
            PlatformTemplate.GetComponent<CodedPlatformTemplate>().playerInZone = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            PlatformTemplate.GetComponent<CodedPlatformTemplate>().playerInZone = false;
        }
    }

}
