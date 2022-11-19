﻿namespace CleanArchitecture.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public int? LastModifiedBy { get; set; }
}
