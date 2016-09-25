using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The king's moveset is precisely like the queen's barring one tenet: it's directional magnitude
/// can only be the absolute value of 1. Since piece movetypes are expressed in directions only,
/// the queen's implementation is identical to the king's.
/// </summary>
public class King: Piece {

    public override bool ValidMove(Vector2 pos) {
        return true;

    }
}