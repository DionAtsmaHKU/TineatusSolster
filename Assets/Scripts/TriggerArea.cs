using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [SerializeField] AreaMusic areaMusic;
    [SerializeField] bool rich;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rich)
                areaMusic.RichMiddle();
            else
                areaMusic.PoorMiddle();
        }
    }
}
