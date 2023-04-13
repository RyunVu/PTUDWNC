import React, { useEffect, useState } from 'react';
import { getProductsByQueries } from '../../Services/products';

import ProductItem from './ProductItem';
import Pager from './Pager';

export default function ProductsFilter({ productQuery }) {
    const { keyword, year, month, unitTag, productSlug, categorySlug } = productQuery;

    const [pageNumber, setPageNumber] = useState(1);
    const [products, setProducts] = useState([]);
    const [metadata, setMetaData] = useState({});

    const handlePagedChange = (value) => {
        setPageNumber((current) => current + value);
    };

    useEffect(() => {
        fetchProducts();

        async function fetchProducts() {
            const queries = new URLSearchParams({
                Actived: true,
                PageNumber: pageNumber || 1,
                PageSize: 10,
            });

            categorySlug && queries.append('CategorySlug', categorySlug);
            productSlug && queries.append('ProductSlug', productSlug);
            unitTag && queries.append('UnitTag', unitTag);
            year && queries.append('PostedYear', year);
            month && queries.append('PostedMonth', month);
            keyword && queries.append('Keyword', keyword);

            const data = await getProductsByQueries(queries);
            if (data) {
                console.log(data);
                setProducts(data.items);
                setMetaData(data.metadata);
            } else {
                setProducts([]);
                setMetaData([]);
            }
        }
    }, [keyword, year, month, unitTag, productSlug, categorySlug, pageNumber]);

    return (
        <div>
            {products.map((product) => (
                <ProductItem key={product.id} product={product} />
            ))}
            <Pager metadata={metadata} onPageChange={handlePagedChange} />
        </div>
    );
}
