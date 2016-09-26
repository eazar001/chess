using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The king piece.
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