using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The rook can move straight in any direction, but the diagonal ones.
/// </summary>
public class Rook: Piece {

    public override void MoveTo(Vector2 pos) {
        PlaceAt(pos);

    }
}