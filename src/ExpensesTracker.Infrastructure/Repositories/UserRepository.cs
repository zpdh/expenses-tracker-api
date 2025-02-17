﻿using System.Data;
using Dapper;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Repositories.User;
using ExpensesTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure.Repositories;

public class UserRepository : IUserReadRepository, IUserWriteRepository
{
    private readonly IDbConnection _connection;
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
        _connection = context.Database.GetDbConnection();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        const string query = "SELECT * From Users WHERE Id = @id";
        var parameters = new { Id = id };

        var user = await _connection.QuerySingleOrDefaultAsync<User>(query, parameters);

        return user;
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        const string query = "SELECT * From Users WHERE Email = @email";
        var parameters = new { Email = email };

        var user = _connection.QueryFirstOrDefaultAsync<User>(query, parameters);

        return user;
    }

    public User? GetUserByEmail(string email)
    {
        const string query = "SELECT * From Users WHERE Email = @email";
        var parameters = new { Email = email };

        var user = _connection.QueryFirstOrDefault<User>(query, parameters);

        return user;
    }

    public bool IsEmailUnique(string email)
    {
        var user = GetUserByEmail(email);

        return user is null;
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        var user = await GetUserByEmailAsync(email);

        return user is null;
    }

    public bool UserExists(int id)
    {
        const string query = "SELECT COUNT(1) FROM Users WHERE Id = @id";
        var parameters = new { Id = id };

        var count = _connection.ExecuteScalar<int>(query, parameters);
        return count > 0;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task AddRoleAsync(User user, Role role)
    {
        var roleInDb = await _context.Roles.FirstAsync(r => r.Id == role.Id);

        user.Roles.Add(roleInDb);
    }

    public async Task UpdateAsync(User user)
    {
        var userInDb = await _context.Users.FirstAsync(u => u.Id == user.Id);

        userInDb.Name = user.Name;
        userInDb.Email = user.Email;
    }

    public async Task UpdatePasswordAsync(User user)
    {
        var userInDb = await _context.Users.FirstAsync(u => u.Id == user.Id);

        userInDb.Password = user.Password;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FirstAsync(u => u.Id == id);

        _context.Remove(user);
    }
}