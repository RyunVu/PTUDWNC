import { useEffect, useState } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import TopPage from '../../Components/store/TopPage';
import CategorySidebar from '../../Components/store/CategorySidebar';
import ProductInShop from '../../Components/store/ProductInShop';
import { getProductsByQueries } from '../../Services/products';

function Shop() {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        const params = new URLSearchParams({ PageSize: 10, PageNumber: 1, Actived: true });
        getProductsByQueries(params).then((res) => {
            if (res && res.items) setProducts(res.items);
        });
    }, []);

    return (
        <div>
            <TopPage title="SẢN PHẨM" />

            <Container className="mt-4 mb-4">
                <Row>
                    <Col xs={3}>
                        <CategorySidebar />
                    </Col>
                    <Col xs={9}>
                        <Row>
                            {products &&
                                products.map((product) => (
                                    <Col key={product.id} xs={4}>
                                        <ProductInShop product={product} />
                                    </Col>
                                ))}
                        </Row>
                    </Col>
                </Row>
            </Container>
        </div>
    );
}

export default Shop;
