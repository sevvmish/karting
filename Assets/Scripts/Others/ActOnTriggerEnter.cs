using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActOnTriggerEnter : MonoBehaviour
{
    public GameObject[] toEnable;
    public GameObject[] toDisable;

    private void Start()
    {
        if (toEnable.Length == 0 && toDisable.Length == 0) Destroy(gameObject);

        if (gameObject.TryGetComponent(out MeshRenderer m))
        {
            m.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (toEnable.Length == 0 && toDisable.Length == 0) return;

        if (other.TryGetComponent(out MainPlayerDrivingHelper main))
        {
            if (toEnable.Length > 0)
            {
                for (int i = 0; i < toEnable.Length; i++)
                {
                    toEnable[i].SetActive(true);
                }
            }

            if (toDisable.Length > 0)
            {
                for (int i = 0; i < toDisable.Length; i++)
                {
                    toDisable[i].SetActive(false);
                }
            }
        }
    }
}
