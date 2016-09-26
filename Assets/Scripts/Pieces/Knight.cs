using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The knight piece.
/// </summary>
public class Knight: Piece {

    public override bool ValidMove(Vector2 pos) {
        Vector2 myPos = transform.position;

        if(pos == myPos+k1 || pos == myPos+k2 || pos == myPos+k3 || pos == myPos+k4
            || pos == myPos+k5 || pos == myPos+k6 || pos == myPos+k7 || pos == myPos+k8) {

            return true;
        }

        return false;
    }
}
