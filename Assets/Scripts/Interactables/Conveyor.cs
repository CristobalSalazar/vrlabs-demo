using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private float force = 1;
    [SerializeField] private Transform endpoint;
    public bool isOn = true;

    public void SetOffState()
    {
        isOn = false;
    }

    public void SetOnState()
    {
        isOn = true;
    }

    void OnCollisionStay(Collision other)
    {
        if (isOn)
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, force * Time.deltaTime);
        }
    }
}