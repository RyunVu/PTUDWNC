// Libraries
import { useEffect, useState } from 'react';
import { Button, Form } from 'react-bootstrap';
import { Link, Navigate, useNavigate, useParams } from 'react-router-dom';

// App's features
import { isEmptyOrSpaces, decode, isInteger } from '../../../Utils/utils';
import { createProduct, getProductById, updateProduct } from '../../../Services/products';
import { getCategories } from '../../../Services/categories';

export default function ProductEdit() {
    // Hooks
    const navigate = useNavigate();

    const initialState = {
        id: 0,
        name: '',
        shortDescription: '',
        urlSlug: '',
        meta: '',
        actived: false,
        imageUrl: '',
        categoryId: 0,
        category: {},
    };

    const [categories, setCategories] = useState([]);
    const [product, setProduct] = useState(initialState);
    const [validated, setValidated] = useState(false);

    const { id } = useParams();

    // Component's event handlers
    const handleSubmit = async (e) => {
        e.preventDefault();

        if (e.currentTarget.checkValidity() === false) {
            e.stopPropagation();
            setValidated(true);
        } else {
            let isSuccess = true;
            if (id > 0) {
                const productData = {
                    id,
                    name: product.name,
                    shortDescription: product.shortDescription,
                    urlSlug: product.urlSlug,
                    meta: product.meta,
                    actived: product.actived,
                    imageUrl: product.imageUrl,
                    categoryId: product.category.id,
                };
                const data = await updateProduct(id, productData);
                if (!data.isSuccess) isSuccess = false;
            } else {
                const productData = {
                    id: 0,
                    name: product.name,
                    shortDescription: product.shortDescription,
                    urlSlug: product.urlSlug,
                    meta: product.meta,
                    actived: product.actived,
                    imageUrl: product.imageUrl,
                    categoryId: product.category.id,
                };
                const data = await createProduct(productData);
                if (!data.isSuccess) isSuccess = false;
            }
            if (isSuccess) alert('Đã lưu thành công!');
            else alert('Đã xảy ra lỗi!');
            navigate('/admin/products');
        }
    };

    useEffect(() => {
        document.title = 'Thêm/cập nhật sản phẩm';

        fetchCategories();
        fetchProduct();

        async function fetchCategories() {
            const data = await getCategories();
            if (data) setCategories(data.items);
            else setCategories([]);
        }
        async function fetchProduct() {
            const data = await getProductById(id);
            if (data)
                setProduct({
                    ...data,
                });
            else setProduct(initialState);
        }
        // eslint-disable-next-line
    }, [id]);

    if (id && !isInteger(id)) return <Navigate to="/400?redirectTo=/admin/products" />;

    return (
        <>
            <h1 className="px-4 py-3 text-danger">Thêm/cập nhật sản phẩm</h1>
            <Form className="mb-5 px-4" onSubmit={handleSubmit} noValidate validated={validated}>
                <Form.Control type="hidden" name="id" value={product.id} />
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Tiêu đề</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="text"
                            name="name"
                            required
                            value={product.name || ''}
                            onChange={(e) =>
                                setProduct({
                                    ...product,
                                    name: e.target.value,
                                })
                            }
                        />
                        <Form.Control.Feedback type="invalid">Không được bỏ trống</Form.Control.Feedback>
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Slug</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="text"
                            name="urlSlug"
                            title="Url slug"
                            value={product.urlSlug || ''}
                            onChange={(e) =>
                                setProduct({
                                    ...product,
                                    urlSlug: e.target.value,
                                })
                            }
                            required
                        />
                        <Form.Control.Feedback type="invalid">Không được bỏ trống</Form.Control.Feedback>
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Giới thiệu</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            as="textarea"
                            type="text"
                            rows={'10'}
                            required
                            name="shortDescription"
                            title="Short description"
                            value={decode(product.shortDescription || '')}
                            onChange={(e) =>
                                setProduct({
                                    ...product,
                                    shortDescription: e.target.value,
                                })
                            }
                        />
                        <Form.Control.Feedback type="invalid">Không được bỏ trống</Form.Control.Feedback>
                    </div>
                </div>

                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Metadata</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="text"
                            name="meta"
                            title="meta"
                            value={decode(product.meta || '')}
                            onChange={(e) =>
                                setProduct({
                                    ...product,
                                    metadata: e.target.value,
                                })
                            }
                        />
                    </div>
                </div>

                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Loại sản phẩm</Form.Label>
                    <div className="col-sm-10">
                        <Form.Select
                            name="categoryId"
                            title="Category Id"
                            value={product.category.id}
                            required
                            onChange={(e) =>
                                setProduct({
                                    ...product,
                                    categoryId: e.target.value,
                                })
                            }>
                            <option value="">-- Chọn chủ đề --</option>
                            {categories.length > 0 &&
                                categories.map((category) => (
                                    <option key={category.id} value={category.id}>
                                        {category.name}
                                    </option>
                                ))}
                        </Form.Select>
                        <Form.Control.Feedback type="invalid">Không được bỏ trống</Form.Control.Feedback>
                    </div>
                </div>

                {!isEmptyOrSpaces(product.imageUrl) && (
                    <div className="row mb-3">
                        <Form.Label className="col-sm-2 col-form-label">Hình hiện tại</Form.Label>
                        <div className="col-sm-10">
                            <img src={process.env.REACT_APP_API_ROOT_URL + product.imageUrl} alt={product.title} />
                        </div>
                    </div>
                )}
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Chọn hình ảnh</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="file"
                            name="imageFile"
                            accept="image/*"
                            title="Image file"
                            onChange={(e) => {
                                setProduct({
                                    ...product,
                                    imageFile: e.target.files[0],
                                });
                            }}
                        />
                    </div>
                </div>
                <div className="row mb-3">
                    <div className="col-sm-10 offset-sm-2">
                        <div className="form-check">
                            <input
                                className="form-check-input"
                                type="checkbox"
                                name="actived"
                                checked={product.actived}
                                title="Actived"
                                onChange={(e) => {
                                    setProduct({
                                        ...product,
                                        actived: e.target.checked,
                                    });
                                }}
                            />
                            <Form.Label className="form-check-label">Còn hàng</Form.Label>
                        </div>
                    </div>
                </div>
                <div className="text-center">
                    <Button variant="primary" type="submit">
                        Lưu các thay đổi
                    </Button>
                    <Link to="/admin/products" className="btn btn-danger ms-2">
                        Hủy và quay lại
                    </Link>
                </div>
            </Form>
        </>
    );
}
