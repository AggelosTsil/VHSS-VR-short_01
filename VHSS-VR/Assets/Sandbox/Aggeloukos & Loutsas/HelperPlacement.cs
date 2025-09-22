using UnityEngine;
using System.Collections;

public class HelperPlacement : MonoBehaviour
{
    public Camera playerCamera;
    public float distance = 2f;
    public float followDuration = 5f;   
    public float disableDelay = 10f;    

    private void OnEnable()
    {
        StartCoroutine(FollowThenFreezeThenDisable());
    }

    private IEnumerator FollowThenFreezeThenDisable()
    {
        float timer = 0f;
        while (timer < followDuration)
        {
            Vector3 forwardFlat = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z).normalized;
            transform.position = playerCamera.transform.position + forwardFlat * distance;
            Quaternion lookRotation = Quaternion.LookRotation(forwardFlat, Vector3.up);
            transform.rotation = lookRotation;
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(disableDelay);
        gameObject.SetActive(false);
    }
}
