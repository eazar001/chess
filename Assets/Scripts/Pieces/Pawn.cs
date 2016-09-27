using UnityEngine;
using System.Collections;


/// <summary>
/// The pawn piece.
/// </summary>
public class Pawn: Piece {

    bool firstMove = true;
    public bool enpassantAvailable = false;

    public override bool ValidMove(Vector2 pos) {
        Vector2 dir = Vector2.up;
        Vector2 d1 = new Vector2(xMove, yMove);
        Vector2 d2 = new Vector2(-xMove, yMove);
        Vector2 y = y1;
        Vector2 myPos = transform.position;


        if(GetAffiliation() == Player.Black) {
            dir = -dir;
            d1 = -d1;
            d2 = -d2;
            y = -y;
        }


        if(firstMove && pos == myPos + 2*y) {
            RaycastHit2D up = Physics2D.Raycast(myPos, dir, 2*yMove, 1);

            if(up.collider == null) {
                firstMove = false;
                enpassantAvailable = true;
                return true;
            }
        }

        if((pos == myPos + y)) {
            RaycastHit2D up = Physics2D.Raycast(myPos, dir, yMove, 1);

            if(up.collider == null) {
                firstMove = false;
                return true;
            }
        } else if((pos == myPos + d1)) {
            float distance = Mathf.Sqrt(Mathf.Pow(xMove, 2) + Mathf.Pow(yMove, 2));
            RaycastHit2D diagRight = Physics2D.Raycast(myPos, d1, distance, 1);

            if(diagRight.collider != null) {
                firstMove = false;
                return true;
            } else {
                Vector2 eDir;
                Piece.Player mySide = GetAffiliation();

                if(mySide == Player.Black) {
                    eDir = Vector2.left;
                } else {
                    eDir = Vector2.right;
                }

                RaycastHit2D right = Physics2D.Raycast(myPos, eDir, xMove, 1);

                if(right.collider != null) {
                    GameObject otherRight = right.collider.gameObject;
                    Piece otherPieceRight = otherRight.GetComponent<Piece>();
                    Piece.Player rightSide = otherPieceRight.GetAffiliation();

                    if(otherRight.CompareTag("Pawn") && mySide != rightSide) {
                        if(otherRight.GetComponent<Pawn>().EnpassantAvailable()) {
                            firstMove = false;
                            otherPieceRight.Remove();
                            return true;
                        }
                    }
                }
            }
        } else if((pos == myPos + d2)) {
            float distance = Mathf.Sqrt(Mathf.Pow(xMove, 2) + Mathf.Pow(yMove, 2));
            RaycastHit2D diagLeft = Physics2D.Raycast(myPos, d2, distance, 1);

            if(diagLeft.collider != null) {
                firstMove = false;
                return true;
            } else {
                Vector2 eDir;
                Piece.Player mySide = GetAffiliation();
                if(mySide == Player.Black) {
                    eDir = Vector2.right;
                } else {
                    eDir = Vector2.left;
                }

                RaycastHit2D left = Physics2D.Raycast(myPos, eDir, xMove, 1);

                if(left.collider != null) {
                    GameObject otherLeft = left.collider.gameObject;
                    Piece otherPieceLeft = otherLeft.GetComponent<Piece>();
                    Piece.Player leftSide = otherPieceLeft.GetAffiliation();
                    
                    if(otherLeft.CompareTag("Pawn") && mySide != leftSide) {
                        if(otherLeft.GetComponent<Pawn>().EnpassantAvailable()) {
                            firstMove = false;
                            otherPieceLeft.Remove();
                            return true;
                        }
                    }   
                }
            }
        }

        return false;
    }

    // This indicates that the last move the pawn made was En Passant. This variable will be reset
    // on the next move.
    protected bool EnpassantAvailable() {
        return enpassantAvailable;
    }
}