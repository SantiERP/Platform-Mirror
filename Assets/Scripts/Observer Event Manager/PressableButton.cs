using UnityEngine;

public class PressableButton : Interruptor
{
    Vector3 desiredPosition;
    Vector3 normalPosition;

    int amountOfObjectsPressing;

    void Start()
    {
        normalPosition = transform.position;
        desiredPosition = normalPosition;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.01f);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing rebotable))
        {
            ButtonAction();
            amountOfObjectsPressing++;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing rebotable))
        {
            amountOfObjectsPressing--;

            if (amountOfObjectsPressing <= 0)
            {
                ButtonAntiAction(); 
            }
        }
    }

    public override void NormalAction()
    {
        desiredPosition = normalPosition - transform.up * 0.8f;
    }

    public override void AntiAction()
    {
        desiredPosition = normalPosition;
    }

}
