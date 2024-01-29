using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Form : AuditableSoftDeleteEntity
{
    public string Name { get; set; }
    public FormType FormType { get; set; }
}