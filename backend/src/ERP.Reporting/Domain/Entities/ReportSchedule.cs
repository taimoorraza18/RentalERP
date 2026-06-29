using ERP.SharedKernel.Base;

namespace ERP.Reporting.Domain.Entities;

public sealed class ReportSchedule : AuditableEntity
{
    public long ReportDefinitionId { get; private set; }
    public long UserId { get; private set; }
    public string ScheduleName { get; private set; } = string.Empty;
    public string? ParameterJson { get; private set; }
    public short Frequency { get; private set; }
    public string? CronExpression { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public DateTime? NextRunDate { get; private set; }
    public DateTime? LastRunDate { get; private set; }
    public short DeliveryMethod { get; private set; }
    public bool IsEnabled { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ReportDefinition? ReportDefinition { get; private set; }
}
