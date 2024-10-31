public interface ILiveComponent
{
    float MaxHealth { get; set; }
    float Health { get; set; }
    void SetDamage(float damage);
}
