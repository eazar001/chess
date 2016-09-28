using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base class for all Piece types. Each piece subclass should define its own implementation of
/// the `ValidMove` method. The Square class will be responsible for validating capture moves,
/// animations, and rudimentary boundary checking. `ValidMove` should validate the core move pattern
/// for its respective piece.
/// </summary>
public abstract class Piece: MonoBehaviour {

    public float xMove { get; private set; }
    public float yMove { get; private set; }

    // internal move variables
    public Vector2 x1 { get; private set; }
    public Vector2 x2 { get; private set; }
    public Vector2 y1 { get; private set; }
    public Vector2 y2 { get; private set; }

    // special move variables for the knight
    public Vector2 k1 { get; private set; }
    public Vector2 k2 { get; private set; }
    public Vector2 k3 { get; private set; }
    public Vector2 k4 { get; private set; }
    public Vector2 k5 { get; private set; }
    public Vector2 k6 { get; private set; }
    public Vector2 k7 { get; private set; }
    public Vector2 k8 { get; private set; }

    public Affiliation affiliation;

    public enum Player {
        White,
        Black,
    }

    [System.Serializable]
    public class Affiliation {
        public Player side;
    }

    void Start() {
        xMove = BoardManager.xMove;
        yMove = BoardManager.yMove;

        x1 = new Vector2(xMove, 0);
        y1 = new Vector2(0, yMove);

        k1 = new Vector2(xMove, 2.0f*yMove);
        k2 = new Vector2(-xMove, 2.0f*yMove);
        k3 = new Vector2(xMove, -2.0f*yMove);
        k4 = new Vector2(-xMove, -2.0f*yMove);
        k5 = new Vector2(2.0f*xMove, yMove);
        k6 = new Vector2(-2.0f*xMove, yMove);
        k7 = new Vector2(2.0f*xMove, -yMove);
        k8 = new Vector2(-2.0f*xMove, -yMove);
    }

    public abstract bool ValidMove(Vector2 pos);

    // This is the main method for executing moves requested directly by the user, i.e. non
    // special moves (like castling) that are requested by selecting source and destination and
    // awaiting validation. Moves should generally always be initiated with this method.
    public void PlaceAt(Vector2 pos) {
        Vector3 newPos = pos;
        newPos += Vector3.forward;

        BoardManager.srcPiece.transform.position = newPos;
        BoardManager.srcSquare.myPiece = null;

        BoardManager.srcSquare.anim.SetTrigger("Click");
        BoardManager.srcSquare.selected = false;
        BoardManager.srcSquare = null;
        BoardManager.squareSelected = false;
    }

    // This method is for directly moving a piece. Useful for situations like castling.
    public void MoveTo(Vector2 pos) {
        Vector3 newPos = pos;
        newPos += Vector3.forward;
        transform.position = newPos;
    }

    public void PickUp() {
        gameObject.SetActive(false);
    }

    public void Remove() {
        PickUp();
        Destroy(gameObject);
    }

    public Player GetAffiliation() {
        return affiliation.side;
    }
}