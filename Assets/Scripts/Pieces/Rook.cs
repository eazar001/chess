using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The rook piece.
/// </summary>
public class Rook: Piece {
    
    bool firstMove = true;

    public bool FirstMove {
        get { return firstMove; }
        set { firstMove = value; }
    }

    public override bool ValidMove(Vector2 pos) {
        Vector2 upDir = Vector2.up;
        Vector2 downDir = Vector2.down;
        Vector2 leftDir = Vector2.left;
        Vector2 rightDir = Vector2.right;

        Vector2 myPos = transform.position;

        float myX = myPos.x;
        float myY = myPos.y;
        float otherX = pos.x;
        float otherY = pos.y;

        float dist = Vector2.Distance(myPos, pos);
        float xDist = myX - otherX;
        float yDist = myY - otherY;


        if(Mathf.Approximately(myX, otherX)) {
            if(yDist < 0.0f) {
                return LegalMove(Physics2D.Raycast(myPos, upDir, dist, 1), pos);
            } else if(yDist > 0.0f) {
                return LegalMove(Physics2D.Raycast(myPos, downDir, dist, 1), pos);
            }
        } else if(Mathf.Approximately(myY, otherY)) {
            if(xDist < 0.0f) {
                return LegalMove(Physics2D.Raycast(myPos, rightDir, dist, 1), pos);
            } else if(xDist > 0.0f) {
                return LegalMove(Physics2D.Raycast(myPos, leftDir, dist, 1), pos);
            }
        }

        return false;
    }

    bool LegalMove(RaycastHit2D ray, Vector2 pos) {
        if(ray.collider == null) {
            return true;
        } else {
            if((Vector2)ray.collider.transform.position == pos) {
                return true;
            }
        }

        return false;
    }
}