using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour {

    [SerializeField] List<Transform> roomAttachmentPoints = new List<Transform>();

	public List<Transform> GetAttachmentPoints()
    {
        if (roomAttachmentPoints.Count <= 0)
        {
            return null;
        }
        return roomAttachmentPoints;
    }

}
