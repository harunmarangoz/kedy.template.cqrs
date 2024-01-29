using Application.Services;
using Contracts.Constants;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.Interceptors;

public class SoftDeleteInterceptor(IWorkContext workContext) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not null)
        {
            UpdateSoftDeleteEntities(eventData.Context);
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
            UpdateSoftDeleteEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateSoftDeleteEntities(DbContext context)
    {
        var now = DateTime.UtcNow;
        var softDeletedEntities = context.ChangeTracker.Entries<ISoftDeleteEntity>()
            .Where(e => e.State == EntityState.Deleted);
        foreach (var entity in softDeletedEntities)
        {
            entity.Entity.IsDeleted = true;
            entity.Entity.DeletedAt = now;
            entity.Entity.DeletedBy = workContext.Email;
            entity.State = EntityState.Modified;
        }
    }
}