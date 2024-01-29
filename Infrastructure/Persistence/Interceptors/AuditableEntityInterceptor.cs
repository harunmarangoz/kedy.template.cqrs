using Application.Services;
using Contracts.Constants;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.Interceptors;

public class AuditableEntityInterceptor(IWorkContext workContext) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableEntities(eventData.Context);
        }

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableEntities(DbContext context)
    {
        var now = DateTime.UtcNow;
        var modifiedEntities = context.ChangeTracker.Entries<IAuditableEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified).ToList();

        foreach (var entity in modifiedEntities)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    entity.Property(PersistenceConstants.CreatedAtFieldName).CurrentValue = now;
                    entity.Property(PersistenceConstants.CreatedByFieldName).CurrentValue = workContext.Email;
                    break;
                case EntityState.Modified:
                    if (entity.Properties.Any(x => x.Metadata.Name == PersistenceConstants.LastModifiedAtFieldName))
                        entity.Property(PersistenceConstants.LastModifiedAtFieldName).CurrentValue = now;
                    if (entity.Properties.Any(x => x.Metadata.Name == PersistenceConstants.LastModifiedByFieldName))
                        entity.Property(PersistenceConstants.LastModifiedByFieldName).CurrentValue = workContext.Email;
                    break;
            }
        }
    }
}