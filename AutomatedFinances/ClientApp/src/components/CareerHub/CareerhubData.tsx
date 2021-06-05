import * as React from 'react';
import { useState, useEffect, useMemo } from 'react';
import MidTableDefault from '../Tables/MidTableDefault';
import { TableLoadingPlaceHolder } from '../Tables/CommonStyledComponents/ModernBlackAndWhiteTable';

type UTSJobListing = {
    positionTitle: string;
    summary: string;
    company: string;
    closingDate: Date;
    detailURL: URL;
    location: string;
}

// TODO: Extract to string extensions module
function truncate(input: string) {
    const truncLength: number = 18;
    const truncVal: String = '...';
    if (input.length > truncLength) {
        return input.substring(0, truncLength - truncVal.length) + truncVal;
    }
    return input;
};

function CustomSelectColumnFilter({ column: { filterValue, preFilteredRows, setFilter }, }): JSX.Element {
    const defaultVal = undefined; // Set undefined to remove the filter entirely

    // determine the selectable options for the dropdown
    const options: string[] = useMemo<string[]>(() => {
        const options = new Set();
        preFilteredRows.forEach((row: any) => {
            options.add(row.original.company.trim())
        });

        return [...options.values()];
    }, preFilteredRows);

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
                    {truncate(option)}
                </option>
            ))}
        </select>
    );
}

/* Overrides the react-table builtin includes filter. */
function CustomIncludes_override(rows: Array<any>, ids: Array<number>, filterValue: String) {
    return rows.filter((row: any) => {
        return ids.some(id => {
            const rowValue = row.original.company.trim()
            return rowValue.includes(filterValue);
        })
    });
}

// Let the table remove the filter if the string is empty
CustomIncludes_override.autoRemove = (val: any) => !val


export default function CareerHubData() {
    const [isLoading, setLoading] = useState(true);
    const [dataList, setDataList] = useState<UTSJobListing[]>([]);
    const loadingMsg = "Loading the Careerhub data...";

    async function populateCareerHubData() {
        const response = await fetch('/CareerHub');
        const data = await response.json();
        setDataList(data)
        setLoading(false);
    }

    useEffect(() => { populateCareerHubData(); }, []);

    const columns = useMemo(() => [
        {
            Header: 'Listing Oppertunity',
            accessor: 'positionTitle_summary',
            Cell: (data: { row: { original: { positionTitle: React.ReactNode; summary: any; }; }; }) => {
                return (
                    <div>
                        <b style={{ textTransform: "capitalize" }}>{ data.row.original.positionTitle }</b>
                        <br />
                        <br />
                        <span style={{ textTransform: "capitalize" }}>{ (data.row.original.summary).slice(0, 57) + '...' }</span>
                    </div>
                )
            },
            filter: 'fuzzyText',
        },
        {
            Header: 'Company',
            accessor: 'company_location',
            Cell: (data: { row: { original: { company: React.ReactNode; location: React.ReactNode; }; }; }) => {
                return (
                    <div>
                        <b style={{ textTransform: "capitalize" }}>{ data.row.original.company }</b>
                        <br />
                        <span style={{ textTransform: "capitalize" }}>{ data.row.original.location }</span>
                    </div>
                )
            },
            Filter: CustomSelectColumnFilter,
            filter: CustomIncludes_override,
        },
        {
            Header: 'Closing Date',
            accessor: 'closingDate',
        },
    ],
        []
    );

    const data = useMemo(() => dataList, [dataList]);

    if (isLoading) {
        return (
            <TableLoadingPlaceHolder LoadingMessage={loadingMsg} />
        )
    }

    return (<MidTableDefault columns={columns} data={data} />);
}