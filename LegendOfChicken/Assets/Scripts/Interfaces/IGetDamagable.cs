using System;

public interface IGetDamagable
{
    public bool IsDied { get; set; }

    public void GetDamage(IDamage damage);
}
