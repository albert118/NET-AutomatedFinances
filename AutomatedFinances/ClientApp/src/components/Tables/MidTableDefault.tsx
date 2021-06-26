import {
    TableWrapper, StyledTable, StyledTableHeader, StyledTableRow,
    StyledTableDataFirstCell, StyledTableHeaderFirstCell,
    StyledTableDataCell, StyledTableHeaderCell,
    StyledTableBody
} from './CommonStyledComponents/ModernBlackAndWhiteTable';
import { Column, useTable, useFilters, useAsyncDebounce } from 'react-table';
import * as React from 'react';
import { useMemo, useState } from 'react';
import { matchSorter } from 'match-sorter';

const FILTER_RETRY_DELAY_MS = 500;

type ColumnDefinitiontype<T, K extends keyof T> = {
    Header: string;
    accessor: K;
    width?: number;
    Cell?: string;
    Filter: Function;
    filter: string;
}

type TableProps<T, K extends keyof T> = {
    data: Array<T>;
    columns: Column<ColumnDefinitiontype<T, K>>[];
};

/**
 * Define a default UI for filtering
 * Note: useState(filterValue) logic along with useAsyncDebounce allow the user to interact
 *       with the UI filter seamlessly. Else, the UI mucks about rerendering every keystroke
 */
function DefaultColumnFilter({ column: { filterValue, preFilteredRows, setFilter }, }): JSX.Element {
    const count = preFilteredRows.length
    const [value, setValue] = useState(filterValue)
    const defaultVal = undefined; // Set undefined to remove the filter entirely
    const onChange = useAsyncDebounce((value: any) => { setFilter(value || defaultVal) }, FILTER_RETRY_DELAY_MS)
    
    return (
        <input style={{ background: "white"}}
            value={value || ''}
            onChange={e => {
                setValue(e.target.value);
                onChange(e.target.value);
            }}
            placeholder={`Search ${count} records...`}
        />
    );
}

/**
 * This is a custom filter UI for selecting a unique option from a list.
 */

export function SelectColumnFilter({ column: { filterValue, preFilteredRows, setFilter, id }, }): JSX.Element {
    const defaultVal = undefined; // Set undefined to remove the filter entirely

    // determine the selectable options for the dropdown
    const options = useMemo(() => {
        const options = new Set();
        preFilteredRows.forEach((row: any) => {
            options.add(row[id].values)
        });

        return [...options.values()];
    }, [id, preFilteredRows]);

    return (
        <select
            value={filterValue}
            onChange={e => {
                setFilter(e.target.value || defaultVal);
            }}
        >
            <option value="">All</option>
            {options.map((option, i) => (
                <option key={i} value={option}>
                    {option}
                </option>
            ))}
        </select>
    );
}

function fuzzyTextFilterFn(rows: string[], id: any, filterValue: string) {
    return matchSorter(rows, filterValue, { keys: [row => Object.values(row.original)] });
}

// Let the table remove the filter if the string is empty
fuzzyTextFilterFn.autoRemove = (val: any) => !val

/**
 * Create a new Mid-szied data table with pre-configured styling. Just drop in an aync handle to load data
 * @param {props} TableProps data to configure and build the table.
 * @public
 */
export default function MidTableDefault<T, K extends keyof T>({ data, columns }: TableProps<T, K>): JSX.Element {
    const filterTypes = useMemo(() => ({
        fuzzyText: fuzzyTextFilterFn,
    }), []);

    const defaultColumn = React.useMemo(
        () => ({
            // adds the default UI filter option for columns.
            Filter: DefaultColumnFilter,
        }),
        []
    );

    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        rows,
        prepareRow,
    } = useTable({
        data,
        columns,
        defaultColumn, // required!!
        filterTypes,
    }, 
        useFilters
    );

    return (
        <TableWrapper>
            <StyledTable {...getTableProps()}>
                <StyledTableHeader>
                    {headerGroups.map(headerGroup => (
                        <StyledTableRow { ...headerGroup.getHeaderGroupProps() }>
                            {headerGroup.headers.map((column, i) => {

                                if (i === 0) {
                                    return (
                                        <StyledTableHeaderFirstCell {...column.getHeaderProps()}>
                                            {column.render('Header')}
                                            <div>{column.filter ? column.render('Filter') : null}</div>
                                        </StyledTableHeaderFirstCell>
                                    )
                                } else {
                                    return (
                                        <StyledTableHeaderCell {...column.getHeaderProps()}>
                                            {column.render('Header')}
                                            <div>{column.filter ? column.render('Filter') : null}</div>
                                        </StyledTableHeaderCell>
                                    )
                                }
                            })}
                        </StyledTableRow>
                    ))}
                </StyledTableHeader>
                <StyledTableBody {...getTableBodyProps()}>
                    {rows.map(row => {
                        prepareRow(row)
                        return (
                            <StyledTableRow { ...row.getRowProps() }>
                                {row.cells.map((cell, i) => {
                                    if (i === 0) {
                                        return (
                                            <StyledTableDataFirstCell {...cell.getCellProps()}>
                                                {cell.render('Cell')}
                                            </StyledTableDataFirstCell>
                                        )
                                    } else {
                                        return (

                                            <StyledTableDataCell {...cell.getCellProps()}>
                                                {cell.render('Cell')}
                                            </StyledTableDataCell>
                                        )
                                    }
                                })}
                            </StyledTableRow>
                        )
                    })}
                </StyledTableBody>
            </StyledTable>
        </TableWrapper>
    )
}