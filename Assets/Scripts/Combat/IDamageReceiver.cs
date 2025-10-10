namespace TowerDefense.Combat
{
    public interface IDamageReceiver
    {
        public void ApplyDamage(int damagePoints);
        public void ApplyEffect(CombatEffect effectType);
        public void RemoveEffect(CombatEffect effectType);
        public TeamTag GetTeamTag();
    }
}