using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("WhiteKingMove")]
public class WhiteKingMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 8) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}