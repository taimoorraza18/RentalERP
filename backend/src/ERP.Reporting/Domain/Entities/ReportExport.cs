using ERP.SharedKernel.Base;

namespace ERP.Reporting.Domain.Entities;

public sealed class ReportExport : AuditableEntity
{
    public long ReportExecutionId { get; private set; }
    public string FileName { get; private set; } = string.Empty;
    public string FileExtension { get; private set; } = string.Empty;
    public long FileSize { get; private set; }
    public string StoragePath { get; private set; } = string.Empty;
    public int DownloadCount { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ReportExecution? ReportExecution { get; private set; }
}
