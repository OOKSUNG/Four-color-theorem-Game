using NUnit.Framework;
using UnityEngine;

public class GameManagerSingletonTests
{
    [TearDown]
    public void Cleanup()
    {
        GameManager.Instance = null;

        foreach (var gm in Object.FindObjectsOfType<GameManager>())
        {
            Object.DestroyImmediate(gm.gameObject);
        }
    }

    [Test]
    public void Is_Not_Null()
    {
        var gmObj = new GameObject("GameManager");
        var gm = gmObj.AddComponent<GameManager>();
        gm.Awake(); //Awake 강제 호출

        Assert.IsNotNull(GameManager.Instance);
    }

    [Test]
    public void Points_To_Same_Object()
    {
        var gmObj = new GameObject("GM1");
        var gm1 = gmObj.AddComponent<GameManager>();
        gm1.Awake(); //Awake 강제 호출

        Assert.AreSame(gm1, GameManager.Instance);
    }

    [Test]
    public void OnlyOneInstance()
    {
        var gmObj1 = new GameObject("GM1");
        var gm1 = gmObj1.AddComponent<GameManager>();
        gm1.Awake(); //Awake 강제 호출
        Assert.AreSame(gm1, GameManager.Instance);

        var gmObj2 = new GameObject("GM2");
        var gm2 = gmObj2.AddComponent<GameManager>();
        gm2.Awake(); //Awake 강제 호출

        // gm2가 DestroyImmediate 되었기 때문에 Instance는 gm1이어야 함
        Assert.AreSame(gm1, GameManager.Instance);
        Assert.IsTrue(gm2 == null || !gm2 || gm2.gameObject == null || !gm2.gameObject.activeInHierarchy);
    }
}