namespace Catalog.Core.Common;

public class CommonSpecParams
{
    public int MaxPageSize { get; set; }
    public int PageIndex { get; set; }
    private int _pageSize { get; set; }

    public int PageSize
    {
        get=>_pageSize;
        set=>_pageSize=value > MaxPageSize ? MaxPageSize: value;
    }
    public string Sort { get; set; }
    public string Search { get; set; }
}
    