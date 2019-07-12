using Data;
using Data.Models;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class StoresRepository : IStoresRepository
    {
        private const double MilesToMeters = 1.609 * 1000;

        private readonly StoreLocatorContext _context;
        private readonly IGeometryFactory _geometryFactory;

        public StoresRepository(StoreLocatorContext context)
        {
            _context = context;
            _geometryFactory =
                NtsGeometryServices.Instance.CreateGeometryFactory(Constants.DefaultSRID);
        }

        public async Task<IReadOnlyList<Store>> Get()
        {
            return await _context.Stores
                .Include(s => s.StoreType)
                .OrderByDescending(s => s.StoreType.Weight)
                .ThenBy(s => s.Id)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Store>> Get(
            double latitude,
            double longitude,
            int radiusInMiles)
        {
            // TODO Add validation

            // To represent longitude and latitude, use X for longitude and Y for latitude
            // https://docs.microsoft.com/en-us/ef/core/modeling/spatial#longitude-and-latitude
            IPoint source = _geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

            return await _context.Stores
                .Include(s => s.StoreType)
                .Where(s => s.Location != null)
                .Where(s => s.Location.Distance(source) <= radiusInMiles * MilesToMeters)
                .OrderByDescending(s => s.StoreType.Weight)
                .ThenBy(s => s.Location.Distance(source))
                .ToListAsync();
        }
    }
}
