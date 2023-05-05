import React, { useRef } from 'react';
import { Form } from 'react-bootstrap';

export default function ProductsFilter({ setKeyword }) {
    const keywordRef = useRef();

    // Component's event handlers
    const handleFilterPosts = (e) => {
        e.preventDefault();
        setKeyword(keywordRef.current.value);
    };

    return (
        <Form method="get" onSubmit={handleFilterPosts} className="gx-3 gy-2 align-items-center py-2">
            <Form.Group className="col-auto">
                <Form.Label className="visually-hidden">Từ khóa</Form.Label>
                <Form.Control ref={keywordRef} type="text" placeholder="Nhập từ khóa..." name="keyword" />
            </Form.Group>
            <Form.Group className="col-auto"></Form.Group>
        </Form>
    );
}
