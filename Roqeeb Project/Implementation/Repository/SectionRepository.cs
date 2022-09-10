using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<Section>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sections = await _context.Sections
                .Include(sec => sec.Store)
                .ToListAsync(cancellationToken);
            return sections;
        }

        public async Task<Section> GetAsync(Expression<Func<Section, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var section = await _context.Sections
                .Include(s => s.Store)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return section;
        }

        public async Task<Section> GetByNameAsync(string sectionName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (sectionName == null) throw new ArgumentNullException(null);
            var section = await _context.Sections
                .Include(s => s.Store)
                .SingleOrDefaultAsync(sec => sec.SectionName == sectionName, cancellationToken);
            return section;
        }
        
        public async Task<IList<Section>> GetAllByStoreAsync(Expression<Func<Section, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var sections = await _context.Sections
                .Include(sec => sec.Store)
                .ToListAsync(cancellationToken);
            return sections;
                
        }
        

        public Task<Section> UpdateAsync(Section section, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Section> GetByStoreAsync(Expression<Func<Section, bool>> expression, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var section = await _context.Sections
                .Include(se => se.Store)
                .SingleOrDefaultAsync(expression, cancellationToken);
            return section;
        }
        
    }
}
