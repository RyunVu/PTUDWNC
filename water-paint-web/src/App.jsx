import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { useEffect, useState } from 'react';

import {
    BadRequest,
    NotFound,
    StoreHome,
    StoreLayout,
    ContactPage,
    CategoryPage,
    AdminHome,
    AdminLayout,
    Categories,
    Products,
    ProductEdit,
    UnitEdit,
    CategoryEdit,
} from './Pages';

import { SignIn, ForgotPassword } from './Components/account';

function App() {
    const [roles, setRoles] = useState([]);

    useEffect(() => {
        if (localStorage.getItem('token')) {
            fetch(`${process.env.REACT_APP_API_ENDPOINT}/account/GetProfile`, {
                method: 'GET',
                headers: {
                    Authorization: localStorage.getItem('token').replace(/['"]+/g, ''),
                },
            })
                .then((response) => response.json())
                .then((responseToken) => {
                    setRoles(responseToken.result.roles);
                });
        }
        // eslint-disable-next-line
    }, []);

    return (
        <div className="container-custom">
            <Router>
                <Routes>
                    <Route path="/" element={<StoreLayout />}>
                        <Route path="/" element={<StoreHome />} />
                        <Route path="store" element={<StoreHome />} />
                        <Route path="store/category" element={<CategoryPage />} />
                        <Route path="store/contact" element={<ContactPage />} />
                        <Route path="store/login" element={<SignIn />} />
                        <Route path="store/forgetpassword" element={<ForgotPassword />} />
                    </Route>
                    {(roles !== [] && roles.some((role) => role.name === 'Admin')) ||
                    roles.some((role) => role.name === 'Manager') ? (
                        <Route path="/admin" element={<AdminLayout />}>
                            <Route path="/admin" element={<AdminHome />} />

                            <Route path="/admin/categories" element={<Categories />} />
                            <Route path="/admin/categories/edit" element={<CategoryEdit />} />
                            <Route path="/admin/categories/edit/:id" element={<CategoryEdit />} />

                            <Route path="/admin/products" element={<Products />} />
                            <Route path="/admin/products/edit" element={<ProductEdit />} />
                            <Route path="/admin/products/edit/:id" element={<ProductEdit />} />

                            {/* <Route path="/admin/products/edit/:id/units" element={<Units />} /> */}
                            <Route path="/admin/products/:productId/add/units" element={<UnitEdit />} />
                            <Route path="/admin/products/edit/:id/units/:id" element={<UnitEdit />} />
                        </Route>
                    ) : null}
                    <Route path="/400" element={<BadRequest />} />
                    <Route path="*" element={<NotFound />} />
                </Routes>
            </Router>
        </div>
    );
}

export default App;
