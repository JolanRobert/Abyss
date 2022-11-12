using System.Collections;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private PlayerData data => playerController.data;
    [SerializeField] private Transform safeZone;
    [SerializeField] private float safeZoneTime;

    [SerializeField] private float cooldown;
    private bool canUseLight = true;

    public void Light()
    {
        if (canUseLight) 
        {
            StartCoroutine(SafeZoneCoroutine(safeZoneTime));
            StartCoroutine(CooldownCoroutine(cooldown));
        }
    }

    IEnumerator SafeZoneCoroutine(float t) 
    {
        safeZone.transform.position = transform.position;
        safeZone.gameObject.SetActive(true);
        yield return new WaitForSeconds(t);
        safeZone.gameObject.SetActive(false);
    }

    IEnumerator CooldownCoroutine(float t) 
    {
        canUseLight = false;
        yield return new WaitForSeconds(t);
        canUseLight = true;
    }
}
