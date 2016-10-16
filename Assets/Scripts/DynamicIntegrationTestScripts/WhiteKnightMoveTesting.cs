using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("WhiteKnightMove")]
public class WhiteKnightMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 8) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}