using System.Collections.Generic;
using UnityEngine;
using Tools.Pool;

public class ChickenAttack : AttackAction
{
    [SerializeField] private ChickenParameters _chickenParameters;
    [SerializeField] private ChickenAnimator _animator;
    [SerializeField] private EnhanceElementParameters _enhanceDamageParameters;
    [SerializeField] private ChickenEquipmentParameters _equipmentParameters;

    private Enemy _enemyPos;

    private IDamage _damage;

    private PoolContainer<Bullet> _poolContainer = new PoolContainer<Bullet>();

    private void Awake()
    {
        InvokeRepeating(nameof(Attack), _chickenParameters.AttackSpeed, _chickenParameters.AttackSpeed);

        DamageRefresh();
    }

    public override void Attack()
    {
        if(EnemiesSpawner.Instance.TryGetEnemy(out _enemyPos))
        {
            _animator.OnPlayAttackAnimation();
        }
    }

    public void CreateBullet()
    {
        if (EnemiesSpawner.Instance.TryGetEnemy(out _enemyPos))
        {
            Bullet bullet;

            if (_equipmentParameters.WeaponIcon == null || _equipmentParameters.WeaponIcon.Bullet == null)
            {
                if(_poolContainer.TryGetObject(out bullet, bullet => bullet.CanInteract)) CreateOrInitializeBullet(bullet, true);
                else CreateOrInitializeBullet(_defaultBullet, false);

                return;
            }

            if (_poolContainer.TryGetObject(out bullet, bullet => bullet.CanInteract)) CreateOrInitializeBullet(bullet, true);
            else CreateOrInitializeBullet(_equipmentParameters.WeaponIcon.Bullet, false);

        }
    }

    public void EnhanceDamage()
    {
        _chickenParameters.Damage += 10;

        DamageRefresh();
        Debug.Log("damage enhanced");
    }

    private void CreateOrInitializeBullet(Bullet bullet, bool onlyInitialize)
    {
        Bullet createdBullet;

        if (!onlyInitialize)
            createdBullet = Instantiate(bullet);
        else
            createdBullet = bullet;

        _damage.NumberDamage = _chickenParameters.Damage;
        Damage(ref _damage, _chickenParameters.CriticalHitChance, _chickenParameters.CriticalHitDamage);

        createdBullet.Initialize(_damage, transform.position, _enemyPos.transform.position, _enemyPos);
        _poolContainer.Objs.Add(createdBullet);
    }

    private IDamage Damage(ref IDamage damage, float critHitChance, float critHitDamage)
    {
        float chance = UnityEngine.Random.Range(0f, 1f);

        if (chance < (critHitChance / 100f))
        {
            damage.CriticalDamage = true;
            damage.NumberDamage *= (1f + (critHitDamage / 100f));
            return damage;
        }

        damage.CriticalDamage = false;
        return damage;
    }

    private void OnDestroy()
    {
        foreach (Bullet i in _poolContainer.Objs)
        {
            if (i == null)
                continue;
            Destroy(i.gameObject);
        }
        _poolContainer.Objs.Clear();
    }

    public void DamageRefresh()
    {
        _enhanceDamageParameters.Value = _chickenParameters.Damage;
        _damage.NumberDamage = _chickenParameters.Damage;
        Debug.Log("damage refreshed");
    }
}
