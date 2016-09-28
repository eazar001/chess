using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The king piece.
/// </summary>
public class King: Piece {

    bool firstMove = true;
    Vector2 d1, d2, d3;

    Piece.Player mySide;

    public override bool ValidMove(Vector2 pos) {
        mySide = GetAffiliation();
        d1 = new Vector2(xMove, yMove);
        d2 = new Vector2(-xMove, yMove);
        d3 = new Vector2(xMove, 0);

        Vector2 myPos = transform.position;

        // Castling moves
        if(firstMove) {
            if(pos == myPos + 2.0f*d3) {
                // King side
                return LegalCastle(Vector2.right, myPos, pos);
            } else if(pos == myPos - 2.0f*d3) {
                // Queen side
                return LegalCastle(Vector2.left, myPos, pos);
            }
        }

        // Normal moves
        if((pos == myPos + y1) || pos == myPos + d2 || pos == myPos - y1 || pos == myPos - d1
            || pos == myPos - d2 || pos == myPos + d1 || pos == myPos + d3 || pos == myPos - d3) {

            firstMove = false;
            return true;
        }

        return false;
    }

    bool LegalCastle(Vector2 direction, Vector2 myPos, Vector2 pos) {
        RaycastHit2D ray;

        if(direction == Vector2.right) {
            ray = Physics2D.Raycast(myPos, Vector2.right, 3.0f*xMove, 1);
        } else {
            ray = Physics2D.Raycast(myPos, Vector2.left, 4.0f*xMove, 1);
        }

        if(ray.collider != null) {
            GameObject otherObj = ray.collider.gameObject;
            Vector2 otherPos = otherObj.transform.position;

            if(direction == Vector2.right) {
                if(!otherObj.CompareTag("Rook") || otherPos != pos + d3) {
                    return false;
                }
            } else {
                if(!otherObj.CompareTag("Rook") || otherPos != pos - 2.0f*d3) {
                    return false;
                }
            }
                    
            Piece.Player otherSide = otherObj.GetComponent<Piece>().GetAffiliation();
            Rook rook = otherObj.GetComponent<Rook>();

            if(direction == Vector2.right) {
                if(mySide == otherSide && rook.firstMove) {
                    Vector2 rookPos = otherPos - d3;
                    rookPos -= d3;
                    rook.firstMove = false;
                    rook.MoveTo(rookPos);
                    return true;
                }
            } else {
                if(mySide == otherSide && rook.firstMove) {
                    Vector2 rookPos = otherPos + d3;
                    rookPos += d3;
                    rookPos += d3;
                    rook.firstMove = false;
                    rook.MoveTo(rookPos);
                    return true;
                }
            }
        }

        return false;
    }
}