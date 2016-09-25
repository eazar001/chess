using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The king's moveset is precisely like the queen's barring one tenet: it's directional magnitude
/// can only be the absolute value of 1. Since piece movetypes are expressed in directions only,
/// the queen's implementation is identical to the king's.
/// </summary>
public class King: Piece {

    bool firstMove = true;

    public override bool ValidMove(Vector2 pos) {
        Vector2 d1 = new Vector2(xMove, yMove);
        Vector2 d2 = new Vector2(-xMove, yMove);
        Vector2 d3 = new Vector2(xMove, 0);
        Vector2 myPos = transform.position;

        if((pos == myPos + y1) || pos == myPos + d2 || pos == myPos - y1 || pos == myPos - d1
            || pos == myPos - d2 || pos == myPos + d1 || pos == myPos + d3 || pos == myPos - d3) {

            return true;
        }

        return false;
    }
}