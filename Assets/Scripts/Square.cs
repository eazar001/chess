using UnityEngine;
using System.Collections;

public class Square: MonoBehaviour {
	
    public Animator anim { get; private set; }
    public bool selected { get; set; }
    public Piece myPiece { get; set; }

    void Start() {
        anim = GetComponent<Animator>();
        selected = false;
    }

    void OnMouseDown() {
        if(BoardManager.squareSelected) {
            if(selected) {
                anim.SetTrigger("Click");
                selected = false;
                BoardManager.squareSelected = false;
            } else {
                Piece.Player mySide, srcSide;
                Piece srcPiece = BoardManager.srcPiece;

                srcSide = srcPiece.GetAffiliation();

                if(myPiece == null) {
                    if(srcPiece.ValidMove(transform.position)) {
                        srcPiece.PlaceAt(transform.position);
                        CompleteMove();
                    }
                } else {
                    mySide = myPiece.GetAffiliation();

                    if(mySide != srcSide && srcPiece.ValidMove(transform.position)) {
                        myPiece.Remove();
                        myPiece = null;
                        srcPiece.PlaceAt(transform.position);
                        srcPiece = null;
                        CompleteMove();
                    }
                }
            }

                
        } else {
            if(myPiece != null) {
                Piece.Player mySide = myPiece.GetAffiliation();

                if(mySide == GameManager.turn) {
                    selected = true;
                    BoardManager.squareSelected = true;
                    BoardManager.srcPiece = myPiece;
                    BoardManager.srcSquare = gameObject.GetComponent<Square>();
                    anim.SetTrigger("Click");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        GameObject obj = otherCollider.gameObject;
        myPiece = obj.GetComponent<Piece>();
    }

    void OnTriggerExit2D(Collider2D otherCollider) {
        myPiece = null;
    }

    // routines to run on successful execution of a move
    void CompleteMove() {
        GameObject[] allObjs = FindObjectsOfType<GameObject>();
        string pawnName;

        if(GameManager.turn == Piece.Player.White) {
            pawnName = "BlackPawn(Clone)";
        } else {
            pawnName = "WhitePawn(Clone)";
        }

        foreach(GameObject obj in allObjs) {
            if(obj.name == pawnName) {
                obj.GetComponent<Pawn>().enpassantAvailable = false;
            }

        }

        GameManager.NextTurn();
    }
}