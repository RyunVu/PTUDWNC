import UnitsList from './unitsList';
import Card from 'react-bootstrap/Card';
import { Link } from 'react-router-dom';
import { isEmptyOrSpaces } from '../../Utils/utils';

const ProductItem = ({ productItem }) => {
    let imageUrl = isEmptyOrSpaces(productItem.imageUrl)
        ? process.env.PUBLIC_URL + '/images/image_1.jpg'
        : `${productItem.imageUrl}`;

    let productDateString = new Date(productItem.postedDate).toDateString();

    let postedDate = new Date(productDateString.slice(3, 15));

    return (
        <article className="blog-entry mb-4">
            <Card>
                <div className="row g-0">
                    <div className="col-md-4">
                        <Card.Img variant="top" src={imageUrl} alt={productItem.name} />
                    </div>
                    <div className="col-md-8">
                        <Card.Body>
                            <Card.Title>{productItem.title}</Card.Title>
                            <Card.Text>
                                <small className="text-muted">Chủ đề:</small>
                                <Link
                                    to={`/store/category/${productItem.category.urlSlug}`}
                                    className="text-primary text-decoration-none m-1">
                                    {productItem.category.name}
                                </Link>
                            </Card.Text>
                            <Card.Text>{productItem.shortDescription}</Card.Text>
                            <div className="tag-list">
                                <UnitsList unitsList={productItem.unitsList} />
                            </div>
                            <div className="text-end">
                                <Link
                                    to={`/store/product/?year=${postedDate.getFullYear()}&month=${postedDate.getMonth()}&day=${postedDate.getDay()}&slug=${
                                        productItem.urlSlug
                                    }`}
                                    className="btn btn-primary"
                                    title={productItem.title}>
                                    Xem chi tiết
                                </Link>
                            </div>
                        </Card.Body>
                    </div>
                </div>
            </Card>
        </article>
    );
};

export default ProductItem;
