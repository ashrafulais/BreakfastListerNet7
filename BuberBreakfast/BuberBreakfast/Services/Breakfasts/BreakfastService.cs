using BuberBreakfast.Models;
using BuberBreakfast.Persistent;
using BuberBreakfast.ServiceErrors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private readonly BuberBreakfastDbContext _dbContext;

    public BreakfastService(BuberBreakfastDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _dbContext.Breakfasts.Add(breakfast);
        _dbContext.SaveChanges();

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        var breakfast = _dbContext.Breakfasts.Find(id);
        if(breakfast == null)
            return Errors.Breakfast.NotFound;

        _dbContext.Breakfasts.Remove(breakfast);
        _dbContext.SaveChanges();

        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        var breakfast = _dbContext.Breakfasts.Find(id);
        return breakfast ?? (ErrorOr<Breakfast>)Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast)
    {
        var IsNewlyCreated = !_dbContext.Breakfasts.Any(b => b.Id == breakfast.Id);

        if(IsNewlyCreated)
            _dbContext.Breakfasts.Add(breakfast);
        else
            _dbContext.Breakfasts.Update(breakfast);

        _dbContext.SaveChanges();

        return new UpsertedBreakfast(IsNewlyCreated);
    }
}