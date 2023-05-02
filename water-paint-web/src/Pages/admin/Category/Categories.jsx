import { useEffect, useState } from 'react';
import { Table } from 'react-bootstrap';
import { Link } from 'react-router-dom';

// App's features
import { deleteCategoryById, getCategoriesByQueries } from '../../../Services/categories';

// App's components

import Loading from '../../../Components/store/Loading';
import Pager from '../../../Components/store/Pager';
import CategoryFilterPane from '../../../Components/admin/CategoryFilterPane';

import styles from '../Layout/layout.module.scss';

export default function Categories() {
    // Component's states
    const [pageNumber, setPageNumber] = useState(1);
    const [keyword, setKeyword] = useState('');
    const [actived, setActived] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [metadata, setMetadata] = useState({});
    const [categories, setCategories] = useState([]);
    const [isChangeStatus, setIsChangeStatus] = useState(false);

    // Component's event handlers
    const handleChangePage = (value) => {
        setPageNumber((current) => current + value);
        window.scroll(0, 0);
    };

    const handleDeleteCategory = async (e, id) => {
        if (window.confirm('Bạn có chắc muốn xóa loại sản phẩm?')) {
            const data = await deleteCategoryById(id);
            if (data.isSuccess) alert(data.result);
            else alert(data.errors[0]);
            setIsChangeStatus(!isChangeStatus);
        }
    };

    useEffect(() => {
        document.title = 'Danh sách chủ đề';
        fetchCategories();

        async function fetchCategories() {
            const queries = new URLSearchParams({
                PageNumber: pageNumber || 1,
                PageSize: 10,
                Actived: actived || false,
            });
            keyword && queries.append('Keyword', keyword);

            const data = await getCategoriesByQueries(queries);
            if (data) {
                setCategories(data.items);
                setMetadata(data.metadata);
            } else {
                setCategories([]);
                setMetadata({});
            }
            setIsLoading(false);
        }
    }, [pageNumber, keyword, actived, isChangeStatus]);

    return (
        <div className="mb-5">
            <div className={styles.text}>Danh sách chủ đề</div>
            <CategoryFilterPane setKeyword={setKeyword} setActived={setActived} />
            {isLoading ? (
                <Loading />
            ) : (
                <>
                    <Table striped responsive bordered>
                        <thead>
                            <tr>
                                <th>Chủ đề</th>
                                <th>Hiện trên menu</th>
                                <th>Số sản phẩm</th>
                                <th>Xóa</th>
                            </tr>
                        </thead>
                        <tbody>
                            {categories.length > 0 ? (
                                categories.map((category) => (
                                    <tr key={category.id}>
                                        <td>
                                            <Link to={`/admin/categories/edit/${category.id}`} className="text-bold">
                                                {category.name}
                                            </Link>
                                        </td>

                                        <td>{category.actived ? 'Có' : 'Không'}</td>
                                        <td>{category.productsCount}</td>
                                        <td>
                                            <button
                                                type="button"
                                                className="btn btn-danger"
                                                onClick={(e) => handleDeleteCategory(e, category.id)}>
                                                Xóa
                                            </button>
                                        </td>
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan={4}>
                                        <h4 className="text-center text-danger">Không tìm thấy chủ đề</h4>
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
