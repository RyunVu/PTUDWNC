import UnitsList from './unitsList';
import Card from 'react-bootstrap/Card';
import { Link } from 'react-router-dom';
import { isEmptyOrSpaces } from '../../Utils/utils';

const ProductItem = ({ product = {} }) => {
    let imageUrl = isEmptyOrSpaces(product.imageUrl)
        ? process.env.PUBLIC_URL + '/images/image_1.jpg'
        : `${product.imageUrl}`;

    let productDateString = new Date(product.postedDate).toDateString();

    let postedDate = new Date(productDateString.slice(3, 15));

    return (
        <article className="blog-entry mb-4">
            <Card>
                <div className="row g-0">
                    <div className="col-md-4">
                        <Card.Img variant="top" src={imageUrl} alt={product.name} />
                    </div>
                    <div className="col-md-8">
                        <Card.Body>
                            <Card.Title>{product.title}</Card.Title>
                            <Card.Text>
                                <small className="text-muted">Chủ đề:</small>
                                <Link
                                    to={`/store/category/${product.category.urlSlug}`}
                                    className="text-primary text-decoration-none m-1">
                                    {product.category.name}
                                </Link>
                            </Card.Text>
                            <Card.Text>{product.shortDescription}</Card.Text>
                            <div className="tag-list">
                                <UnitsList unitsList={product.unitsList} />
                            </div>
                            <div className="text-end">
                                <Link
                                    to={`/store/product/?year=${postedDate.getFullYear()}&month=${postedDate.getMonth()}&day=${postedDate.getDay()}&slug=${
                                        product.urlSlug
                                    }`}
                                    className="btn btn-primary"
                                    title={product.title}>
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
