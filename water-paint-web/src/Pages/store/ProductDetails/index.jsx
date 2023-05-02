import { useEffect, useState } from 'react';
import { getProductsByQueries } from '../../../Services/products';
import { useParams } from 'react-router-dom';
import { Col, Container, Row } from 'react-bootstrap';
import NoImagePaint from '../../../assets/images/NoImagePaint.jpg';
import styles from './ProductDetails.module.scss';
import TopPage from '../../../Components/store/TopPage';

function ProductDetails() {
    const { slug } = useParams();

    const [product, setProduct] = useState({});
    const [unitSelected, setUnitSelected] = useState(null);

    useEffect(() => {
        const params = new URLSearchParams({ PageSize: 1, PageNumber: 1, Actived: true, ProductSlug: slug });
        getProductsByQueries(params).then((res) => {
            if (res && res.items[0]) {
                const product = res.items[0];
                setProduct(product);

                if (product.unitDetails && product.unitDetails.length > 0) {
                    setUnitSelected(product.unitDetails[0]);
                }
            }
        });
    }, [slug]);

    const getPriceByUnit = (unitId) => {
        if (product?.unitDetails) {
            return product.unitDetails.find((unit) => unit.id === unitId)?.price ?? 0;
        }

        return 0;
    };

    const handleChoiceWeight = (unitId) => {
        setUnitSelected(unitId);
    };

    return (
        <>
            <TopPage title="CHI TIẾT SẢN PHẨM" />

            <Container className="mt-4 mb-4">
                {product ? (
                    <Row>
                        <Col xs={6}>
                            <img
                                className={styles.image}
                                src={
                                    product.imageUrl
                                        ? product.imageUrl
                                        : // : NoImagePaint
                                          'https://cdn.nanoweb.vn/mediacenter/media/images/3005/products/3005/3596/s400_400/k871-v-1658472987.jpg'
                                }
                                alt="product"
                            />
                        </Col>
                        <Col xs={6}>
                            <div className={styles.info}>
                                <h5 className={styles.name}>{product.name}</h5>
                                <h5 className={styles.price}>
                                    Giá: {getPriceByUnit(unitSelected ? unitSelected.id : 0)} VNĐ
                                </h5>
                                <p className={styles.weight}>Khối lượng:</p>
                                <div className={styles.weightOptions}>
                                    {product.unitDetails && product.unitDetails.length > 0 ? (
                                        product.unitDetails.map((unit) => (
                                            <button
                                                key={unit.id}
                                                className={styles.weightOption}
                                                onClick={() => handleChoiceWeight(unit.id)}>
                                                {unit.unitTag}
                                            </button>
                                        ))
                                    ) : (
                                        <p className={styles.unitDetailsEmpty}>Không có khối lượng</p>
                                    )}
                                </div>
                            </div>
                        </Col>
                        <Col xs={12} className="mt-4">
                            <h5 className={styles.descriptionHeading}>Thông tin chung:</h5>
                            <p className={styles.description}>{product.shortDescription}</p>
                        </Col>
                    </Row>
                ) : (
                    <p>Không tìm thấy sản phẩm</p>
                )}
            </Container>
        </>
    );
}

export default ProductDetails;
