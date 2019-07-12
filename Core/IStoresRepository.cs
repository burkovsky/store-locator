using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface IStoresRepository
    {
        Task<IReadOnlyList<Store>> Get();

        Task<IReadOnlyList<Store>> Get(double latitude, double longitude, int radiusInMiles);
    }
}
