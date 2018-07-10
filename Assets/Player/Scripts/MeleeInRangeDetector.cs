using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeInRangeDetector : MonoBehaviour {

    bool isInMeleeRange;
    List<IDamageable> objectsInRange = new List<IDamageable>();

    private void OnTriggerEnter2D(Collider2D obj)
    {
        Debug.Log(obj.gameObject.name + " has entered the melee range");
        IDamageable killableObj = obj.gameObject.GetComponent<IDamageable>();
        if (killableObj != null)
        {
            isInMeleeRange = true;
            if (!objectsInRange.Contains(killableObj))
            {
                objectsInRange.Add(killableObj);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.gameObject.GetComponent<IDamageable>() != null)
        {
            isInMeleeRange = true;
        }
    }



    private void OnTriggerExit2D(Collider2D obj)
    {
        IDamageable killableObj = obj.gameObject.GetComponent<IDamageable>();
        if (killableObj != null)
        {
            isInMeleeRange = false;
            if (objectsInRange.Contains(killableObj))
            {
                objectsInRange.Remove(killableObj);
            }
        }
    }

    public List<IDamageable> GetListOfObjInMeleeRange()
    {
        return objectsInRange;
    }

}
