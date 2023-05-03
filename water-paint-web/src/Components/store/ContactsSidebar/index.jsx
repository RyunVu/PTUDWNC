import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import styles from './ContactsSidebar.module.scss';
import { getCategories } from '../../../Services/categories';

function ContactsSidebar({ onFilterByCategory }) {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        getCategories().then((res) => {
            if (res.items && res.items.length > 0) setCategories([{ id: -1, name: 'Tất cả' }, ...res.items]);
        });
    }, []);

    return (
        <Form>
            <h4 className={styles.heading}>LIÊN HỆ</h4>
            <ul className={styles.list}>
                <li>
                    <div>
                    VĂN PHÒNG CÔNG TY
                    </div>
                </li>
            </ul>
        </Form>
    );
}

export default ContactsSidebar;
