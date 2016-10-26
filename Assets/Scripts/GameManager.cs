using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Management of all variables and operations necessary to keep the game running consistently and
/// correctly.
/// </summary>
public class GameManager: MonoBehaviour {

    /// <summary>
    /// Whose turn is it?
    /// </summary>
    public static PlayerSide turn { get; private set; }
    public static bool promoting { get; set; }

    static Player white;
    static Player black;

    [SerializeField]
    PieceMovement movementProperties = new PieceMovement(0.68f, 0.70f);

    BoardManager b;

    [System.Serializable]
    public struct PieceMovement {
        public float xMove;
        public float yMove;

        public PieceMovement(float x, float y) {
            xMove = x;
            yMove = y;
        }
    }

    /// <summary>
    /// The player struct type.
    /// </summary>
    struct Player {

        /// <summary>
        /// Which side is it? (white, black)
        /// </summary>
        public PlayerSide side;


        /// <summary>
        /// What's the player's state? (normal, check, checkmate)
        /// </summary>
        public PlayerState state;

        public Player(PlayerSide side, PlayerState state) {
            this.side = side;
            this.state = state;
        }
    }

    /// <summary>
    /// What side is the player on?
    /// </summary>
    public enum PlayerSide {
        White,
        Black,
    }

    /// <summary>
    /// What is the current state of the player?
    /// </summary>
    public enum PlayerState {
        Normal,
        Check,
        Checkmate,
    }

    void Start() {
        IEnumerable<Square> squares = FindObjectsOfType<Square>();
        IEnumerable<Piece> pieces = FindObjectsOfType<Piece>();

        white = new Player(PlayerSide.White, PlayerState.Normal);
        black = new Player(PlayerSide.Black, PlayerState.Normal);
        turn = white.side;

        if(squares.Any()) {
            float xMove = movementProperties.xMove;
            float yMove = movementProperties.yMove;

            b = new BoardManager(xMove, yMove);

            if(pieces.Any()) {
                foreach(Piece piece in pieces) {
                    piece.InitMoveSet();
                }
            }
        } else {
            float xMove = movementProperties.xMove;
            float yMove = movementProperties.yMove;

            b = new BoardManager(xMove, yMove);
            b.CreateBoard();
        }
    }

    void Update() {
        if(Input.GetKeyDown("escape")) {
            Application.Quit();
        }

        if(promoting) {
            if(Input.GetKeyDown("q")) {
                Square.PromoteAnyPawns("Queen");
                promoting = false;
            } else if(Input.GetKeyDown("r")) {
                Square.PromoteAnyPawns("Rook");
                promoting = false;
            } else if(Input.GetKeyDown("b")) {
                Square.PromoteAnyPawns("Bishop");
                promoting = false;
            } else if(Input.GetKeyDown("k")) {
                Square.PromoteAnyPawns("Knight");
                promoting = false;
            }
        }
    }

    /// <summary>
    /// Switch to the next player's turn.
    /// </summary>
    public static void NextTurn() {
        if(turn == PlayerSide.Black) {
            turn = PlayerSide.White;
        } else {
            turn = PlayerSide.Black;
        }
    }

    /// <summary>
    /// Evaluate the state of the player with initiative and update the respective properties for
    /// that player.
    /// </summary>
    public static void EvaluateState() {
        King[] kings = FindObjectsOfType<King>();

        switch(turn) {
            case PlayerSide.Black:
                foreach(King king in kings) {
                    if(king.name == "WhiteKing(Clone)") {
                        if(king.InCheck && !king.InCheckMate) {
                            white.state = PlayerState.Check;
                            break;
                        } else if(king.InStaleMate || (king.InCheck && king.InCheckMate)) {
                            Application.Quit();
                        } else {
                            white.state = PlayerState.Normal;
                            break;
                        }
                    }
                }
                break;
            default:
                foreach(King king in kings) {
                    if(king.name == "BlackKing(Clone)") {
                        if(king.InCheck && !king.InCheckMate) {
                            black.state = PlayerState.Check;
                            break;
                        } else if(king.InCheck && king.InCheckMate) {
                            Application.Quit();
                        } else {
                            black.state = PlayerState.Normal;
                            break;
                        }
                    }
                }
                break;
        }
    }

    // This method is used for switching sides (and visual perspective for main player)
    void FlipBoard() {
        Vector3 rotationVector = new Vector3(0, 0, 180);

        foreach(GameObject obj in FindObjectsOfType<GameObject>()) {
            if(obj.name != "GameManager") {
                obj.transform.Rotate(rotationVector);
            }
        }
    }

    /// <summary>
    /// Find the state of whichever player has the initiative.
    /// </summary>
    /// <returns>Returns the game state of the whichever player has the initiative.</returns>
    public static PlayerState GetState() {
        switch(GameManager.turn) {
            case PlayerSide.Black:
                return black.state;
            default:
                return white.state;
        }
    }
}
