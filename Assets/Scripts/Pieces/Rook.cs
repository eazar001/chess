using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The rook can move straight in any direction, but the diagonal ones.
/// </summary>
public class Rook: Piece {

    public override bool ValidMove(Vector2 pos) {
        return true;

    }
}