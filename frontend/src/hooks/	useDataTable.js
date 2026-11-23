
import { useState, useMemo, useCallback } from "react";

export const useDataTable = (
    source = [],
    options = {}
) => {
    const {
        initialPage = 1,
        initialPageSize = 10,
        initialSearchText = "",
        initialDescending = false
    } = options;

    const [searchText, setSearchText] = useState(initialSearchText);
    const [currentPage, setCurrentPage] = useState(initialPage);
    const [pageSize, setPageSize] = useState(initialPageSize);
    const [sortField, setSortField] = useState(null);
    const [descending, setDescending] = useState(initialDescending);
    const [filters, setFilters] = useState([]);

    const safeSource = Array.isArray(source) ? source : [];

    const processedData = useMemo(() => {
        let result = [...safeSource];

        // SEARCH
        if (searchText.trim()) {
            const keyword = searchText.trim().toLowerCase();

            result = result.filter((item) =>
                Object.values(item).some((value) =>
                    String(value ?? "")
                        .toLowerCase()
                        .includes(keyword)
                )
            );
        }

        // FILTER
        filters.forEach((filter) => {
            result = result.filter(filter);
        });

        // SORT
        if (sortField) {
            result.sort((a, b) => {
                const valueA = a[sortField];
                const valueB = b[sortField];

                if (valueA == null) return 1;
                if (valueB == null) return -1;

                let comparison;

                if (
                    typeof valueA === "number" &&
                    typeof valueB === "number"
                ) {
                    comparison = valueA - valueB;
                } else {
                    comparison = String(valueA).localeCompare(
                        String(valueB),
                        "vi",
                        {
                            numeric: true,
                            sensitivity: "base"
                        }
                    );
                }

                return descending
                    ? -comparison
                    : comparison;
            });
        }

        return result;
    }, [
        safeSource,
        searchText,
        filters,
        sortField,
        descending
    ]);

    const totalItems = processedData.length;

    const totalPages = Math.ceil(totalItems / pageSize);

    const validPage = Math.min(
        currentPage,
        Math.max(totalPages, 1)
    );

    const items = useMemo(() => {
        const startIndex = (validPage - 1) * pageSize;

        return processedData.slice(
            startIndex,
            startIndex + pageSize
        );
    }, [processedData, validPage, pageSize]);

    const search = useCallback((text) => {
        setSearchText(text);
        setCurrentPage(1);
    }, []);

    const sort = useCallback((field) => {
        if (sortField === field) {
            setDescending((prev) => !prev);
        } else {
            setSortField(field);
            setDescending(false);
        }

        setCurrentPage(1);
    }, [sortField]);

    const filter = useCallback((predicate) => {
        if (typeof predicate !== "function") {
            return;
        }

        setFilters((prev) => [...prev, predicate]);
        setCurrentPage(1);
    }, []);

    const clearFilters = useCallback(() => {
        setFilters([]);
        setCurrentPage(1);
    }, []);

    const paginate = useCallback((page) => {
        setCurrentPage(Math.max(1, page));
    }, []);

    const changePageSize = useCallback((size) => {
        setPageSize(Math.max(1, size));
        setCurrentPage(1);
    }, []);

    const nextPage = useCallback(() => {
        setCurrentPage((prev) =>
            Math.min(prev + 1, Math.max(totalPages, 1))
        );
    }, [totalPages]);

    const previousPage = useCallback(() => {
        setCurrentPage((prev) => Math.max(prev - 1, 1));
    }, []);

    const reset = useCallback(() => {
        setSearchText("");
        setCurrentPage(1);
        setSortField(null);
        setDescending(false);
        setFilters([]);
    }, []);

    return {
        items,
        currentPage: validPage,
        pageSize,
        totalItems,
        totalPages,
        searchText,
        sortField,
        descending,

        hasPreviousPage: validPage > 1,
        hasNextPage: validPage < totalPages,

        search,
        sort,
        filter,
        clearFilters,
        paginate,
        changePageSize,
        nextPage,
        previousPage,
        reset
    };
};

export default useDataTable;
