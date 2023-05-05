import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import styles from './CategorySidebar.module.scss';
import { getCategories } from '../../../Services/categories';

function CategorySidebar({ onFilterByCategory }) {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        getCategories().then((res) => {
            if (res.items && res.items.length > 0) setCategories([{ id: -1, name: 'Tất cả' }, ...res.items]);
        });
    }, []);

    return (
        <Form>
            <h4 className={styles.heading}>DANH MỤC SẢN PHẨM</h4>
            <div className={styles.list}>
                {categories &&
                    categories.map((category) => (
                        <div key={category.id} className={styles.item}>
                            <Form.Check
                                name="category-select"
                                type="radio"
                                label={category.name}
                                id={category.id}
                                onChange={() => onFilterByCategory(category.id)}
                            />
                        </div>
                    ))}
            </div>
        </Form>
    );
}

export default CategorySidebar;
