using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The king piece.
/// </summary>
public class King: Piece {

    bool firstMove = true;
    GameManager.PlayerSide mySide;
    bool inCheck = false;
    Vector2 upDir = Vector2.up;
    Vector2 downDir = Vector2.down;
    Vector2 leftDir = Vector2.left;
    Vector2 rightDir = Vector2.right;

    public override bool ValidMove(Vector2 pos) {
        mySide = GetAffiliation();
        Vector2 myPos = transform.position;

        // Castling moves
        if(firstMove) {
            if(pos == myPos + 2.0f*x1) {
                // King side
                return LegalCastle(Vector2.right, myPos, pos);
            } else if(pos == myPos - 2.0f*x1) {
                // Queen side
                return LegalCastle(Vector2.left, myPos, pos);
            }
        }

        // Normal moves
        if((pos == myPos + y1) || pos == myPos + d2 || pos == myPos - y1 || pos == myPos - d1
            || pos == myPos - d2 || pos == myPos + d1 || pos == myPos + x1 || pos == myPos - x1) {

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
                if(!otherObj.CompareTag("Rook") || otherPos != pos + x1) {
                    return false;
                }
            } else {
                if(!otherObj.CompareTag("Rook") || otherPos != pos - 2.0f*x1) {
                    return false;
                }
            }
                    
            GameManager.PlayerSide otherSide = otherObj.GetComponent<Piece>().GetAffiliation();
            Rook rook = otherObj.GetComponent<Rook>();

            if(direction == Vector2.right) {
                if(mySide == otherSide && rook.firstMove) {
                    Vector2 rookPos = otherPos - x1;
                    rookPos -= x1;
                    rook.firstMove = false;
                    rook.MoveTo(rookPos);
                    return true;
                }
            } else {
                if(mySide == otherSide && rook.firstMove) {
                    Vector2 rookPos = otherPos + x1;
                    rookPos += x1;
                    rookPos += x1;
                    rook.firstMove = false;
                    rook.MoveTo(rookPos);
                    return true;
                }
            }
        }

        return false;
    }

    public bool InCheck() {
        Vector2 myPos = transform.position;

        RaycastHit2D downCast = Physics2D.Raycast(myPos, downDir, 7*yMove, 1);
        RaycastHit2D upCast = Physics2D.Raycast(myPos, upDir, 7*yMove, 1);
        RaycastHit2D leftCast = Physics2D.Raycast(myPos, leftDir, 7*xMove, 1);
        RaycastHit2D rightCast = Physics2D.Raycast(myPos, rightDir, 7*xMove, 1);
        RaycastHit2D diagRightCast = Physics2D.Raycast(myPos, d1, 7*xMove, 1);
        RaycastHit2D diagLeftCast = Physics2D.Raycast(myPos, d2, 7*xMove, 1);
        RaycastHit2D diagRightDownCast = Physics2D.Raycast(myPos, -d1, 7*xMove, 1);
        RaycastHit2D diagLeftDownCast = Physics2D.Raycast(myPos, -d2, 7*xMove, 1);

        if(RegularEnemyCollision(downCast)) {
            inCheck = true;
            return inCheck;
        } else if(RegularEnemyCollision(upCast)) {
            inCheck = true;
            return inCheck;
        } else if(RegularEnemyCollision(leftCast)) {
            inCheck = true;
            return inCheck;
        } else if(RegularEnemyCollision(rightCast)) {
            inCheck = true;
            return inCheck;
        } else if(DiagonalEnemyCollision(diagRightCast) || DiagonalEnemyCollision(diagLeftCast)
            || DiagonalEnemyCollision(diagRightDownCast)
            || DiagonalEnemyCollision(diagLeftDownCast)) {

            inCheck = true;
            return inCheck;
        } else {
            inCheck = false;
            return inCheck;
        }

    }

    public bool SetCheck() {
        inCheck = !inCheck;
        return inCheck;
    }

    // Check condition passes if colliding with rook or queen enemy collider
    bool RegularEnemyCollision(RaycastHit2D r) {
        Collider2D c = r.collider;
        GameManager.PlayerSide otherSide = c.gameObject.GetComponent<Piece>().GetAffiliation();

        if(c != null && otherSide != mySide) {
            GameObject obj = c.gameObject;

            if(obj.CompareTag("Rook(Clone)") || obj.CompareTag("Queen(Clone)")) {
                return true;
            }
        }

        return false;
    }

    // Check condition passes if colliding with pawn, bishop, or queen enemy collider
    bool DiagonalEnemyCollision(RaycastHit2D r) {
        Collider2D c = r.collider;
        GameManager.PlayerSide otherSide = c.gameObject.GetComponent<Piece>().GetAffiliation();

        if(c != null && otherSide != mySide) {
            GameObject obj = c.gameObject;

            if(obj.CompareTag("Queen(Clone)") || obj.CompareTag("Bishop(Clone)")) {

                return true;
            }
        }

        return false;
    }
}