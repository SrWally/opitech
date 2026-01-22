namespace Dominio.Interfaces
{
    public interface ITransaccionalidad : IDisposable
    {
        ISiniestro Siniestros { get; }
        ICiudad Ciudades { get; }
        IDepartamento Departamentos { get; }
        ITipoSiniestro TiposSiniestro { get; }
        
        Task<int> CompleteAsync();
        int Complete();
    }
}