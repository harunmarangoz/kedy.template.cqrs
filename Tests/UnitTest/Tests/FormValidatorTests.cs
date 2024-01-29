using Application.FormRequests.Commands;
using Application.FormRequests.Dtos;
using Infrastructure.FormRequestHandlers.Validators;
using Shouldly;

namespace UnitTest.Tests;

public class FormValidatorTests
{
    [Fact]
    public async Task FormValidatorTest()
    {
        var validator = new CreateUpdateFormDtoValidator();
        var result = await validator.ValidateAsync(new CreateUpdateFormDto
        {
            Name = "Test"
        });

        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public async Task FormValidatorTest2()
    {
        var validator = new CreateUpdateFormDtoValidator();
        var result = await validator.ValidateAsync(new CreateUpdateFormDto
        {
            Name = ""
        });

        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public async Task CreateFormCommandValidatorTest()
    {
        var validator = new CreateFormCommandValidator();
        var result = await validator.ValidateAsync(new CreateFormCommand(new CreateUpdateFormDto
        {
            Name = "Test"
        }));

        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public async Task CreateFormCommandValidatorTest2()
    {
        var validator = new CreateFormCommandValidator();
        var result = await validator.ValidateAsync(new CreateFormCommand(new CreateUpdateFormDto
        {
            Name = ""
        }));

        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public async Task UpdateFormCommandValidatorTest()
    {
        var validator = new UpdateFormCommandValidator();
        var result = await validator.ValidateAsync(new UpdateFormCommand(Guid.NewGuid(), new CreateUpdateFormDto
        {
            Name = "Test"
        }));

        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public async Task UpdateFormCommandValidatorTest2()
    {
        var validator = new UpdateFormCommandValidator();
        var result = await validator.ValidateAsync(new UpdateFormCommand(Guid.NewGuid(), new CreateUpdateFormDto
        {
            Name = ""
        }));

        result.IsValid.ShouldBeFalse();
    }
}