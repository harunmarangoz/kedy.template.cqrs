using Application.Common;

namespace Application.FormRequests.Dtos;

public class CreateUpdateFormDto : IDto
{
    public string Name { get; set; }
}