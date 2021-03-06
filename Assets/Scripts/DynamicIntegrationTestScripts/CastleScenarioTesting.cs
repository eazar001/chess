﻿using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("KingSideCastle")]
public class CastleScenarioTesting: ScenarioMoveTesting {

    protected override void RunScenario() {
        King whiteKing = GameObject.Find("WhiteKing(Clone)").GetComponent<King>();

        Vector2 startPos = whiteKing.transform.position;

        SimulateMove(whiteKing, new Vector2(1.7f, -2.45f));

        if(!Vector2.Equals(startPos, (Vector2)whiteKing.transform.position)) {
            IntegrationTest.Fail();
        } else {
            IntegrationTest.Pass();
        }
    }
}