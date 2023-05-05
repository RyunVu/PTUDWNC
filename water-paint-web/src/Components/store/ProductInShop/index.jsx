import styles from './ProductInShop.module.scss';
// import NoImagePaint from '../../../assets/images/NoImagePaint.jpg';
import { useNavigate } from 'react-router-dom';

function ProductInShop({ product = {} }) {
    const navigate = useNavigate();

    return (
        <div className={styles.wrapper} onClick={() => navigate(`/store/${product.urlSlug}`)}>
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
            <h4 className={styles.name}>{product.name}</h4>
        </div>
    );
}

export default ProductInShop;
