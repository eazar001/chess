using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The bishop can move in any diagonal.
/// </summary>
public class Bishop: Piece {

    public override bool ValidMove(Vector2 pos) {
        return true;

    }
}
