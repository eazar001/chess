using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The queen piece.
/// </summary>
public class Queen: Piece {

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
                if(LegalMove(Physics2D.Raycast(myPos, upDir, dist, 1), pos)) {
                    return true;
                }
            } else if(yDist > 0.0f) {
                if(LegalMove(Physics2D.Raycast(myPos, downDir, dist, 1), pos)) {
                    return true;
                }
            }
        } else if(myY == otherY) {
            if(xDist < 0.0f) {
                if(LegalMove(Physics2D.Raycast(myPos, rightDir, dist, 1), pos)) {
                    return true;
                }
            } else if(xDist > 0.0f) {
                if(LegalMove(Physics2D.Raycast(myPos, leftDir, dist, 1), pos)) {
                    return true;
                }
            }
        } else {
            if(yDist < 0.0f) {
                if(xDist < 0.0f) {
                    if(LegalMove(Physics2D.Raycast(myPos, d1, dist, 1), pos)) {
                        return true;
                    }
                } else if(xDist > 0.0f) {
                    if(LegalMove(Physics2D.Raycast(myPos, d2, dist, 1), pos)) {
                        return true;
                    }
                }
            } else if(yDist > 0.0f) {
                if(-xDist < 0.0f) {
                    if(LegalMove(Physics2D.Raycast(myPos, -d1, dist, 1), pos)) {
                        return true;
                    }
                } else if(-xDist > 0.0f) {
                    if(LegalMove(Physics2D.Raycast(myPos, -d2, dist, 1), pos)) {
                        return true;
                    }
                }
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
