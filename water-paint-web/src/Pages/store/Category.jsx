import React from 'react';
import { useEffect } from 'react';
import { useLocation } from 'react-router-dom';

import ProductsFilter from '../../Components/store/ProductsFilter';

export default function Category() {
    const queryStrings = new URLSearchParams(useLocation().search);
    const keyword = queryStrings.get('keyword');

    useEffect(() => {
        document.title = 'Trang chủ';
    }, []);

    return (
        <div className="p-4">
            {keyword && <h1 className="mb-4">Kết quả tìm kiếm cho từ khóa: "{keyword}"</h1>}
            <ProductsFilter postQuery={{ keyword }} />
        </div>
    );
}
