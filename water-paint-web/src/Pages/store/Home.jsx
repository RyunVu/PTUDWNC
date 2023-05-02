import { useEffect, useState } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import Slider from '../../Components/store/Slider';
import { getProductsByQueries } from '../../Services/products';
import ProductInShop from '../../Components/store/ProductInShop';

function Home() {
    const [searchParams, setSearchParams] = useState({
        PageSize: 12,
        PageNumber: 1,
        Actived: true,
        SortOrder: 'DESC',
    });
    const [products, setProducts] = useState([]);

    useEffect(() => {
        const params = new URLSearchParams(searchParams);
        getProductsByQueries(params).then((res) => {
            if (res && res.items) setProducts(res.items);
        });
    }, [searchParams]);

    return (
        <div>
            <Slider />

            <h4 className='mt-5' style={ {
                textAlign: 'center',
                fontWeight: 'bold',
                color: 'red'
            }}>SẢN PHẨM MỚI NHẤT</h4>
            <Container className="mt-4 mb-4">
                <Row>
                    {products && products.length > 0 ? (
                        products.map((product) => (
                            <Col key={product.id} xs={4} className="mb-2">
                                <ProductInShop product={product} />
                            </Col>
                        ))
                    ) : (
                        <p>Không có sản phẩm</p>
                    )}
                </Row>
            </Container>
        </div>
    );
}

export default Home;
