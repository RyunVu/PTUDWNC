import { useEffect, useRef, useState } from 'react';
import { Button, Form } from 'react-bootstrap';
import { Link } from 'react-router-dom';

import { getCategories } from '../../Services/categories';

const months = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
const units = ['4kg', '5kg', '10kg', '20kg'];

export default function ProductFilterPane({ setKeyword, setCategoryId, setUnitTag, setYear, setMonth, setActived }) {
    const [categories, setCategories] = useState([]);

    // Component's refs
    const keywordRef = useRef();
    const categoryRef = useRef();
    const unitTagRef = useRef();
    const yearRef = useRef();
    const monthRef = useRef();
    const activedRef = useRef();

    // Component's event handlers
    const handleFilterPosts = (e) => {
        e.preventDefault();
        setKeyword(keywordRef.current.value);
        setCategoryId(categoryRef.current.value);
        setUnitTag(unitTagRef.current.value);
        setYear(yearRef.current.value);
        setMonth(monthRef.current.value);
        setActived(activedRef.current.checked);
    };

    const handleClearFilter = () => {
        setKeyword('');
        setCategoryId('');
        setUnitTag('');
        setYear('');
        setMonth('');
        setActived(false);
        keywordRef.current.value = '';
        categoryRef.current.value = '';
        unitTagRef.current.value = '';
        yearRef.current.value = '';
        monthRef.current.value = '';
        activedRef.current.checked = false;
    };

    useEffect(() => {
        fetchData();

        async function fetchData() {
            const categories = await getCategories();
            if (categories) setCategories(categories.items);
        }
    }, []);

    return (
        <Form method="get" onSubmit={handleFilterPosts} className="row gx-3 gy-2 align-items-center py-2">
            <Form.Group className="col-auto">
                <Form.Label className="visually-hidden">Từ khóa</Form.Label>
                <Form.Control ref={keywordRef} type="text" placeholder="Nhập từ khóa..." name="keyword" />
            </Form.Group>

            <Form.Group className="col-auto">
                <Form.Label className="visually-hidden">Loại sản phẩm</Form.Label>
                <Form.Select ref={categoryRef} title="Chủ đề" name="categoryId">
                    <option value="">-- Chọn loại sản phẩm --</option>
                    {categories.map((category) => (
                        <option key={category.id} value={category.id}>
                            {category.name}
                        </option>
                    ))}
                </Form.Select>
            </Form.Group>
            <Form.Group className="col-auto">
                <Form.Label className="visually-hidden">Loại</Form.Label>
                <Form.Select ref={unitTagRef} title="Loại" name="unitTag">
                    <option value="">-- Chọn loại --</option>
                    {units.map((unit) => (
                        <option key={unit} value={unit}>
                            {unit}
                        </option>
                    ))}
                </Form.Select>
            </Form.Group>
            <Form.Group className="col-auto">
                <Form.Label className="visually-hidden">Nhập năm</Form.Label>
                <Form.Control ref={yearRef} type="text" placeholder="Nhập năm..." name="year" />
            </Form.Group>
            <Form.Group className="col-auto">
                <Form.Label className="visually-hidden">Tháng</Form.Label>
                <Form.Select ref={monthRef} title="Tháng" name="month">
                    <option value="">-- Chọn tháng --</option>
                    {months.map((month) => (
                        <option key={month} value={month}>
                            Tháng {month}
                        </option>
                    ))}
                </Form.Select>
            </Form.Group>
            <Form.Group className="col-auto">
                <input id="actived" type="checkbox" ref={activedRef} />
                <label htmlFor="actived" className="ms-1">
                    Còn hàng
                </label>
            </Form.Group>
            <Form.Group className="col-auto">
                <Button variant="primary" type="submit">
                    Tìm/Lọc
                </Button>
                <Button variant="warning mx-2" onClick={handleClearFilter}>
                    Bỏ lọc
                </Button>
                <Link to="/admin/products/edit" className="btn btn-success">
                    Thêm mới
                </Link>
            </Form.Group>
        </Form>
    );
}
