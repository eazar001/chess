using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The bishop can move in any diagonal.
/// </summary>
public class Bishop: Piece {

    public override void MoveTo(Vector2 pos) {
        PlaceAt(pos);

    }
}
