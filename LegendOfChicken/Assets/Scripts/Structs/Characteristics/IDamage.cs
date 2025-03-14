public struct IDamage
{
    public float PercentBuffDamage { get; set; }

    public float NumberDamage { get; set; }

    public bool CriticalDamage { get; set; }

    public float TotalDamage
    {
        get
        {
            return NumberDamage * (1f + PercentBuffDamage);
        }
    }
}
