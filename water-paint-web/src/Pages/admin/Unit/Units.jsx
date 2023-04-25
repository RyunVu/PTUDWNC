// // Libraries
// import { useEffect, useState } from 'react';
// import { Button, Table } from 'react-bootstrap';
// import { Link } from 'react-router-dom';

// import { deleteUnitById, getUnitsByQueries, toggleUnitActivedStatus } from '../../../Services/units';

// import Pager from '../../../Components/store/Pager';
// import Loading from '../../../Components/store/Loading';
// import ProductFilterPane from '../../../Components/admin/ProductFilterPane';

// export default function Units() {
//     const [pageNumber, setPageNumber] = useState(1);
//     const [units, setUnits] = useState([]);
//     const [isLoading, setIsLoading] = useState(true);
//     const [metadata, setMetadata] = useState({});
//     const [keyword, setKeyword] = useState('');
//     const [productId, setProductId] = useState();
//     const [actived, setActived] = useState(false);
//     const [isChangeStatus, setIsChangeStatus] = useState(false);

//     // Component's event handlers
//     const handleChangePage = (value) => {
//         setPageNumber((current) => current + value);
//         window.scroll(0, 0);
//     };
//     const handleTogglePublishedStatus = async (e, id) => {
//         await toggleUnitActivedStatus(id);
//         setIsChangeStatus(!isChangeStatus);
//     };
//     const handleDeleteproduct = async (e, id) => {
//         if (window.confirm('Bạn có chắc muốn xóa loại sản phẩm ?')) {
//             const data = await deleteUnitById(id);
//             if (data.isSuccess) alert(data.result);
//             else alert(data.errors[0]);
//             setIsChangeStatus(!isChangeStatus);
//         }
//     };

//     useEffect(() => {
//         document.title = 'Danh sách sản phẩm';
//         fetchProducts();

//         async function fetchProducts() {
//             const queries = new URLSearchParams({
//                 Actived: true,
//                 NotActived: false,
//                 PageNumber: pageNumber || 1,
//                 PageSize: 10,
//             });
//             keyword && queries.append('Keyword', keyword);
//             productId && queries.append('ProductId', productId);

//             const data = await getUnitsByQueries(queries);
//             if (data) {
//                 setUnits(data.items);
//                 setMetadata(data.metadata);
//             } else {
//                 setUnits([]);
//                 setMetadata({});
//             }
//             setIsLoading(false);
//         }
//     }, [pageNumber, keyword, productId, actived, isChangeStatus]);

//     return (
//         <div className="mb-5">
//             <div className="text">Danh sách sản phẩm</div>
//             <ProductFilterPane setKeyword={setKeyword} setProductId={setProductId} setActived={setActived} />
//             {isLoading ? (
//                 <Loading />
//             ) : (
//                 <>
//                     <Table striped responsive bordered>
//                         <thead>
//                             <tr>
//                                 <th>Tiêu đề</th>
//                                 <th>Loại</th>
//                                 <th>Còn hàng</th>
//                                 <th>Xóa</th>
//                             </tr>
//                         </thead>
//                         <tbody>
//                             {units.length > 0 ? (
//                                 units.map((unit) => {
//                                     return (
//                                         <tr key={unit.id}>
//                                             <td>
//                                                 <Link
//                                                     to={`/admin/products/edit/${unit.productId}/units/${unit.id}`}
//                                                     className="text-bold">
//                                                     {unit.name}
//                                                 </Link>
//                                                 {/* <p className="text-muted">{product.shortDescription}</p> */}
//                                             </td>
//                                             {/* <td>
//                                                 {product.unitDetails.map((item, index) => {
//                                                     return (
//                                                         <span style={{ display: 'block' }} key={index}>
//                                                             {item.price}
//                                                         </span>
//                                                     );
//                                                 })}
//                                             </td> */}
//                                             <td className="d-flex justify-content-center">
//                                                 {unit.actived ? (
//                                                     <Button
//                                                         variant="primary"
//                                                         onClick={(e) => handleTogglePublishedStatus(e, unit.id)}>
//                                                         Có
//                                                     </Button>
//                                                 ) : (
//                                                     <Button
//                                                         variant="secondary"
//                                                         onClick={(e) => handleTogglePublishedStatus(e, unit.id)}>
//                                                         Không
//                                                     </Button>
//                                                 )}
//                                             </td>
//                                             <td className="text-center">
//                                                 <button
//                                                     class="btn btn-danger"
//                                                     onClick={(e) => handleDeleteproduct(e, unit.id)}>
//                                                     Xóa
//                                                 </button>
//                                             </td>
//                                         </tr>
//                                     );
//                                 })
//                             ) : (
//                                 <tr>
//                                     <td colSpan={5}>
//                                         <h4 className="text-center text-danger">Không tìm thấy bài viết</h4>
//                                     </td>
//                                 </tr>
//                             )}
//                         </tbody>
//                     </Table>
//                     <Pager metadata={metadata} onPageChange={handleChangePage} />
//                 </>
//             )}
//         </div>
//     );
// }
