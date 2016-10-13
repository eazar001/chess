using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class PieceTests {

    [TearDown]
    public void CleanUp() {
        IEnumerable<Piece> allPieces = from piece in Resources.FindObjectsOfTypeAll<Piece>()
                                       where !piece.isActiveAndEnabled &&
                                             piece.name.Contains("Clone")
                                       select piece;

        foreach(Piece piece in allPieces) {
            piece.gameObject.SetActive(true);
        }
    }

	[Test]
	public void RawPieceMovement() {
		Pawn testPawn = GameObject.FindObjectOfType<Pawn>();

        Vector3 newPos = testPawn.transform.position + Vector3.up;

        testPawn.MoveTo((Vector2)testPawn.transform.position + Vector2.up);
        Assert.AreEqual(testPawn.transform.position, newPos);

        newPos = testPawn.transform.position + new Vector3(-5.0f, 2.0f, 0.0f);

        testPawn.MoveTo((Vector2)testPawn.transform.position + new Vector2(-5.0f, 2.0f));
        Assert.AreEqual(testPawn.transform.position, newPos);
	}

    [Test]
    public void PickUpPiece() {
        Pawn testPawn = GameObject.FindObjectOfType<Pawn>();
        Assert.That(testPawn.isActiveAndEnabled);

        testPawn.PickUp();
        Assert.That(!testPawn.isActiveAndEnabled);
        Assert.NotNull(testPawn);
    }

    [Test]
    public void TestAffiliation() {
        IEnumerable<Piece> bPieces = from piece in GameObject.FindObjectsOfType<Piece>()
                                     where piece.GetAffiliation() == GameManager.PlayerSide.Black
                                     select piece;

        IEnumerable<Piece> wPieces = from piece in GameObject.FindObjectsOfType<Piece>()
                                     where piece.GetAffiliation() == GameManager.PlayerSide.White
                                     select piece;

        Assert.AreEqual(bPieces.Count(), 16);
        Assert.AreEqual(wPieces.Count(), 16);

        foreach(Piece piece in bPieces) {
            Assert.That(piece.name.Contains("Black"));
        }

        foreach(Piece piece in wPieces) {
            Assert.That(piece.name.Contains("White"));
        }    
    }
}