using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// The king piece.
/// </summary>
public class King: Piece {

    bool firstMove = true;

    public bool FirstMove {
        get { return firstMove; }
        set { firstMove = value; }
    }

    GameManager.PlayerSide mySide;
    bool inCheck = false;
    bool inCheckMate = false;

    public bool InCheck {
        get { return inCheck; }
    }

    public bool InCheckMate {
        get { return inCheckMate; }
    }

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
                if(mySide == otherSide && rook.FirstMove) {
                    Vector2 rookPos = otherPos - x1;
                    rookPos -= x1;
                    rook.FirstMove = false;
                    rook.MoveTo(rookPos);
                    return true;
                }
            } else {
                if(mySide == otherSide && rook.FirstMove) {
                    Vector2 rookPos = otherPos + x1;
                    rookPos += x1;
                    rookPos += x1;
                    rook.FirstMove = false;
                    rook.MoveTo(rookPos);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Determine whether or not the piece is currently in check and mark the corresponding property
    /// for the King piece.
    /// </summary>
    public void EvalCheck() {
        mySide = GetAffiliation();
        Vector2 myPos = transform.position;

        RaycastHit2D downCast = Physics2D.Raycast(myPos, downDir, 7*yMove, 1);
        RaycastHit2D upCast = Physics2D.Raycast(myPos, upDir, 7*yMove, 1);
        RaycastHit2D leftCast = Physics2D.Raycast(myPos, leftDir, 7*xMove, 1);
        RaycastHit2D rightCast = Physics2D.Raycast(myPos, rightDir, 7*xMove, 1);
        RaycastHit2D diagRightCast = Physics2D.Raycast(myPos, d1, 7*xMove, 1);
        RaycastHit2D diagLeftCast = Physics2D.Raycast(myPos, d2, 7*xMove, 1);
        RaycastHit2D diagRightDownCast = Physics2D.Raycast(myPos, -d1, 7*xMove, 1);
        RaycastHit2D diagLeftDownCast = Physics2D.Raycast(myPos, -d2, 7*xMove, 1);

        if(RegularEnemyCollision(downCast)
            || RegularEnemyCollision(upCast)
            || RegularEnemyCollision(leftCast)
            || RegularEnemyCollision(rightCast)) {

            inCheck = true;
        } else if(DiagonalEnemyCollision(diagRightCast)
            || DiagonalEnemyCollision(diagLeftCast)
            || DiagonalEnemyCollision(diagRightDownCast)
            || DiagonalEnemyCollision(diagLeftDownCast)) {

            inCheck = true;
        } else {
            // because knights are so special
            IEnumerable<Knight> knights = from knight in FindObjectsOfType<Knight>()
                                          where knight.GetAffiliation() != mySide &&
                                                knight.ValidMove(transform.position)
                                          select knight;

            if(knights.Any()) {
                inCheck = true;
            } else {
                inCheck = false;
            }
        }
    }

    /// <summary>
    /// Determine whether or not the player's king piece is currently in checkmate and mark the
    /// corresponding property as such.
    /// </summary>
    public void EvalCheckMate() {
        // We obviously can't be in checkmate if we're not even in check
        if(!inCheck) {
            inCheckMate = false;
            return;
        }

        IEnumerable<Piece> myPieces = from piece in FindObjectsOfType<Piece>()
                                      where mySide == piece.GetAffiliation()
                                      select piece;

        // Determine whether any of player's pieces can break the check here
        foreach(Piece piece in myPieces) {
            if(CanBreakCheck(piece)) {
                inCheckMate = false;
                return;
            }
        }

        inCheckMate = true;
    }

    bool CanBreakCheck(Piece piece) {
        return TryMoves(piece);
    }

    bool TryMoves(Piece piece) {
        Square[] allSquares = FindObjectsOfType<Square>();
        Vector2 oldPos = piece.transform.position;
        Vector2 newPos;

        foreach(Square square in allSquares) {
            newPos = square.transform.position;

            if(piece.ValidMove(newPos)
                && (square.myPiece == null || square.myPiece.GetAffiliation() != mySide)) {

                piece.MoveTo(newPos);
                EvalCheck();

                if(!inCheck) {
                    // We must preserve the check state
                    inCheck = true;
                    piece.MoveTo(oldPos);
                    return true;
                }

                piece.MoveTo(oldPos);
            }
        }

        return false;
    }

    // Check condition passes if colliding with rook or queen enemy collider
    bool RegularEnemyCollision(RaycastHit2D r) {
        Collider2D c = r.collider;
        if(c == null) { return false; }

        GameManager.PlayerSide otherSide = c.gameObject.GetComponent<Piece>().GetAffiliation();

        if(otherSide != mySide) {
            GameObject obj = c.gameObject;

            if(obj.CompareTag("Rook") || obj.CompareTag("Queen")) {
                return true;
            }
        }

        return false;
    }

    // Check condition passes if colliding with pawn, bishop, or queen enemy collider
    bool DiagonalEnemyCollision(RaycastHit2D r) {
        Collider2D c = r.collider;
        if(c == null) { return false; }

        GameManager.PlayerSide otherSide = c.gameObject.GetComponent<Piece>().GetAffiliation();

        if(otherSide != mySide) {
            GameObject obj = c.gameObject;

            if(obj.CompareTag("Queen") || obj.CompareTag("Bishop")) {
                return true;
            }

            if(obj.CompareTag("Pawn") && obj.GetComponent<Pawn>().ValidMove(transform.position)) {
                return true;
            }
        }

        return false;
    }
}