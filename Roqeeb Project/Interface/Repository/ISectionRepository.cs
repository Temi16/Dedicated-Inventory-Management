using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Roqeeb_Project.Entities;

namespace Roqeeb_Project.Interface.Repository
{
    public interface ISectionRepository
    {
        Task<Section> CreateAsync(Section section, CancellationToken cancellationToken);
        Task<Section> UpdateAsync(Section section, CancellationToken cancellationToken);
        Task<Section> GetByNameAsync(string sectionId, CancellationToken cancellationToken);
        Task<Section> GetAsync(Expression<Func<Section, bool>> expression, CancellationToken cancellationToken);
    }
}
