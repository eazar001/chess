using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("WhiteQueenMove")]
public class WhiteQueenMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 28) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}