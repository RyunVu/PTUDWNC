import React from 'react';
import { useLocation } from 'react-router-dom';

export function isEmptyOrSpaces(str) {
    return str === null || (typeof str === 'string' && str.match(/^ *$/) !== null);
}

export function getMonthName(monthNumber) {
    const month = [
        'January',
        'February',
        'March',
        'April',
        'May',
        'June',
        'July',
        'August',
        'September',
        'October',
        'November',
        'December',
    ];

    return month[monthNumber - 1];
}

export function useQuery() {
    const { search } = useLocation();
    return React.useMemo(() => new URLSearchParams(search), [search]);
}
