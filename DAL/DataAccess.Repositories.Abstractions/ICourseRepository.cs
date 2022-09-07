using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Abstraction;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий работы с уроками
    /// </summary>
    public interface ICourseRepository: IRepository<Course, int>
    {
        /// <summary>
        /// Получить постраничный список
        /// </summary>
        /// <param name="page">номер страницы</param>
        /// <param name="itemsPerPage">объем страницы</param>
        /// <returns>список ДТО курсов</returns>
        Task<List<Course>> GetPagedAsync(int page, int itemsPerPage);
    }
}
