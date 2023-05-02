import { Col, Container, Row } from 'react-bootstrap';
import TopPage from '../../Components/store/TopPage';
import CategorySidebar from '../../Components/store/CategorySidebar';

function Shop() {
    return (
        <div>
            <TopPage title="SẢN PHẨM" />

            <Container className="mt-4 mb-4">
                <Row>
                    <Col xs={3}>
                        <CategorySidebar />
                    </Col>
                    <Col xs={9}>2 of 3 (wider)</Col>
                </Row>
            </Container>
        </div>
    );
}

export default Shop;
