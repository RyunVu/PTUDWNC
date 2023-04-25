// Libraries
import { useEffect, useState } from 'react';
import { Button, Form } from 'react-bootstrap';
import { Link, Navigate, useNavigate, useParams } from 'react-router-dom';

// App's features
import { decode, isInteger } from '../../../Utils/utils';
import { createUnit, getUnitById, updateUnit, deleteUnitById } from '../../../Services/units';

const units = ['4kg', '5kg', '10kg', '20kg'];

export default function ProductEdit() {
    // Hooks
    const navigate = useNavigate();

    const initialState = {
        id: 0,
        unitTag: '',
        price: 0,
        quantity: 0,
        discount: 0,
        soldCount: 0,
        actived: false,
        productId: 0,
    };

    const [unit, setUnit] = useState(initialState);
    const [validated, setValidated] = useState(false);
    const { productId } = useParams();
    const { id } = useParams();

    const handleSubmit = async (e) => {
        e.preventDefault();

        const decodedProductId = decodeURIComponent(productId);
        const numericDecodedProductId = decodedProductId.slice(0, -1);

        if (e.currentTarget.checkValidity() === false) {
            e.stopPropagation();
            setValidated(true);
        } else {
            let isSuccess = true;
            if (id > 0) {
                const unitData = {
                    id,
                    unitTag: unit.unitTag,
                    price: unit.price,
                    quantity: unit.quantity,
                    discount: unit.discount,
                    soldCount: unit.soldCount,
                    actived: unit.actived,
                    productId: unit.productId,
                };
                const data = await updateUnit(id, unitData);
                if (!data.isSuccess) isSuccess = false;
            } else {
                const unitData = {
                    id: 0,
                    unitTag: unit.unitTag,
                    price: unit.price,
                    quantity: unit.quantity,
                    discount: unit.discount,
                    soldCount: unit.soldCount,
                    actived: unit.actived,
                    productId: numericDecodedProductId,
                };

                const data = await createUnit(unitData);
                if (!data.isSuccess) isSuccess = false;
            }
            if (isSuccess) {
                alert('Đã lưu thành công!');
                navigate('/admin/products');
            } else alert('Đã xảy ra lỗi!');
        }
    };

    const handleDeleteUnit = async (e) => {
        if (window.confirm('Bạn có chắc muốn xóa sản phẩm?')) {
            const data = await deleteUnitById(id);
            if (data.isSuccess) {
                alert(data.result);
                navigate('/admin/products');
            } else alert(data.errors[0]);
        }
    };

    useEffect(() => {
        document.title = 'Thêm/cập nhật sản phẩm';

        fetchUnit();

        async function fetchUnit() {
            const data = await getUnitById(id);
            if (data)
                setUnit({
                    ...data,
                });
            else setUnit(initialState);
        }
        // eslint-disable-next-line
    }, [id]);

    if (id && !isInteger(id)) return <Navigate to="/400?redirectTo=/admin/products" />;

    return (
        <>
            <h1 className="px-4 py-3 text-danger">Thêm/cập nhật loại sản phẩm</h1>
            <Form className="mb-5 px-4" onSubmit={handleSubmit} noValidate validated={validated}>
                <Form.Control type="hidden" name="id" value={unit.id} />
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Tên loại</Form.Label>
                    <div className="col-sm-10">
                        <Form.Select
                            name="unitTag"
                            title="Unit Tag"
                            value={unit.unitTag}
                            required
                            onChange={(e) =>
                                setUnit({
                                    ...unit,
                                    unitTag: e.target.value,
                                })
                            }>
                            <option value="">-- Chọn loại --</option>
                            {units.length > 0 &&
                                units.map((unit) => (
                                    <option key={unit.unitTag} value={unit.unitTag}>
                                        {unit}
                                    </option>
                                ))}
                        </Form.Select>
                        <Form.Control.Feedback type="invalid">Không được bỏ trống</Form.Control.Feedback>
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Giá</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="number"
                            name="price"
                            title="Price"
                            value={unit.price || 0}
                            onChange={(e) =>
                                setUnit({
                                    ...unit,
                                    price: e.target.value,
                                })
                            }
                            required
                        />
                        <Form.Control.Feedback type="invalid">Không được bỏ trống</Form.Control.Feedback>
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Số lượng tồn</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="number"
                            name="quantity"
                            title="Quantity"
                            value={unit.quantity || 0}
                            onChange={(e) =>
                                setUnit({
                                    ...unit,
                                    quantity: e.target.value,
                                })
                            }
                            required
                        />
                        <Form.Control.Feedback type="invalid">Không được bỏ trống</Form.Control.Feedback>
                    </div>
                </div>

                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Giảm giá</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="number"
                            name="discount"
                            title="Discount"
                            value={decode(unit.discount || 0)}
                            onChange={(e) =>
                                setUnit({
                                    ...unit,
                                    discount: e.target.value,
                                })
                            }
                        />
                    </div>
                </div>

                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Số lượng đã bán</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            disabled
                            name="soldCount"
                            title="Sold Count"
                            value={unit.soldCount}
                            required
                            onChange={(e) =>
                                setUnit({
                                    ...unit,
                                    soldCount: e.target.value,
                                })
                            }></Form.Control>
                        <Form.Control.Feedback type="invalid">Không được bỏ trống</Form.Control.Feedback>
                    </div>
                </div>

                <div className="row mb-3">
                    <div className="col-sm-10 offset-sm-2">
                        <div className="form-check">
                            <input
                                className="form-check-input"
                                type="checkbox"
                                name="actived"
                                checked={unit.actived}
                                title="Actived"
                                onChange={(e) => {
                                    setUnit({
                                        ...unit,
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
                    <Link to="/admin/products" className="btn btn-warning mx-2">
                        Hủy và quay lại
                    </Link>
                    <Button variant="danger" onClick={handleDeleteUnit}>
                        Xóa
                    </Button>
                </div>
            </Form>
        </>
    );
}
