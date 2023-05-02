import { useEffect, useState } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import TopPage from '../../Components/store/TopPage';
import CategorySidebar from '../../Components/store/CategorySidebar';
import ProductInShop from '../../Components/store/ProductInShop';
import { getProductsByQueries } from '../../Services/products';

function Shop() {
    const [searchParams, setSearchParams] = useState({
        PageSize: 10,
        PageNumber: 1,
        Actived: true,
    });
    const [products, setProducts] = useState([]);

    useEffect(() => {
        const params = new URLSearchParams(searchParams);
        getProductsByQueries(params).then((res) => {
            if (res && res.items) setProducts(res.items);
        });
    }, [searchParams]);

    const handleFilterByCategory = (categoryId) => {
        setSearchParams((prevState) => ({
            ...prevState,
            CategoryId: categoryId,
        }));
    };

    return (
        <>
            <TopPage title="SẢN PHẨM" />

            <Container className="mt-4 mb-4">
                <Row>
                    <Col xs={3}>
                        <CategorySidebar onFilterByCategory={handleFilterByCategory} />
                    </Col>
                    <Col xs={9}>
                        <Row>
                            {products && products.length > 0 ? (
                                products.map((product) => (
                                    <Col key={product.id} xs={4} className='mb-2'>
                                        <ProductInShop product={product} />
                                    </Col>
                                ))
                            ) : (
                                <p>Không có sản phẩm</p>
                            )}
                        </Row>
                    </Col>
                </Row>
            </Container>
        </>
    );
}

export default Shop;
