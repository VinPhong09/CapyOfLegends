public class DoubleDamageBuff : Buff
{
    private IDamage _damage;

    private void OnEnable()
    {
        if(gameObject.TryGetComponent(out IDamage damage))
        {
            _damage = damage;
            _damage.NumberDamage *= 2f;
        }
    }
}
