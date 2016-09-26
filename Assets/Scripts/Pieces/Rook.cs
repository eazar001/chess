using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The rook piece.
/// </summary>
public class Rook: Piece {

    public override bool ValidMove(Vector2 pos) {
        Vector2 upDir = Vector2.up;
        Vector2 downDir = Vector2.down;
        Vector2 leftDir = Vector2.left;
        Vector2 rightDir = Vector2.right;

        Vector2 d1 = new Vector2(xMove, yMove);
        Vector2 d2 = new Vector2(-xMove, yMove);
        Vector2 myPos = transform.position;

        float myX = myPos.x;
        float myY = myPos.y;
        float otherX = pos.x;
        float otherY = pos.y;

        float dist = Vector2.Distance(myPos, pos);
        float xDist = myX - otherX;
        float yDist = myY - otherY;


        if(myX == otherX) {
            if(yDist < 0.0f) {
                RaycastHit2D up = Physics2D.Raycast(myPos, upDir, dist, 1);

                if(up.collider == null) {
                    return true;
                } else {
                    if((Vector2)up.collider.transform.position == pos) {
                        return true;
                    }
                }
            } else if(yDist > 0.0f) {
                RaycastHit2D down = Physics2D.Raycast(myPos, downDir, dist, 1);

                if(down.collider == null) {
                    return true;
                } else {
                    if((Vector2)down.collider.transform.position == pos) {
                        return true;
                    }
                }
            }
        } else if(myY == otherY) {
            if(xDist < 0.0f) {
                RaycastHit2D right = Physics2D.Raycast(myPos, rightDir, dist, 1);

                if(right.collider == null) {
                    return true;
                } else {
                    if((Vector2)right.collider.transform.position == pos) {
                        return true;
                    }
                }
            } else if(xDist > 0.0f) {
                RaycastHit2D left = Physics2D.Raycast(myPos, leftDir, dist, 1);

                if(left.collider == null) {
                    return true;
                } else {
                    if((Vector2)left.collider.transform.position == pos) {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}