using System.Collections;
using UnityEngine;

public class Agua : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ModelPlayer>(out ModelPlayer player))
        {
            StartCoroutine("Wait");
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(.75f);

        EventManager.TriggerEvent(EventManager.EventsType.Event_PlayerDead);
    }
}
