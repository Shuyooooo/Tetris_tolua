using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTest : MonoBehaviour
{
    delegate void forTest(int arg1);

    private void Start()
    {
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");
        var obj = ab.LoadAsset<GameObject>("Circle");
        Instantiate(obj);
    }
}
