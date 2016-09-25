using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The knight has 8 L shaped move patterns.
/// </summary>
public class Knight: Piece {

    public override bool ValidMove(Vector2 pos) {
        return true;

    }
}
