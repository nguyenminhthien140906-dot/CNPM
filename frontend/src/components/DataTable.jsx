
import React, { useMemo, useState, useEffect } from "react";
import "./DataTable.css";

const DataTable = ({
    items = [],
    headerTemplate,
    rowTemplate,
    searchSelector,
    searchPlaceholder = "Tìm kiếm dữ liệu...",
    filterOptions = [],
    filterSelector,
    filterPlaceholder = "Tất cả",
    extraToolbarContent,
    pageSize = 10,
    emptyMessage = "Không có dữ liệu."
}) => {
    const [searchText, setSearchText] = useState("");
    const [selectedFilter, setSelectedFilter] = useState("");
    const [currentPage, setCurrentPage] = useState(1);

    const safePageSize = Math.max(1, pageSize);

    const filteredItems = useMemo(() => {
        return items.filter(item => {
            const searchValue = searchSelector
                ? searchSelector(item)
                : "";

            const matchSearch =
                !searchText.trim() ||
                String(searchValue ?? "")
                    .toLowerCase()
                    .includes(searchText.trim().toLowerCase());

            const filterValue = filterSelector
                ? filterSelector(item)
                : "";

            const matchFilter =
                !selectedFilter ||
                !filterSelector ||
                String(filterValue ?? "").trim().toLowerCase() ===
                selectedFilter.trim().toLowerCase();

            return matchSearch && matchFilter;
        });
    }, [
        items,
        searchText,
        selectedFilter,
        searchSelector,
        filterSelector
    ]);

    const totalItems = filteredItems.length;

    const totalPages = Math.max(
        1,
        Math.ceil(totalItems / safePageSize)
    );

    useEffect(() => {
        setCurrentPage(1);
    }, [searchText, selectedFilter]);

    useEffect(() => {
        setCurrentPage(page =>
            Math.min(Math.max(1, page), totalPages)
        );
    }, [totalPages]);

    const pagedItems = useMemo(() => {
        const startIndex = (currentPage - 1) * safePageSize;

        return filteredItems.slice(
            startIndex,
            startIndex + safePageSize
        );
    }, [filteredItems, currentPage, safePageSize]);

    const previousPage = () => {
        setCurrentPage(page => Math.max(1, page - 1));
    };

    const nextPage = () => {
        setCurrentPage(page => Math.min(totalPages, page + 1));
    };

    return (
        <div className="data-table-container">

            <div className="table-toolbar">

                <div className="table-toolbar-left">

                    <div className="table-search">
                        <input
                            type="text"
                            placeholder={searchPlaceholder}
                            value={searchText}
                            onChange={event =>
                                setSearchText(event.target.value)
                            }
                        />
                    </div>

                    {filterOptions.length > 0 && filterSelector && (
                        <div className="table-filter">
                            <select
                                value={selectedFilter}
                                onChange={event =>
                                    setSelectedFilter(event.target.value)
                                }
                            >
                                <option value="">
                                    {filterPlaceholder}
                                </option>

                                {filterOptions.map((option, index) => (
                                    <option key={index} value={option}>
                                        {option}
                                    </option>
                                ))}
                            </select>
                        </div>
                    )}

                    {extraToolbarContent}

                </div>

                <div className="table-info">
                    Tổng số: {totalItems} bản ghi
                </div>

            </div>

            {filteredItems.length > 0 ? (
                <div className="table-responsive">

                    <table className="data-table">

                        <thead>
                            {headerTemplate}
                        </thead>

                        <tbody>
                            {pagedItems.map((item, index) => (
                                <tr key={item.id ?? item.userId ?? index}>
                                    {rowTemplate
                                        ? rowTemplate(item, index)
                                        : null}
                                </tr>
                            ))}
                        </tbody>

                    </table>

                </div>
            ) : (
                <div className="table-empty">
                    {emptyMessage}
                </div>
            )}

            <div className="table-pagination">

                <span>
                    Trang {currentPage} / {totalPages}
                </span>

                <div className="pagination-actions">

                    <button
                        disabled={currentPage <= 1}
                        onClick={previousPage}
                    >
                        Trước
                    </button>

                    <button
                        disabled={currentPage >= totalPages}
                        onClick={nextPage}
                    >
                        Tiếp
                    </button>

                </div>

            </div>

        </div>
    );
};

export default DataTable;
