using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMgr
{
    private static TestMgr Instance;

    public TestMgr instance
    {
        get
        {
            if (instance == null)
            {
                Instance = new TestMgr();
            }

            return Instance;
        }
    }

    private TestMgr() { }

}
