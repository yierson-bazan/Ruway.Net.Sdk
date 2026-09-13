namespace Ruway.Net.Core.Abstractions.Models.Pager;


public sealed class Pager<TData>
{
    /// <summary>
    /// Gets the total number of items.
    /// </summary>
    public int TotalItems { get; }

    /// <summary>
    /// Gets the current page.
    /// </summary>
    public int CurrentPage { get; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Gets the start page.
    /// </summary>
    public int StartPage { get; }

    /// <summary>
    /// Gets the end page.
    /// </summary>
    public int EndPage { get; }

    /// <summary>
    /// Gets the start item index.
    /// </summary>
    public int StartIndex { get; }

    /// <summary>
    /// Gets the end item index.
    /// </summary>
    public int EndIndex { get; }

    /// <summary>
    /// Gets the available page numbers.
    /// </summary>
    public IReadOnlyCollection<int> Pages { get; }

    /// <summary>
    /// Gets the data for the current page.
    /// </summary>
    public IReadOnlyCollection<TData> Data { get; }

    /// <summary>
    /// Creates a new instance of Pager.
    /// </summary>
    /// <param name="totalItems">Total number of items.</param>
    /// <param name="data">Data for the current page.</param>
    /// <param name="currentPage">Current page. Default is 1.</param>
    /// <param name="pageSize">Number of records per page. Default is 10.</param>
    /// <param name="maxPages">Maximum number of page numbers to display. Default is 10.</param>
    public Pager(
        int totalItems,
        IReadOnlyCollection<TData> data,
        int currentPage = 1,
        int pageSize = 10,
        int maxPages = 10)
    {
        if (totalItems < 0)
            throw new ArgumentOutOfRangeException(
                nameof(totalItems),
                "Total items cannot be negative.");

        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(pageSize),
                "Page size must be greater than zero.");

        if (maxPages <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(maxPages),
                "Maximum pages must be greater than zero.");

        ArgumentNullException.ThrowIfNull(data);

        TotalItems = totalItems;
        PageSize = pageSize;

        TotalPages = totalItems == 0
            ? 0
            : (int)Math.Ceiling((double)totalItems / pageSize);

        CurrentPage = TotalPages == 0
            ? 1
            : Math.Clamp(currentPage, 1, TotalPages);

        if (TotalPages == 0)
        {
            StartPage = 0;
            EndPage = 0;
            StartIndex = 0;
            EndIndex = -1;
            Pages = Array.Empty<int>();
            Data = data;

            return;
        }

        (StartPage, EndPage) = CalculatePageRange(
            CurrentPage,
            TotalPages,
            maxPages);

        StartIndex = (CurrentPage - 1) * PageSize;

        EndIndex = Math.Min(
            StartIndex + PageSize - 1,
            TotalItems - 1);

        Pages = Enumerable
            .Range(StartPage, EndPage - StartPage + 1)
            .ToArray();

        Data = data;
    }

    private static (int StartPage, int EndPage) CalculatePageRange(
        int currentPage,
        int totalPages,
        int maxPages)
    {
        if (totalPages <= maxPages)
            return (1, totalPages);

        var maxPagesBeforeCurrentPage = maxPages / 2;
        var maxPagesAfterCurrentPage = maxPages - maxPagesBeforeCurrentPage - 1;

        if (currentPage <= maxPagesBeforeCurrentPage + 1)
            return (1, maxPages);

        if (currentPage + maxPagesAfterCurrentPage >= totalPages)
            return (totalPages - maxPages + 1, totalPages);

        return (
            currentPage - maxPagesBeforeCurrentPage,
            currentPage + maxPagesAfterCurrentPage);
    }
}