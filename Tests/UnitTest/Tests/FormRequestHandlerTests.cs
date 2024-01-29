using Application.FormRequests.Commands;
using Application.FormRequests.Dtos;
using Application.FormRequests.Queries;
using Kedy.Result;
using Infrastructure.FormRequestHandlers.Commands;
using Infrastructure.FormRequestHandlers.Queries;
using Moq;
using Persistence.Contexts;
using Persistence.Interfaces;
using Shouldly;
using UnitTest.Constants;
using UnitTest.Mocks;

namespace UnitTest.Tests;

public class FormRequestHandlerTests
{
    private readonly Mock<DatabaseContext> _databaseContext = MockDatabaseContext.GetDatabaseContext();

    [Fact]
    public async Task ListFormsTest()
    {
        var handler = new ListFormsQueryHandler(_databaseContext.Object);
        var result = await handler.Handle(new ListFormsQuery(), CancellationToken.None);

        result.ShouldBeOfType<SuccessListResult<FormDto>>();
        result.Data.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GetFormTest()
    {
        var handler = new GetFormQueryHandler(_databaseContext.Object);
        var result = await handler.Handle(new GetFormQuery(MockConstants.Form.Form1Id),
            CancellationToken.None);

        result.ShouldBeOfType<SuccessDataResult<FormDto>>();
        result.Data.Name.ShouldBe(MockConstants.Form.Form1Name);
        result.Data.Id.ShouldBe(MockConstants.Form.Form1Id);
    }

    [Fact]
    public async Task CreateFormTest()
    {
        var handler = new CreateFormCommandHandler(_databaseContext.Object);
        var result = await handler.Handle(new CreateFormCommand(new CreateUpdateFormDto
        {
            Name = MockConstants.Form.Form2Name
        }), CancellationToken.None);

        result.ShouldBeOfType<SuccessDataResult<Guid>>();
        result.HasError.ShouldBeFalse();
    }

    [Fact]
    public async Task UpdateFormTest()
    {
        var handler = new UpdateFormCommandHandler(_databaseContext.Object);
        var result = await handler.Handle(new UpdateFormCommand(MockConstants.Form.Form1Id,
            new CreateUpdateFormDto
            {
                Name = $"{MockConstants.Form.Form2Name} Updated"
            }), CancellationToken.None);

        result.ShouldBeOfType<SuccessResult>();
        result.HasError.ShouldBeFalse();
    }
    
    [Fact]
    public async Task UpdateFormWithErrorTest()
    {
        var handler = new UpdateFormCommandHandler(_databaseContext.Object);
        var result = await handler.Handle(new UpdateFormCommand(Guid.NewGuid(),
            new CreateUpdateFormDto
            {
                Name = $"{MockConstants.Form.Form2Name} Updated"
            }), CancellationToken.None);

        result.ShouldBeOfType<ErrorDataResult<Guid>>();
        result.HasError.ShouldBeTrue();
    }

    [Fact]
    public async Task DeleteFormTest()
    {
        var handler = new DeleteFormCommandHandler(_databaseContext.Object);
        var result = await handler.Handle(new DeleteFormCommand(MockConstants.Form.Form1Id),
            CancellationToken.None);
        
        result.ShouldBeOfType<SuccessResult>();
        result.HasError.ShouldBeFalse();
    }
    
    [Fact]
    public async Task DeleteFormWithErrorTest()
    {
        var handler = new DeleteFormCommandHandler(_databaseContext.Object);
        var result = await handler.Handle(new DeleteFormCommand(Guid.NewGuid()),
            CancellationToken.None);
        
        result.ShouldBeOfType<ErrorResult>();
        result.HasError.ShouldBeTrue();
    }
}