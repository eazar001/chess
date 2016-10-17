using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("EnPassant")]
public class EnPassantScenarioTesting: ScenarioMoveTesting {

    protected override void RunScenario() {
        King whiteKing = GameObject.Find("WhiteKing(Clone)").GetComponent<King>();
        Pawn firstPawn = GameObject.Find("BlackPawn(Clone1)").GetComponent<Pawn>();
        Pawn whitePawn = GameObject.Find("WhitePawn(Clone)").GetComponent<Pawn>();

        SimulateMove(whiteKing, new Vector2(0.34f, -1.75f));
        SimulateMove(firstPawn, new Vector2(-0.34f, 0.35f));
        SimulateMove(whitePawn, new Vector2(-0.34f, 1.05f));

        if((Vector2)whitePawn.transform.position == new Vector2(-0.34f, 1.05f)) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}