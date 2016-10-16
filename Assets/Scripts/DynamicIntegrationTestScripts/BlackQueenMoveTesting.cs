using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("BlackQueenMove")]
public class BlackQueenMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 28) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}