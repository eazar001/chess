using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("Castle")]
public class CastleScenarioTesting: ScenarioMoveTesting {

    protected override void RunScenario() {
        King whiteKing = GameObject.Find("WhiteKing(Clone)").GetComponent<King>();

        Vector2 startPos = whiteKing.transform.position;

        SimulateMove(whiteKing, new Vector2(-1.02f, -2.45f));

        if((Vector2)whiteKing.transform.position != startPos) {
            IntegrationTest.Fail();
        }

        SimulateMove(whiteKing, new Vector2(1.7f, -2.45f));

        if((Vector2)whiteKing.transform.position == new Vector2(1.7f, -2.45f)) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}