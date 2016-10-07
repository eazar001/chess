using UnityEngine;
using System.Collections;

public class GameManager: MonoBehaviour {

    public static PlayerSide turn { get; set; }
    public static Player white;
    public static Player black;
    public PieceMovement movementProperties = new PieceMovement(0.68f, 0.70f);
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

    public struct Player {
        public PlayerSide side;
        public PlayerState state;

        public Player(PlayerSide side, PlayerState state) {
            this.side = side;
            this.state = state;
        }
    }

    public enum PlayerSide {
        White,
        Black,
    }

    public enum PlayerState {
        Normal,
        Check,
        Checkmate,
    }

    void Start() {
        white = new Player(PlayerSide.White, PlayerState.Normal);
        black = new Player(PlayerSide.Black, PlayerState.Normal);
        turn = white.side;

        float xMove = movementProperties.xMove;
        float yMove = movementProperties.yMove;

        b = new BoardManager(xMove, yMove);
        b.CreateBoard();
    }

    void Update() {
        if(Input.GetKeyDown("escape")) {
            Application.Quit();
        }
    }

    public static void NextTurn() {
        if(turn == PlayerSide.Black) {
            turn = PlayerSide.White;
        } else {
            turn = PlayerSide.Black;
        }
    }

    public static void EvaluateState() {
        King[] kings = FindObjectsOfType<King>();

        switch(turn) {
            case PlayerSide.Black:
                foreach(King king in kings) {
                    if(king.name == "WhiteKing(Clone)") {
                        if(king.InCheck()) {
                            white.state = PlayerState.Check;
                            break;
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
                        if(king.InCheck()) {
                            black.state = PlayerState.Check;
                            break;
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
        GameObject camera = GameObject.FindWithTag("MainCamera");

        camera.transform.Rotate(new Vector3(0, 0, 180));

        foreach(GameObject obj in FindObjectsOfType<GameObject>()) {
            if(!obj.CompareTag("MainCamera") && obj.name != "GameManager") {
                obj.transform.Rotate(new Vector3(0, 0, 180));
            }
        }
    }
}
