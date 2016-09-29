﻿using UnityEngine;
using System.Collections;


/// <summary>
/// The pawn piece.
/// </summary>
public class Pawn: Piece {

    bool firstMove = true;
    public bool enpassantAvailable = false;
    Vector2 diag1, diag2;

    public override bool ValidMove(Vector2 pos) {
        Vector2 dir = Vector2.up;
        Vector2 y = y1;
        Vector2 myPos = transform.position;
        diag1 = d1;
        diag2 = d2;


        if(GetAffiliation() == Player.Black) {
            dir = -dir;
            diag1 = -diag1;
            diag2 = -diag2;
            y = -y;
        }


        if(firstMove && pos == myPos + 2*y) {
            return LegalMove(Physics2D.Raycast(myPos, dir, 2*yMove, 1), 2);
        }

        if(pos == myPos + y) {
            return LegalMove(Physics2D.Raycast(myPos, dir, yMove, 1), 1);
        } else if(pos == myPos + diag1) {
            float distance = Mathf.Sqrt(Mathf.Pow(xMove, 2) + Mathf.Pow(yMove, 2));
            return LegalDiagonal(Physics2D.Raycast(myPos, diag1, distance, 1), diag1, myPos, pos);
        } else if(pos == myPos + diag2) {
            float distance = Mathf.Sqrt(Mathf.Pow(xMove, 2) + Mathf.Pow(yMove, 2));
            return LegalDiagonal(Physics2D.Raycast(myPos, diag2, distance, 1), diag2, myPos, pos);
        }

        return false;
    }

    // This indicates that the last move the pawn made was En Passant. This variable will be reset
    // on the next move.
    protected bool EnpassantAvailable() {
        return enpassantAvailable;
    }

    bool LegalMove(RaycastHit2D ray, uint squaresUp) {
        if(ray.collider == null) {
            firstMove = false;
            if(squaresUp > 1) {
                enpassantAvailable = true;
            } else {
                enpassantAvailable = false;
            }

            return true;
        }

        return false;
    }

    bool LegalDiagonal(RaycastHit2D ray, Vector2 dir, Vector2 myPos, Vector2 pos) {
        if(ray.collider != null) {
            firstMove = false;
            return true;
        } else {
            Vector2 eDir;
            Piece.Player mySide = GetAffiliation();

            if(pos == myPos + diag1) {
                if(mySide == Player.Black) {
                    eDir = Vector2.left;
                } else {
                    eDir = Vector2.right;
                }
            } else if(pos == myPos + diag2) {
                if(mySide == Player.Black) {
                    eDir = Vector2.right;
                } else {
                    eDir = Vector2.left;
                }
            } else {
                return false;
            }

            RaycastHit2D hit = Physics2D.Raycast(myPos, eDir, xMove, 1);

            if(hit.collider != null) {
                GameObject other = hit.collider.gameObject;
                Piece otherPiece = other.GetComponent<Piece>();
                Piece.Player otherSide = otherPiece.GetAffiliation();

                if(otherPiece.CompareTag("Pawn") && mySide != otherSide) {
                    if(otherPiece.GetComponent<Pawn>().EnpassantAvailable()) {
                        firstMove = false;
                        otherPiece.Remove();
                        return true;
                    }
                }
            }
        }

        return false;
    }
}