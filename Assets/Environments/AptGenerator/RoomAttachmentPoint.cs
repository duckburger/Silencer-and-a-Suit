using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardinalSide
{
    Left,
    Right,
    Bottom,
    Top
}

public class RoomAttachmentPoint : MonoBehaviour {


    [SerializeField] CardinalSide pointLocaton;
    public bool isOccupied;


    public CardinalSide GetMyCardinalSide()
    {
        return pointLocaton;
    }
}
