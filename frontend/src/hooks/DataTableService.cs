
using System;
using System.Collections.Generic;
using System.Linq;

namespace CollabSphere.Client.Hooks;

public class DataTableService
{
    public DataTableResult<T> Query<T>(
        IEnumerable<T> source,
        string? searchText = null,
        Func<T, string?>? searchSelector = null,
        Func<T, object?>? sortSelector = null,
        bool descending = false,
        int page = 1,
        int pageSize = 10,
        Func<T, bool>? filter = null)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = 10;

        IEnumerable<T> query = source;

        if (!string.IsNullOrWhiteSpace(searchText) &&
            searchSelector != null)
        {
            query = query.Where(item =>
                searchSelector(item)?.Contains(
                    searchText.Trim(),
                    StringComparison.OrdinalIgnoreCase
                ) == true
            );
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (sortSelector != null)
        {
            query = descending
                ? query.OrderByDescending(sortSelector)
                : query.OrderBy(sortSelector);
        }

        var totalItems = query.Count();

        var totalPages = (int)Math.Ceiling(
            (double)totalItems / pageSize
        );

        var items = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new DataTableResult<T>
        {
            Items = items,
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
    }

    public IEnumerable<T> Search<T>(
        IEnumerable<T> source,
        string? searchText,
        Func<T, string?> selector)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return source;

        return source.Where(item =>
            selector(item)?.Contains(
                searchText.Trim(),
                StringComparison.OrdinalIgnoreCase
            ) == true
        );
    }

    public IEnumerable<T> Sort<T>(
        IEnumerable<T> source,
        Func<T, object?> selector,
        bool descending = false)
    {
        return descending
            ? source.OrderByDescending(selector)
            : source.OrderBy(selector);
    }

    public IEnumerable<T> Filter<T>(
        IEnumerable<T> source,
        Func<T, bool> predicate)
    {
        return source.Where(predicate);
    }

    public List<T> Paginate<T>(
        IEnumerable<T> source,
        int page,
        int pageSize)
    {
        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = 10;

        return source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}

public class DataTableResult<T>
{
    public List<T> Items { get; set; } = new();

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;

    public bool HasNextPage => CurrentPage < TotalPages;
}

