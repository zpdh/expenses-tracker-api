﻿using ExpensesTracker.Api.Controllers.Base;
using ExpensesTracker.Application.Category.Commands;
using ExpensesTracker.Application.Category.Queries;
using ExpensesTracker.Domain.Requests.Category;
using ExpensesTracker.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers.Implementations;

[Authorize]
public class CategoryController : ApiController
{
    public CategoryController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var query = new GetAllCategoriesQuery();

        var result = await Sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new AddCategoryCommand(request);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Created() : HandleFailure(result);
    }
}