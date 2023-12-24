﻿namespace FluentCMS.Entities;

public class Page : SiteAssociatedEntity
{
    public string Title { get; set; } = string.Empty;
    public Guid? ParentId { get; set; }
    public int Order { get; set; }
    public string Path { get; set; } = string.Empty;
    public Guid? LayoutId { get; set; }
}
