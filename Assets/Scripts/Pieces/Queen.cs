using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The queen can move in any direction.
/// </summary>
public class Queen: Piece {

    public override bool ValidMove(Vector2 pos) {
        return true;

    }
}
