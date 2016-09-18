using UnityEngine;
using System.Collections;

public class BoardManager {

    GameObject[] allObjs;
    GameObject lightSquare, darkSquare;

    float xMove, yMove;
    public static bool squareSelected { get; set; }
    public static Piece srcPiece { get; set; }
    public static Square srcSquare { get; set; }

    public BoardManager(float x, float y) {
        xMove = x;
        yMove = y;
        squareSelected = false;
    }

    public void CreateBoard() {

        allObjs = Resources.FindObjectsOfTypeAll<GameObject>();
        
        foreach(GameObject obj in allObjs) {
            if(lightSquare != null && darkSquare != null) {
                break;
            }

            switch(obj.name) {
                case "LightSquare":
                    lightSquare = obj;
                    break;
                case "DarkSquare":
                    darkSquare = obj;
                    break;
            }
        }


        float x = -3.5f * xMove;
        float y = -3.5f * yMove;

        bool invert = false;

        for(int i = 0; i < 8; ++i) {
            for(int j = 0; j < 4; ++j) {

                switch(i) {
                    case 0:
                        switch(j) {
                            case 0:
                                MakeBoard("WhiteRook", "WhiteKnight", invert, ref x, ref y);
                                break;
                            case 1:
                                MakeBoard("WhiteBishop", "WhiteQueen", invert, ref x, ref y);
                                break;
                            case 3:
                                MakeBoard("WhiteKnight", "WhiteRook", invert, ref x, ref y);
                                break;
                            default:
                                MakeBoard("WhiteKing", "WhiteBishop", invert, ref x, ref y);
                                break;
                        }

                        break;

                    case 7:
                        switch(j) {
                            case 0:
                                MakeBoard("BlackRook", "BlackKnight", invert, ref x, ref y);
                                break;
                            case 1:
                                MakeBoard("BlackBishop", "BlackQueen", invert, ref x, ref y);
                                break;
                            case 3:
                                MakeBoard("BlackKnight", "BlackRook", invert, ref x, ref y);
                                break;
                            default:
                                MakeBoard("BlackKing", "BlackBishop", invert, ref x, ref y);
                                break;
                        }

                        break;

                    case 1:
                        MakeBoard("WhitePawn", "WhitePawn", invert, ref x, ref y);
                        break;
                    case 6:
                        MakeBoard("BlackPawn", "BlackPawn", invert, ref x, ref y);
                        break;
                    default:
                        if(invert) {
                            InstantiateObject(lightSquare, x, y);
                            x += xMove;
                            InstantiateObject(darkSquare, x, y);
                        } else {
                            InstantiateObject(darkSquare, x, y);
                            x += xMove;
                            InstantiateObject(lightSquare, x, y);
                        }

                        x += xMove;
                        break;
                }
            }

            x = -3.5f * xMove;
            y += yMove;
            invert = !invert;

        }
    }
    

    void MakeBoard(string o1, string o2, bool invert, ref float x, ref float y) {

        foreach(GameObject obj in allObjs) {
            if(obj.name == o1) {
                InstantiateObject(obj, x, y);
                break;
            }
        }

        if(invert) {
            InstantiateObject(lightSquare, x, y);
        } else {
            InstantiateObject(darkSquare, x, y);
        }

        x += xMove;

        foreach(GameObject obj in allObjs) {
            if(obj.name == o2) {
                InstantiateObject(obj, x, y);
                break;
            }
        }

        if(invert) {
            InstantiateObject(darkSquare, x, y);
        } else {
            InstantiateObject(lightSquare, x, y);
        }

        x += xMove;
    }

    GameObject InstantiateObject(GameObject obj, float x, float y) {
        GameObject o;
        if(obj.tag == "Square") {
            o = GameObject.Instantiate(obj, new Vector2(x, y), Quaternion.identity) as GameObject;
        } else {
            Vector3 newPos = new Vector3(x, y, 1.0f);
            o = GameObject.Instantiate(obj, newPos, Quaternion.identity) as GameObject;
        }

        o.SetActive(true);

        return o;
    }
}