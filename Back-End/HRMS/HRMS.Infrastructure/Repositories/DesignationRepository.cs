using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HRMS.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Infrastructure.Repositories
{
    public class DesignationRepository
    : Repository<Designation>, IDesignationRepository
    {
        private readonly ApplicationDbContext _context;

        public DesignationRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> DesignationNameExistsAsync(
            Guid organizationId,
            string designationName,
            Guid? excludeDesignationId = null,
            CancellationToken cancellationToken = default)
        {
            return await _context.Designations
                .AnyAsync(
                    x =>
                        x.OrganizationId == organizationId &&
                        x.Name == designationName &&
                        (!excludeDesignationId.HasValue ||
                         x.Id != excludeDesignationId.Value),
                    cancellationToken);
        }

        public async Task<(IReadOnlyList<Designation> Items, int TotalCount)> GetPagedAsync(
            Guid organizationId,
            string? search,
            string? sortBy,
            bool sortDescending,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Designations
                .AsNoTracking()
                .Where(x => x.OrganizationId == organizationId);

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(x =>
                    x.Name.Contains(search) ||
                    (x.Description != null &&
                     x.Description.Contains(search)));
            }

            // Sorting
            query = sortBy?.ToLower() switch
            {
                "name" => sortDescending
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name),

                "description" => sortDescending
                    ? query.OrderByDescending(x => x.Description)
                    : query.OrderBy(x => x.Description),

                _ => query.OrderBy(x => x.Name)
            };

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}
