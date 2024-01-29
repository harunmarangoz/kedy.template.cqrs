using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Interfaces;
using UnitTest.Constants;

namespace UnitTest.Mocks;

public static class MockDatabaseContext
{
    public static Mock<DatabaseContext> GetDatabaseContext()
    {
        var mockDbContext = new Mock<DatabaseContext>();
        mockDbContext
            .Setup<DbSet<Form>>(x => x.Forms)
            .ReturnsDbSet(GetForms());

        return mockDbContext;
    }
    private static List<Form> GetForms()
    {
        var forms = new List<Form>
        {
            new()
            {
                Id = MockConstants.Form.Form1Id,
                Name = MockConstants.Form.Form1Name
            },
            new()
            {
                Id = MockConstants.Form.Form2Id,
                Name = MockConstants.Form.Form2Name
            }
        };
        return forms;
    }
}