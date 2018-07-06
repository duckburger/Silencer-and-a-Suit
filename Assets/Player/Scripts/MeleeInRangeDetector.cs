using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeInRangeDetector : MonoBehaviour {

    bool isInMeleeRange;
    List<IKillable> objectsInRange = new List<IKillable>();

    private void OnTriggerEnter2D(Collider2D obj)
    {
        Debug.Log(obj.gameObject.name + " has entered the melee range");
        IKillable killableObj = obj.gameObject.GetComponent<IKillable>();
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
        if (obj.gameObject.GetComponent<IKillable>() != null)
        {
            isInMeleeRange = true;
        }
    }



    private void OnTriggerExit2D(Collider2D obj)
    {
        IKillable killableObj = obj.gameObject.GetComponent<IKillable>();
        if (killableObj != null)
        {
            isInMeleeRange = false;
            if (objectsInRange.Contains(killableObj))
            {
                objectsInRange.Remove(killableObj);
            }
        }
    }

    public List<IKillable> GetListOfObjInMeleeRange()
    {
        return objectsInRange;
    }

}
