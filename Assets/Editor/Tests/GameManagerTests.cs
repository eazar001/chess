using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class GameManagerTests {

	[Test]
	public void TurnManagement() {
		Assert.AreEqual(GameManager.turn, GameManager.PlayerSide.White);
        Assert.AreEqual(GameManager.PlayerState.Normal, GameManager.GetState());

        GameManager.NextTurn();

        Assert.AreEqual(GameManager.turn, GameManager.PlayerSide.Black);
        Assert.AreEqual(GameManager.PlayerState.Normal, GameManager.GetState());

        GameManager.NextTurn();

        Assert.AreEqual(GameManager.turn, GameManager.PlayerSide.White);
        Assert.AreEqual(GameManager.PlayerState.Normal, GameManager.GetState());
	}
}