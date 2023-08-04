using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRuntime : MonoBehaviour
{
    public GameObject[] GameObjectsToDeactive;
    public GameObject[] GameObjectsToActive;

    public MeshRenderer[] MeshRendererToHide;
    public MeshRenderer[] MeshRendererToShow;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObjectsToDeactive.Length > 0)
        {
            for (int i = 0; i < GameObjectsToDeactive.Length; i++)
            {
                GameObjectsToDeactive[i].SetActive(false);
            }
        }

        if (GameObjectsToActive.Length > 0)
        {
            for (int i = 0; i < GameObjectsToActive.Length; i++)
            {
                GameObjectsToActive[i].SetActive(true);
            }
        }

        if (MeshRendererToHide.Length > 0)
        {
            for (int i = 0; i < MeshRendererToHide.Length; i++)
            {
                MeshRendererToHide[i].enabled = false;
            }
        }

        if (MeshRendererToShow.Length > 0)
        {
            for (int i = 0; i < MeshRendererToShow.Length; i++)
            {
                MeshRendererToShow[i].enabled = true;
            }
        }
    }
}
