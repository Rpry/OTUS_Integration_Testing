using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.EntityFramework;
using DataAccess.Implementation;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий работы с уроками
    /// </summary>
    public class LessonRepository: Repository<Lesson, int>, ILessonRepository 
    {
        public LessonRepository(DatabaseContext context): base(context)
        {
        }
        
        public async Task<List<Lesson>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }
    }
}
