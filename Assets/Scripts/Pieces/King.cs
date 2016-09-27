using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The king piece.
/// </summary>
public class King: Piece {

    bool firstMove = true;

    public override bool ValidMove(Vector2 pos) {
        Piece.Player mySide = GetAffiliation();
        Vector2 d1 = new Vector2(xMove, yMove);
        Vector2 d2 = new Vector2(-xMove, yMove);
        Vector2 d3 = new Vector2(xMove, 0);

        Vector2 myPos = transform.position;

        // Castling moves
        if(firstMove) {
            if(pos == myPos + 2.0f*d3) {
                RaycastHit2D ray = Physics2D.Raycast(myPos, Vector2.right, 3.0f*xMove, 1);

                if(ray.collider != null) {
                    GameObject otherObj = ray.collider.gameObject;
                    Vector2 otherPos = otherObj.transform.position;

                    if(!otherObj.CompareTag("Rook") || otherPos != pos + d3) {
                        return false;
                    }
                    
                    Piece.Player otherSide = otherObj.GetComponent<Piece>().GetAffiliation();
                    Rook rook = otherObj.GetComponent<Rook>();

                    if(mySide == otherSide && rook.firstMove) {
                        Vector2 rookPos = otherPos - d3;
                        rookPos -= d3;
                        rook.firstMove = false;
                        rook.MoveTo(rookPos);
                        return true;
                    }
                }
            } else if(pos == myPos - 2.0f*d3) {
                RaycastHit2D ray = Physics2D.Raycast(myPos, Vector2.left, 4.0f*xMove, 1);

                if(ray.collider != null) {
                    GameObject otherObj = ray.collider.gameObject;
                    Vector2 otherPos = otherObj.transform.position;

                    if(!otherObj.CompareTag("Rook") || otherPos != pos - 2.0f*d3) {
                        return false;
                    }
                    
                    Piece.Player otherSide = otherObj.GetComponent<Piece>().GetAffiliation();
                    Rook rook = otherObj.GetComponent<Rook>();

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
        }

        // Normal moves
        if((pos == myPos + y1) || pos == myPos + d2 || pos == myPos - y1 || pos == myPos - d1
            || pos == myPos - d2 || pos == myPos + d1 || pos == myPos + d3 || pos == myPos - d3) {

            firstMove = false;
            return true;
        }

        return false;
    }
}