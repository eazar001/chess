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
                    }
                } else {
                    mySide = myPiece.GetAffiliation();

                    if(mySide != srcSide && srcPiece.ValidMove(transform.position)) {
                        myPiece.Remove();
                        myPiece = null;
                        srcPiece.PlaceAt(transform.position);
                        srcPiece = null;
                    }
                }
            }

                
        } else {
            if(myPiece != null) {
                selected = true;
                BoardManager.squareSelected = true;
                BoardManager.srcPiece = myPiece;
                BoardManager.srcSquare = gameObject.GetComponent<Square>();
                anim.SetTrigger("Click");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        GameObject obj = otherCollider.gameObject;
        myPiece = obj.GetComponent<Piece>();
    }
}