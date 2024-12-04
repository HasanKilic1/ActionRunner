using UnityEngine;

public class Apple : PickUp
{
    LevelGenerator levelGenerator;
    [SerializeField] float moveSpeedChangeAmount = 3f;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    protected override void OnPickUp()
    {
        levelGenerator.ChangeScrollSpeed(moveSpeedChangeAmount);
        Destroy(gameObject);
    }
}
