using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Abstraction;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий работы с уроками
    /// </summary>
    public interface ILessonRepository: IRepository<Lesson, int>
    {
        Task<List<Lesson>> GetPagedAsync(int page, int itemsPerPage);
    }
}
