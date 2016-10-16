using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("BlackBishopMove")]
public class BlackBishopMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 14) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}