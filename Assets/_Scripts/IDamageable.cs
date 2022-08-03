// INTERFAZ IDAMAGEABLE
// Una interfaz para usar en cualquier elemento que queremos que tenga vida y sea dañable.
public interface IDamageable
{
    float Health { get; set; }
    void Damage();
}
