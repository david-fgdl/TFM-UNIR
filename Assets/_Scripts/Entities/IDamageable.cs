/* SCRIPT PARA CREAR UNA INTERFAZ IDAMAGEABLE (Interfaz para usar en cualquier elemento que queremos que tenga vida y sea dañable) */
public interface IDamageable
{
    float Health { get; set; }
    void Damage();
}
