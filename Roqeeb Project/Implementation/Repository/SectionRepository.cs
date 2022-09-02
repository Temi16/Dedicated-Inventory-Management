using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Roqeeb_Project.Context;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Interface.Repository;

namespace Roqeeb_Project.Implementation.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly ApplicationContext _context;
        public SectionRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Section> CreateAsync(Section section, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (section == null) throw new ArgumentNullException(nameof(section));
            await _context.Sections.AddAsync(section, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return section;
        }

        public async Task<Section> GetAsync(Expression<Func<Section, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var section = await _context.Sections
                .Include(s => s.Store)
                .ThenInclude(st => st.StoreName)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return section;
        }

        public async Task<Section> GetByNameAsync(string sectionId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (sectionId == null) throw new ArgumentNullException(null);
            var section = await _context.Sections
                .Include(s => s.Store)
                .ThenInclude(st => st.StoreName)
                .SingleOrDefaultAsync(s => s.Id == sectionId, cancellationToken);
            return section;
        }

        public Task<Section> UpdateAsync(Section section, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
