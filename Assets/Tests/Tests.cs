using NUnit.Framework;
using UnityEngine;

public class Tests
{
    private GameObject ballObject;
    private Ball ballScript;
    private GameObject gmObject;

    [SetUp]
    public void Setup()
    {
        // GameManager 생성 및 Awake 호출
        gmObject = new GameObject("GameManager");
        var gm = gmObject.AddComponent<GameManager>();
        gm.Awake();  // 강제 호출

        Assert.IsNotNull(GameManager.Instance, "GameManager.Instance is null after creation");
        GameManager.Instance.score = 5f;

        // Ball 생성 및 Rigidbody2D 추가
        ballObject = new GameObject("Ball");
        ballScript = ballObject.AddComponent<Ball>();
        ballObject.AddComponent<Rigidbody2D>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(ballObject);
        Object.DestroyImmediate(gmObject);
    }

    [Test]
    public void Die_Test()
    {
        // Die() 호출
        ballScript.Die();  // 접근이 internal or public이어야 함

        // 검증
        Assert.AreEqual(4.5f, GameManager.Instance.score, 0.001f);
    }
}
