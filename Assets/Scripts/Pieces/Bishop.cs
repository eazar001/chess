using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The bishop piece.
/// </summary>
public class Bishop: Piece {

    public override bool ValidMove(Vector2 pos) {
        Vector2 myPos = transform.position;

        float myX = myPos.x;
        float myY = myPos.y;
        float otherX = pos.x;
        float otherY = pos.y;

        float dist = Vector2.Distance(myPos, pos);
        float xDist = myX - otherX;
        float yDist = myY - otherY;

        if(Mathf.Abs(Mathf.Abs(xDist) - Mathf.Abs(yDist)) >= xMove/2.0f) {
            return false;
        }

        if(yDist < 0.0f) {
            if(xDist < 0.0f) {
                return LegalMove(Physics2D.Raycast(myPos, d1, dist, 1), pos);
            } else if(xDist > 0.0f) {
                return LegalMove(Physics2D.Raycast(myPos, d2, dist, 1), pos);
            }
        } else if(yDist > 0.0f) {
            if(-xDist < 0.0f) {
                return LegalMove(Physics2D.Raycast(myPos, -d1, dist, 1), pos);
            } else if(-xDist > 0.0f) {
                return LegalMove(Physics2D.Raycast(myPos, -d2, dist, 1), pos);
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