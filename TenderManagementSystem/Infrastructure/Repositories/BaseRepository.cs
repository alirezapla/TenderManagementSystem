using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TenderManagementSystem.Core.Exceptions;
using TenderManagementSystem.Infrastructure.Abstractions;
using TenderManagementSystem.Infrastructure.Data;

namespace TenderManagementSystem.Infrastructure.Repositories
{
    public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T>
        where T : class, IEntity
    {
        public void Attach(T entity)
        {
            context.Attach(entity);
        }

        public void SetModifiedProperty<TProperty>(T entity, Expression<Func<T, TProperty>> propertyExpression)
        {
            context.Entry(entity).Property(propertyExpression).IsModified = true;
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }

            catch (Exception ex)
            {
                if (ex.InnerException is not SqlException sqlEx || (sqlEx.Number != 2601 && sqlEx.Number != 2627))
                    throw new RepositoryException("An error occurred while creating the entity.", ex);
                var duplicateValue = ExtractDuplicateValueFromErrorMessage(sqlEx.Message);
                throw new DuplicateKeyException(
                    $"A duplicate key violation occurred. The value '{duplicateValue}' already exists.", ex);
            }
        }


        public async Task<T> DeleteAsync(string id)
        {
            try
            {
                var entity = await context.Set<T>().FindAsync(id);
                if (entity == null) throw new NotFoundException("The entity was not found.");
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<int> SoftDeleteAsync(string id)
        {
            try
            {
                var affectedRows = await context.Set<T>()
                    .Where(t => t.Id == id && !t.IsDeleted)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(t => t.IsDeleted, true));

                return affectedRows;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<T> SoftDeleteAsync(T entity)
        {
            try
            {
                entity.IsDeleted = true;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("An error occurred while deleting the entity.", ex);
            }
        }


        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    context.Set<T>().Attach(entity);
                }

                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await ExistsAsync(entity.Id))
                    throw new NotFoundException("entity not found");

                throw new RepositoryException("Concurrency conflict occurred", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Update failed", ex);
            }
        }

        public async Task<T> UpdatePartialAsync(string id, Action<T> updateAction)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null) throw new NotFoundException("entity not found");
            updateAction(entity);
            await context.SaveChangesAsync();
            return entity;
        }


        private async Task<bool> ExistsAsync(string id)
            => await context.Set<T>().AnyAsync(e => e.Id == id);

        private static string ExtractDuplicateValueFromErrorMessage(string errorMessage)
        {
            var startIndex = errorMessage.LastIndexOf('(') + 1;
            var endIndex = errorMessage.LastIndexOf(')');
            return errorMessage.Substring(startIndex, endIndex - startIndex);
        }
    }
}