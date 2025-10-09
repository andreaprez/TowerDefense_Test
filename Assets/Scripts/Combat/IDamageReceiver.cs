namespace TowerDefense.Combat
{
    public interface IDamageReceiver
    {
        public void ApplyDamage(int damagePoints);
        public TeamTag GetTeamTag();
    }
}