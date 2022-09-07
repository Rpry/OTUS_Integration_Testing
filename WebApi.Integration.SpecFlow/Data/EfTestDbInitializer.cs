using DataAccess.EntityFramework;

namespace WebApi.Integration.SpecFlow.Data
{
    public class EfTestDbInitializer
    {
        private readonly DatabaseContext _dataContext;

        public EfTestDbInitializer(DatabaseContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void InitializeDb()
        {
            _dataContext.Database.EnsureDeleted();
            _dataContext.Database.EnsureCreated();
        }

        public void CleanDb()
        {
            _dataContext.Database.EnsureDeleted();
        }
    }
}