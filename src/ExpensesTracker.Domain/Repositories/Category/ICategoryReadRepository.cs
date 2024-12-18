﻿namespace ExpensesTracker.Domain.Repositories.Category;

public interface ICategoryReadRepository
{
    Task<List<Entities.Category>?> GetAllCategoriesAsync();
    bool CategoryWithNameDoesNotExist(string name);
}