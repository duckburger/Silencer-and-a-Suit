using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AptGenerator : MonoBehaviour {

    [SerializeField] List<GameObject> baseRoomPrefabs = new List<GameObject>();

    [Header("Corridor prefabs")]
    [SerializeField] List<GameObject> topCorridorPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> bottomCorridorPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> leftCorridorPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> rightCorridorPrefabs = new List<GameObject>();

    [Header("Room prefabs")]
    [SerializeField] List<GameObject> topRoomPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> bottomRoomPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> leftRoomPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> rightRoomPrefabs = new List<GameObject>();


    [Header("Settings")]
    [SerializeField] int minFloors, maxFloors;
    [SerializeField] int minRoomsPerFloor, maxRoomsPerFloor;

    public void GenerateApt()
    {
        // Determine amount of floors in this apartmnet
        int floorsToMake = Random.Range(minFloors, maxFloors);

        for (int i = 0; i < floorsToMake; i++)
        {
            GenerateFloor();
        }
    }

    private void GenerateFloor()
    {
        // Determing amount of rooms on this floor
        int roomsToMake = Random.Range(minRoomsPerFloor, maxRoomsPerFloor);
        //bool rotated = RandomizeDecision();
        int baseRoomIndex = Random.Range(0, baseRoomPrefabs.Count - 1);
        GameObject newBaseRoom = Instantiate(baseRoomPrefabs[baseRoomIndex], Vector2.zero, Quaternion.identity);
        //if (rotated)
        //{
        //    newBaseRoom.transform.Rotate(Vector3.forward, 90);
        //}

        // Go through each attachment point and fill it randomly up to the overall room limit
        List<Transform> baseRoomAttachmentPoints = newBaseRoom.GetComponent<RoomData>().GetAttachmentPoints();
        for (int i = 0; i < roomsToMake; i++)
        {
            bool isSpawning = RandomizeDecision();
            if (!isSpawning)
            {
                continue;
            }
            int index = Random.Range(0, baseRoomAttachmentPoints.Count);
            RoomAttachmentPoint currAttachmentPoint = baseRoomAttachmentPoints[index].GetComponent<RoomAttachmentPoint>();
            if (!currAttachmentPoint.isOccupied)
            {
                CardinalSide sideOfAttachmentPoint = currAttachmentPoint.GetMyCardinalSide();
                List<GameObject> prefabsListToUse = new List<GameObject>();
                int roomIndex = 0;
                switch (sideOfAttachmentPoint)
                {
                    case CardinalSide.Left:
                        roomIndex = Random.Range(0, leftRoomPrefabs.Count);
                        prefabsListToUse = leftRoomPrefabs;
                        break;
                    case CardinalSide.Right:
                        roomIndex = Random.Range(0, rightRoomPrefabs.Count);
                        prefabsListToUse = rightRoomPrefabs;
                        break;
                    case CardinalSide.Bottom:
                        roomIndex = Random.Range(0, bottomRoomPrefabs.Count);
                        prefabsListToUse = bottomRoomPrefabs;
                        break;
                    case CardinalSide.Top:
                        roomIndex = Random.Range(0, topRoomPrefabs.Count);
                        prefabsListToUse = topRoomPrefabs;
                        break;
                    default:
                        break;
                }
                GameObject roomToSpawn = prefabsListToUse[roomIndex];
                currAttachmentPoint.isOccupied = true;
                GameObject newRoom = Instantiate(roomToSpawn, currAttachmentPoint.transform.position, Quaternion.identity, currAttachmentPoint.transform);
                CalcOffsetAndMoveNewRoom(newRoom, sideOfAttachmentPoint, currAttachmentPoint.transform);
            }
            else
            {
                continue;
            }
            
            
        }  

    }

    bool RandomizeDecision()
    {
        return (Random.value > 0.5f);
    }


    void CalcOffsetAndMoveNewRoom(GameObject objToMove, CardinalSide sideOfAttachmentPoint, Transform originalRoomAttachPoint)
    {
        List<Transform> attachmentPoints = objToMove.GetComponent<RoomData>().GetAttachmentPoints();
        Vector2 pos1 = originalRoomAttachPoint.position;
        Vector2 pos2 = Vector2.zero;
        foreach(Transform point in attachmentPoints)
        {
            switch (sideOfAttachmentPoint)
            {
                case CardinalSide.Left:
                    if(point.GetComponent<RoomAttachmentPoint>().GetMyCardinalSide() == CardinalSide.Right)
                    {
                        pos2 = point.position;
                        break;
                    }
                    break;
                case CardinalSide.Right:
                    if (point.GetComponent<RoomAttachmentPoint>().GetMyCardinalSide() == CardinalSide.Left)
                    {
                        pos2 = point.position;
                        break;
                    }
                    break;
                case CardinalSide.Bottom:
                    if (point.GetComponent<RoomAttachmentPoint>().GetMyCardinalSide() == CardinalSide.Top)
                    {
                        pos2 = point.position;
                        break;
                    }
                    break;
                case CardinalSide.Top:
                    if (point.GetComponent<RoomAttachmentPoint>().GetMyCardinalSide() == CardinalSide.Bottom)
                    {
                        pos2 = point.position;
                        break;
                    }
                    break;
                default:
                    break;
            }
        }

        Vector3 offset = pos1 - pos2;
        objToMove.transform.position += offset;

    }
}
