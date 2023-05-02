import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import styles from './CategorySidebar.module.scss';
import { getCategories } from '../../../Services/categories';

function CategorySidebar() {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        getCategories().then((res) => {
            if (res.items && res.items.length > 0)
                setCategories(res.items);
        });
    }, []);

    console.log(categories);

    return (
        <Form>
            <h4 className={styles.heading}>DANH MỤC SẢN PHẨM</h4>
            <div className={styles.list}>
                <div className={styles.item}>
                    <Form.Check type="radio" label={`category`} id={1} />
                </div>
                <div className={styles.item}>
                    <Form.Check type="radio" label={`category`} id={1} />
                </div>
                <div className={styles.item}>
                    <Form.Check type="radio" label={`category`} id={1} />
                </div>
                <div className={styles.item}>
                    <Form.Check type="radio" label={`category`} id={1} />
                </div>
            </div>
        </Form>
    );
}

export default CategorySidebar;
