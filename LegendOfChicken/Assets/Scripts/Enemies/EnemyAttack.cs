using UnityEngine;

public class EnemyAttack : AttackAction
{
    [SerializeField] private CharacterParameters _enemyParameters;
    [SerializeField] private EnemyAnimator _animator;

    private IDamage _damage;

    private void Awake()
    {
        _damage.NumberDamage = _enemyParameters.Damage * EnemiesSpawner.Instance.GetDifficultyRatio();
    }

    public override void Attack()
    {
        Chicken chicken = ChickensController.Instance.GetFirstChicken();
        chicken.GetDamage(_damage);
    }

    private void SplashDamage(IDamage damage)
    {
        Chicken[] chickens = ChickensController.Instance.GetAllChickens();

        foreach (Chicken i in chickens)
        { 
            i.GetDamage(_damage);
        }
    }

    public void CreateBullet()
    {
        
    }
}
