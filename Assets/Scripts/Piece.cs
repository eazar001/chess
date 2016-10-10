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

    protected float xMove { get; private set; }
    protected float yMove { get; private set; }

    // internal move variables
    protected Vector2 x1 { get; private set; }
    protected Vector2 y1 { get; private set; }
    protected Vector2 d1 { get; private set; }
    protected Vector2 d2 { get; private set; }

    // special move variables for the knight
    protected Vector2 k1 { get; private set; }
    protected Vector2 k2 { get; private set; }
    protected Vector2 k3 { get; private set; }
    protected Vector2 k4 { get; private set; }
    protected Vector2 k5 { get; private set; }
    protected Vector2 k6 { get; private set; }
    protected Vector2 k7 { get; private set; }
    protected Vector2 k8 { get; private set; }

    [SerializeField]
    Affiliation affiliation;

    [System.Serializable]
    struct Affiliation {
        public GameManager.PlayerSide side;
    }

    void Start() {
        xMove = BoardManager.xMove;
        yMove = BoardManager.yMove;

        x1 = new Vector2(xMove, 0);
        y1 = new Vector2(0, yMove);
        d1 = new Vector2(xMove, yMove);
        d2 = new Vector2(-xMove, yMove);

        k1 = new Vector2(xMove, 2.0f*yMove);
        k2 = new Vector2(-xMove, 2.0f*yMove);
        k3 = new Vector2(xMove, -2.0f*yMove);
        k4 = new Vector2(-xMove, -2.0f*yMove);
        k5 = new Vector2(2.0f*xMove, yMove);
        k6 = new Vector2(-2.0f*xMove, yMove);
        k7 = new Vector2(2.0f*xMove, -yMove);
        k8 = new Vector2(-2.0f*xMove, -yMove);
    }

    /// <summary>
    /// Verify that a move is within the ruleset of the piece's implementation.
    /// </summary>
    /// <param name="pos">The hypothetical position to move the piece to.</param>
    /// <returns>Returns a bool indicating whether or not pos is within moveset.</returns>
    public abstract bool ValidMove(Vector2 pos);

    /// <summary>
    /// This is the main method for executing moves requested directly by the user, i.e. non
    /// special moves (like castling) that are requested by selecting source and destination and
    /// awaiting validation. Moves should generally always be initiated with this method.
    /// </summary>
    /// <param name="pos">The position to place the piece at.</param>
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

    /// <summary>
    /// This method is for directly moving a piece. Useful for situations like castling and testing
    /// moves before finalizing them. Using this method entails none of the default bookeeping that
    /// is associated with the `PlaceAt` method.
    /// </summary>
    /// <param name="pos">The position to directly move the piece to.</param>
    public void MoveTo(Vector2 pos) {
        Vector3 newPos = pos;
        newPos += Vector3.forward;
        transform.position = newPos;
    }

    /// <summary>
    /// Deactivate the piece without destroying the gameobject it's associated with.
    /// </summary>
    public void PickUp() {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Destroy and permanently remove this piece instance from the game.
    /// </summary>
    public void Remove() {
        PickUp();
        Destroy(gameObject);
    }

    /// <summary>
    /// Gets the affiliation of the current piece.
    /// </summary>
    /// <returns>Returns which side the piece is affiliated with (black, white).</returns>
    public GameManager.PlayerSide GetAffiliation() {
        return affiliation.side;
    }
}