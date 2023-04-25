// Libraries
import { useEffect, useState } from 'react';
import { Button, Table } from 'react-bootstrap';
import { Link } from 'react-router-dom';

import { deleteProductById, getProductsByQueries, toggleProductActivedStatus } from '../../../Services/products';

import Pager from '../../../Components/store/Pager';
import Loading from '../../../Components/store/Loading';
import ProductFilterPane from '../../../Components/admin/ProductFilterPane';

export default function Products() {
    // Component's states
    const [pageNumber, setPageNumber] = useState(1);
    const [products, setProducts] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [metadata, setMetadata] = useState({});
    const [keyword, setKeyword] = useState('');
    const [authorId, setAuthorId] = useState();
    const [categoryId, setCategoryId] = useState();
    const [unitTag, setUnitTag] = useState();
    const [year, setYear] = useState();
    const [month, setMonth] = useState();
    const [actived, setActived] = useState(false);
    const [isChangeStatus, setIsChangeStatus] = useState(false);

    // Component's event handlers
    const handleChangePage = (value) => {
        setPageNumber((current) => current + value);
        window.scroll(0, 0);
    };
    const handleTogglePublishedStatus = async (e, id) => {
        await toggleProductActivedStatus(id);
        setIsChangeStatus(!isChangeStatus);
    };
    const handleDeleteproduct = async (e, id) => {
        if (window.confirm('Bạn có chắc muốn xóa sản phẩm?')) {
            const data = await deleteProductById(id);
            if (data.isSuccess) alert(data.result);
            else alert(data.errors[0]);
            setIsChangeStatus(!isChangeStatus);
        }
    };

    useEffect(() => {
        document.title = 'Danh sách sản phẩm';
        fetchProducts();

        async function fetchProducts() {
            const queries = new URLSearchParams({
                Actived: false,
                PageNumber: pageNumber || 1,
                PageSize: 10,
            });
            keyword && queries.append('Keyword', keyword);
            authorId && queries.append('AuthorId', authorId);
            categoryId && queries.append('CategoryId', categoryId);
            unitTag && queries.append('UnitTag', unitTag);
            year && queries.append('productedYear', year);
            month && queries.append('productedMonth', month);

            const data = await getProductsByQueries(queries);
            if (data) {
                setProducts(data.items);
                setMetadata(data.metadata);
            } else {
                setProducts([]);
                setMetadata({});
            }
            setIsLoading(false);
        }
    }, [pageNumber, keyword, authorId, categoryId, unitTag, year, month, actived, isChangeStatus]);

    return (
        <div className="mb-5">
            <div className="text">Danh sách sản phẩm</div>
            <ProductFilterPane
                setKeyword={setKeyword}
                setAuthorId={setAuthorId}
                setCategoryId={setCategoryId}
                setUnitTag={setUnitTag}
                setYear={setYear}
                setMonth={setMonth}
                setActived={setActived}
            />
            {isLoading ? (
                <Loading />
            ) : (
                <>
                    <Table striped responsive bordered>
                        <thead>
                            <tr>
                                <th>Tiêu đề</th>
                                <th>Loại</th>
                                <th>Còn hàng</th>
                                <th>Xóa</th>
                            </tr>
                        </thead>
                        <tbody>
                            {products.length > 0 ? (
                                products.map((product) => {
                                    return (
                                        <tr key={product.id}>
                                            <td>
                                                <Link to={`/admin/products/edit/${product.id}`} className="text-bold">
                                                    {product.name}
                                                </Link>
                                                {/* <p className="text-muted">{product.shortDescription}</p> */}
                                            </td>
                                            <td>
                                                {product.unitDetails.map((item, index) => {
                                                    return (
                                                        <Link
                                                            to={`/admin/products/edit/${product.id}/units/${item.id}`}>
                                                            <span key={index} className="btn btn-outline-primary mx-2">
                                                                {item.unitTag}
                                                            </span>
                                                        </Link>
                                                    );
                                                })}
                                                <Link
                                                    to={`/admin/products/edit/${product.id}/units`}
                                                    className="btn btn-success float-end">
                                                    +
                                                </Link>
                                            </td>
                                            {/* <td>
                                                {product.unitDetails.map((item, index) => {
                                                    return (
                                                        <span style={{ display: 'block' }} key={index}>
                                                            {item.price}
                                                        </span>
                                                    );
                                                })}
                                            </td> */}
                                            <td className="d-flex justify-content-center">
                                                {product.actived ? (
                                                    <Button
                                                        variant="primary"
                                                        onClick={(e) => handleTogglePublishedStatus(e, product.id)}>
                                                        Có
                                                    </Button>
                                                ) : (
                                                    <Button
                                                        variant="secondary"
                                                        onClick={(e) => handleTogglePublishedStatus(e, product.id)}>
                                                        Không
                                                    </Button>
                                                )}
                                            </td>
                                            <td className="text-center">
                                                <button
                                                    class="btn btn-danger"
                                                    onClick={(e) => handleDeleteproduct(e, product.id)}>
                                                    Xóa
                                                </button>
                                            </td>
                                        </tr>
                                    );
                                })
                            ) : (
                                <tr>
                                    <td colSpan={5}>
                                        <h4 className="text-center text-danger">Không tìm thấy bài viết</h4>
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </Table>
                    <Pager metadata={metadata} onPageChange={handleChangePage} />
                </>
            )}
        </div>
    );
}
