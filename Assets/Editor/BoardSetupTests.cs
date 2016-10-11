using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;


[TestFixture]
public class BoardSetupTests {

    [SetUpFixture]
    public class TestBoardSetup {

        [SetUp]
        public void SetUp() {
            BoardManager b = new BoardManager(0.68f, 0.70f);
            b.CreateBoard();
        }
    }

    [Test]
    public void BoardTest() {
        int numSquares = GameObject.FindObjectsOfType<Square>().Length;
        int numGameManagers = GameObject.FindObjectsOfType<GameManager>().Length;

        IEnumerable<Square> lightSquares = from square in GameObject.FindObjectsOfType<Square>()
                                           where square.name == "LightSquare(Clone)"
                                           select square;

        IEnumerable<Square> darkSquares = from square in GameObject.FindObjectsOfType<Square>()
                                          where square.name == "DarkSquare(Clone)"
                                          select square;

        Square[] lightSquareArray = lightSquares.ToArray();
        Square[] darkSquareArray = darkSquares.ToArray();

        Assert.AreEqual(numSquares, 64);
        Assert.AreEqual(lightSquareArray.Length, 32);
        Assert.AreEqual(darkSquareArray.Length, 32);
        Assert.AreEqual(numGameManagers, 1);
    }

    [Test]
    public void PieceCount() {
        Piece[] pieces = GameObject.FindObjectsOfType<Piece>();
        IEnumerable<Piece> whitePieces = from piece in pieces
                                         where piece.GetAffiliation() == GameManager.PlayerSide.White
                                         select piece;

        IEnumerable<Piece> blackPieces = from piece in pieces
                                         where piece.GetAffiliation() == GameManager.PlayerSide.Black
                                         select piece;

        Piece[] whitePieceArray = whitePieces.ToArray();
        Piece[] blackPieceArray = blackPieces.ToArray();

        int numPieces = pieces.Length;

        Assert.AreEqual(numPieces, 32);
        Assert.AreEqual(whitePieceArray.Length, 16);
        Assert.AreEqual(blackPieceArray.Length, 16);

        int numWhitePawn = 0;
        int numBlackPawn = 0;
        int numWhiteRook = 0;
        int numBlackRook = 0;
        int numWhiteKnight= 0;
        int numBlackKnight = 0;
        int numWhiteBishop= 0;
        int numBlackBishop = 0;
        int numWhiteQueen = 0;
        int numBlackQueen = 0;
        int numWhiteKing = 0;
        int numBlackKing = 0;

        foreach(Piece piece in pieces) {
            switch(piece.name) {
                case "WhitePawn(Clone)":
                    ++numWhitePawn;
                    break;
                case "BlackPawn(Clone)":
                    ++numBlackPawn;
                    break;
                case "WhiteRook(Clone)":
                    ++numWhiteRook;
                    break;
                case "BlackRook(Clone)":
                    ++numBlackRook;
                    break;
                case "WhiteKnight(Clone)":
                    ++numWhiteKnight;
                    break;
                case "BlackKnight(Clone)":
                    ++numBlackKnight;
                    break;
                case "WhiteBishop(Clone)":
                    ++numWhiteBishop;
                    break;
                case "BlackBishop(Clone)":
                    ++numBlackBishop;
                    break;                
                case "WhiteQueen(Clone)":
                    ++numWhiteQueen;
                    break;
                case "BlackQueen(Clone)":
                    ++numBlackQueen;
                    break;
                case "WhiteKing(Clone)":
                    ++numWhiteKing;
                    break;
                case "BlackKing(Clone)":
                    ++numBlackKing;
                    break;
            }
        }

        Assert.AreEqual(numWhitePawn, 8);
        Assert.AreEqual(numBlackPawn, 8);
        Assert.AreEqual(numWhiteRook, 2);
        Assert.AreEqual(numBlackRook, 2);
        Assert.AreEqual(numWhiteKnight, 2);
        Assert.AreEqual(numBlackKnight, 2);
        Assert.AreEqual(numWhiteBishop, 2);
        Assert.AreEqual(numBlackBishop, 2);
        Assert.AreEqual(numWhiteQueen, 1);
        Assert.AreEqual(numBlackQueen, 1);
        Assert.AreEqual(numWhiteKing, 1);
        Assert.AreEqual(numBlackKing, 1);
    }
}
