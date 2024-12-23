﻿using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Repositories.User;
using Moq;

namespace TestUtils.Repositories;

public sealed class UserReadRepositoryMock
{
    public static Mock<IUserReadRepository> Create => new();

    public static void SetupIsEmailUnique(Mock<IUserReadRepository> mock)
    {
        mock.Setup(moq => moq.IsEmailUnique(It.IsAny<string>())).Returns(true);
        mock.Setup(moq => moq.IsEmailUniqueAsync(It.IsAny<string>())).ReturnsAsync(true);
    }

    public static void SetupGetUserByEmail(Mock<IUserReadRepository> mock, User user)
    {
        mock.Setup(moq => moq.GetUserByEmail(user.Email)).Returns(user);
        mock.Setup(moq => moq.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);
    }

    public static void SetupGetUserById(Mock<IUserReadRepository> mock, int id, User user)
    {
        mock.Setup(moq => moq.GetByIdAsync(id)).ReturnsAsync(user);
    }
}