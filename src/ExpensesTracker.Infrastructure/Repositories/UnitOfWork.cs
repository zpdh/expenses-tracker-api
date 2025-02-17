﻿using ExpensesTracker.Domain.Infrastructure.Repositories;
using ExpensesTracker.Infrastructure.Data;

namespace ExpensesTracker.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}