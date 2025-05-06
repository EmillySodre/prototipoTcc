using prototipo1204.Models;

namespace prototipo1204.Repositorios.Interface
{
    public interface ILoginRepositorio
    {
        object Login(string email, string senha);
     
    }
}
