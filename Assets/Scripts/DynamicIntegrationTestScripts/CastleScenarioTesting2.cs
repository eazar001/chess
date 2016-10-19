using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("QueenSideCastle")]
public class CastleScenarioTesting2: ScenarioMoveTesting {

    protected override void RunScenario() {
        King whiteKing = GameObject.Find("WhiteKing(Clone)").GetComponent<King>();

        Vector2 startPos = whiteKing.transform.position;

        SimulateMove(whiteKing, new Vector2(-1.02f, -2.45f));

        if(!Vector2.Equals(startPos, (Vector2)whiteKing.transform.position)) {
            Debug.Log("won't happen");
            IntegrationTest.Fail();
        } else {
            Debug.Log("should happen");
            IntegrationTest.Pass();
        }
    }
}