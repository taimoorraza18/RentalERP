using ERP.SharedKernel.Base;

namespace ERP.Reporting.Domain.Entities;

public sealed class ReportDefinition : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long ReportCategoryId { get; private set; }
    public string ReportCode { get; private set; } = string.Empty;
    public string ReportName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string DataSource { get; private set; } = string.Empty;
    public short ReportType { get; private set; }
    public string OutputFormat { get; private set; } = string.Empty;
    public bool AllowExport { get; private set; }
    public bool AllowScheduling { get; private set; }
    public int CacheDuration { get; private set; }
    public bool IsSystem { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ReportCategory? ReportCategory { get; private set; }
    public ICollection<ReportParameter> ReportParameters { get; private set; } = [];
    public ICollection<SavedReport> SavedReports { get; private set; } = [];
    public ICollection<ReportExecution> ReportExecutions { get; private set; } = [];
    public ICollection<ReportSchedule> ReportSchedules { get; private set; } = [];
    public ICollection<ReportAttachment> ReportAttachments { get; private set; } = [];
    public ICollection<ReportNote> ReportNotes { get; private set; } = [];
    public ICollection<ReportActivity> ReportActivities { get; private set; } = [];
    public ICollection<ReportTimeline> ReportTimelines { get; private set; } = [];
}
